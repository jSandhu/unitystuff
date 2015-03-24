using UnityEngine;
using System;
namespace LocalMultiPlayer
{
	public interface ILMPGameLobbyView 
	{
		event Action PlayerKicked;
		event Action GameStarted;
		event Action Closed;

		Transform transform{get;}
		void SetGameName(string gameName);
	}
}