using strange.extensions.context.impl;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using game.world;
using game.mockdata;

namespace game
{
	public class GameContext:MVCSContext
	{
		public GameContext():base()
		{
		
		}
		
		public GameContext(MonoBehaviour view, bool autoStart):base(view, autoStart)
		{
		
		}
		
		override protected void addCoreComponents()
		{
			Debug.Log("addCoreComponents");
			base.addCoreComponents();
			injectionBinder.Unbind<ICommandBinder>();
			injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
		}
		
		override protected void mapBindings()
		{
			Debug.Log("map bindings");
			injectionBinder.Bind<WorldSignalBus>().ToSingleton();
		}
		
		override public void Launch()
		{
			Debug.Log("launch");
			injectionBinder.GetInstance<WorldSignalBus>().setupWorld.Dispatch(MockWorldData.WORLD_ID);
		}
	}
}

