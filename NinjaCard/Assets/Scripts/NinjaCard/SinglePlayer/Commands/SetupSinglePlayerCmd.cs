using UnityEngine;
using strange.extensions.command.impl;

namespace NinjaCard.SinglePlayer
{
	public class SetupSinglePlayerCmd : Command 
	{
		override public void Execute()
		{
			Debug.Log("Start single player game");
		}
	}
}