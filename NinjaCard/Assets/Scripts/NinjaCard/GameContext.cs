using UnityEngine;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.framework.api;
using NinjaCard.SceneManagement;
using NinjaCard.MainMenu;
using NinjaCard.SinglePlayer;

namespace NinjaCard
{
	public class GameContext : MonoBehaviour 
	{
		public static IInjectionBinder InjectionBinder;
		private ICommandBinder commandBinder;

		private void Awake()
		{
			InjectionBinder = new CrossContextInjectionBinder();
			InjectionBinder.Bind<IInstanceProvider>().Bind<IInjectionBinder>().ToValue(InjectionBinder);
			InjectionBinder.Unbind<ICommandBinder>();
			InjectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
			commandBinder = InjectionBinder.GetInstance<ICommandBinder>();

			SetupGameBindings();
			SetupMainMenuBindings();
			SetupSinglePlayerBindings();
		}

		private void SetupGameBindings()
		{
			InjectionBinder.Bind<GameContext>().ToValue(this);

			commandBinder.Bind<LoadSceneRootCmdSig>().To<LoadSceneRootCmd>();
		}

		private void SetupMainMenuBindings()
		{
			commandBinder.Bind<ShowMainMenuCmdSig>().To<ShowMainMenuCmd>();
		}

		private void SetupSinglePlayerBindings()
		{
			commandBinder.Bind<SetupSinglePlayerCmdSig>().To<SetupSinglePlayerCmd>();
		}

		private void Start () 
		{
			InjectionBinder.GetInstance<ShowMainMenuCmdSig>().Dispatch();
		}
	}
}