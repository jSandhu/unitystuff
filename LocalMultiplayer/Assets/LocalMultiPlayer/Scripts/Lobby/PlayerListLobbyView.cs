using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class PlayerListLobbyView : MonoBehaviour, ILMPGameLobbyView
	{
		private const string PREFAB_RESOURCE_PATH = "LocalMultiPlayer/PlayerListLobbyView";
		public static PlayerListLobbyView GetNewFromResources()
		{
			PlayerListLobbyView prefab = Resources.Load<PlayerListLobbyView>(PREFAB_RESOURCE_PATH);
			return Instantiate<PlayerListLobbyView>(prefab);
		}

		public event Action PlayerKicked;
		public event Action GameStarted;
		public event Action Closed;

		public Text GameNameText;
		public Button CloseButton;

		private void Awake()
		{
			CloseButton.onClick.AddListener(OnClosed);
		}

		private void OnDestroy()
		{
			CloseButton.onClick.RemoveAllListeners();
		}

		private void OnClosed()
		{
			if (Network.isServer)
			{
				MasterServer.UnregisterHost();
			}
			Network.Disconnect();

			if (Closed != null)
			{
				Closed();
			}
			Destroy(gameObject);
		}

		public void SetGameName(string gameName)
		{
			GameNameText.text = gameName;
		}

	}
}
