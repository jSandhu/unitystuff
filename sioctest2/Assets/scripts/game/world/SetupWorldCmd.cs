using strange.extensions.command.impl;
using UnityEngine;
using game.mockdata;
using game.utils;

namespace game.world
{
	public class SetupWorldCmd:Command
	{
		[Inject] public string worldID{get;set;}
		
		override public void Execute()
		{
			Debug.Log("setup world command " + worldID);
			// TODO: load world data
			onWorldDataLoaded(MockWorldData.RAW_WORLD_DATA);
		}
		
		private void onWorldDataLoaded(string rawWorldData)
		{
			JSONObject jsonObject = new JSONObject(rawWorldData);
			JSONUtils.printData(jsonObject);
		}
	}
}

