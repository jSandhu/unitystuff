using UnityEngine;
using strange.extensions.command.impl;

namespace NinjaCard.SceneManagement
{
	public class LoadSceneRootCmd : Command 
	{
		[Inject] public string SceneRootResourcePath {get; set;}
		[Inject] public GameContext GameContext{get; set;}

		override public void Execute()
		{
			if (injectionBinder.GetBinding<SceneRoot>() != null)
			{
				injectionBinder.Unbind<SceneRoot>();
			}

			for (int i = 0; i < GameContext.transform.childCount; i++)
			{
				GameObject.Destroy(GameContext.transform.GetChild(i).gameObject);
			}

			SceneRoot sceneRootInstance = GameObject.Instantiate<SceneRoot>(Resources.Load<SceneRoot>(SceneRootResourcePath));
			sceneRootInstance.transform.SetParent(GameContext.transform, false);
			injectionBinder.Bind<SceneRoot>().ToValue(sceneRootInstance);
		}
	}
}