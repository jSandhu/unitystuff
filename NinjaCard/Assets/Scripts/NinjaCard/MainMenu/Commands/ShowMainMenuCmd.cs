using UnityEngine;
using strange.extensions.command.impl;
using NinjaCard.SceneManagement;

namespace NinjaCard.MainMenu
{
	public class ShowMainMenuCmd: Command
	{
		private const string SCENE_ROOT_RESOURCE_PATH = "SceneRoots/MainMenuSceneRoot";

		[Inject] public LoadSceneRootCmdSig LoadSceneRootCmdSig {get; set;}

		override public void Execute()
		{
			LoadSceneRootCmdSig.Dispatch(SCENE_ROOT_RESOURCE_PATH);
		}
	}
}