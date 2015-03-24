using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class LocalMultiplayerPanel : MonoBehaviour 
	{
		private const string PREFAB_RESOURCE_PATH = "LocalMultiPlayer/LocalMultiPlayerPanel";
		public static LocalMultiplayerPanel GetNewFromResources(ConnectionSettingsVO connectionSettingsVO)
		{
			LocalMultiplayerPanel prefab = Resources.Load<LocalMultiplayerPanel>(PREFAB_RESOURCE_PATH);
			LocalMultiplayerPanel instance = Instantiate<LocalMultiplayerPanel>(prefab);
			instance.ConnectionSettingsVO = connectionSettingsVO;
			return instance;
		}

		public event Action<string, bool> Connected; 
		public event Action Closed;

		public HostPanel HostPanel;
		public JoinPanel JoinPanel;
		public Button JoinButton;
		public Button HostButton;
		public Button CloseButton;

		public ConnectionSettingsVO ConnectionSettingsVO {
			get{return HostPanel.ConnectionSettingsVO;} 
			set{HostPanel.ConnectionSettingsVO = value; JoinPanel.ConnectionSettingsVO = value;}
		}

		private void Start()
		{
			HostPanel.gameObject.SetActive(false);
			JoinPanel.gameObject.SetActive(false);

			JoinButton.gameObject.SetActive(true);
			HostButton.gameObject.SetActive(true);
			JoinButton.onClick.AddListener(OnJoinClicked);
			HostButton.onClick.AddListener(OnHostCLicked);
			CloseButton.onClick.AddListener(OnCloseClicked);

			HostPanel.GameRegistered += OnHostedGameRegistered;
			HostPanel.Canceled += OnHostingCanceled;

			JoinPanel.GameJoined += OnGameJoined;
			JoinPanel.Canceled += OnJoinCanceled;
		}

		private void OnDestroy()
		{
			JoinButton.onClick.RemoveAllListeners();
			HostButton.onClick.RemoveAllListeners();
			CloseButton.onClick.RemoveAllListeners();

			HostPanel.GameRegistered -= OnHostedGameRegistered;
			HostPanel.Canceled -= OnHostingCanceled;

			JoinPanel.GameJoined -= OnGameJoined;
			JoinPanel.Canceled -= OnJoinCanceled;

			HostPanel = null;
			JoinPanel = null;
		}


		private void OnCloseClicked()
		{
			if (Closed != null)
			{
				Closed();
			}
			Destroy(gameObject);
		}

		private void SetButtonsVisibility(bool visible)
		{
			JoinButton.gameObject.SetActive(visible);
			HostButton.gameObject.SetActive(visible);
			CloseButton.gameObject.SetActive(visible);
		}

		private void OnJoinClicked()
		{
			SetButtonsVisibility(false);
			JoinPanel.ResetAndShow();
		}

		private void OnJoinCanceled()
		{
			SetButtonsVisibility(true);
		}

		private void OnGameJoined(string gameName)
		{
			if (Connected != null)
			{
				Connected(gameName, Network.isServer);
			}
			Destroy(gameObject);
		}

		private void OnHostCLicked()
		{
			SetButtonsVisibility(false);
			HostPanel.ResetAndShow();
		}

		private void OnHostingCanceled()
		{
			SetButtonsVisibility(true);
		}

		private void OnHostedGameRegistered(string gameName)
		{
			if (Connected != null)
			{
				Connected(gameName, Network.isServer);
			}
			Destroy(gameObject);
		}
	}
}