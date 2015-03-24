using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class JoinPanel : MonoBehaviour 
	{
		public event Action<string> GameJoined;
		public event Action Canceled;

		public Transform GamesList;
		public Button CancelButton;
		public JoiningStatusPanel JoinStatusPanel;
		public GameEntry GameEntryPrefab;
		public float MasterServerPollTimeSec = 3.0f;

		private ConnectionSettingsVO connectionSettingsVO;
		public ConnectionSettingsVO ConnectionSettingsVO{
			get{if(connectionSettingsVO == null) {connectionSettingsVO = new ConnectionSettingsVO();} return connectionSettingsVO;} 
			set{connectionSettingsVO = value;}
		}

		private bool pollMasterServerList = false;
		private float masterServerPollTimeElapsedSec = 0f;

		private void Start()
		{
			CancelButton.onClick.AddListener(OnCancelClicked);
			JoinStatusPanel.Closed += ResetAndShow;
		}

		private void OnDestroy()
		{
			CancelButton.onClick.RemoveAllListeners();
		}

		public void ResetAndShow()
		{
			ClearGamesList();
			CancelButton.gameObject.SetActive(true);
			masterServerPollTimeElapsedSec = MasterServerPollTimeSec;
			pollMasterServerList = true;
			gameObject.SetActive(true);
			JoinStatusPanel.gameObject.SetActive(false);
		}

		private void OnMasterServerEvent(MasterServerEvent msEvent) 
		{
			if (msEvent == MasterServerEvent.HostListReceived)
			{
				HostData[] hostDatas = MasterServer.PollHostList();
				MasterServer.ClearHostList();
				UpdateGamesList(hostDatas);
			}
		}

		private void OnCancelClicked()
		{
			pollMasterServerList = false;
			ClearGamesList();
			gameObject.SetActive(false);
			if (Canceled != null)
			{
				Canceled();
			}
		}

		private void ClearGamesList()
		{
			for(int i = GamesList.transform.childCount - 1; i >= 0; i--)
			{
				Transform child = GamesList.transform.GetChild(i);
				Image[] images = child.GetComponentsInChildren<Image>();
				foreach(Image image in images)
				{
					image.sprite = null;
				}
				child.GetComponent<GameEntry>().Clicked -= OnGameEntryClicked;
				Destroy(child.gameObject);
			}
		}

		private void Update()
		{
			if (!pollMasterServerList)
			{
				return;
			}

			masterServerPollTimeElapsedSec += Time.deltaTime;
			if (masterServerPollTimeElapsedSec < MasterServerPollTimeSec)
			{
				return;
			}
			masterServerPollTimeElapsedSec = 0f;

			MasterServer.RequestHostList(connectionSettingsVO.GameType);
		}

		private void UpdateGamesList(HostData[] hostDatas)
		{
			ClearGamesList();
			foreach(HostData hostData in hostDatas)
			{
				GameEntry gameEntry = Instantiate<GameEntry>(GameEntryPrefab);
				gameEntry.GameNameText.text = hostData.gameName;
				gameEntry.transform.SetParent(GamesList.transform, false);
				gameEntry.HostData = hostData;
				gameEntry.Clicked += OnGameEntryClicked;
			}
		}


		private void OnGameEntryClicked(HostData hostData)
		{
			pollMasterServerList = false;
			ClearGamesList();

			JoinStatusPanel.gameObject.SetActive(true);
			JoinStatusPanel.SetStatusText(connectionSettingsVO.JoiningGameMessage);
			CancelButton.gameObject.SetActive(false);

			NetworkConnectionError error = Network.Connect(hostData.ip, hostData.port);
			if (error == NetworkConnectionError.NoError)
			{
				if (GameJoined != null)
				{
					GameJoined(hostData.gameName);
				}
				return;
			}

			JoinStatusPanel.SetStatusText(connectionSettingsVO.JoinFailedMessage + " Error: " + error, true);
		}
	}
}