using UnityEngine;
using LocalMultiPlayer;

public class TestContext : MonoBehaviour 
{
	public Transform Container;

	void Start () 
	{
		LocalMultiplayerPanel lmp = LocalMultiplayerPanel.GetNewFromResources(new ConnectionSettingsVO());
		lmp.Connected += OnConnected;
		lmp.transform.SetParent(Container, false);
	}

	void OnConnected(string gameName, bool isServer)
	{
		ILMPGameLobbyView gameLobbyView = PlayerListLobbyView.GetNewFromResources();
		gameLobbyView.transform.SetParent(Container, false);
	}
}
