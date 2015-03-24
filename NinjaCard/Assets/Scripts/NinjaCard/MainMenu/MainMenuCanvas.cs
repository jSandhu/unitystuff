using UnityEngine;
using UnityEngine.UI;
using NinjaCard.SinglePlayer;

namespace NinjaCard.MainMenu
{
	public class MainMenuCanvas : MonoBehaviour 
	{
		public Button SinglePlayerButton;

		[Inject] public SetupSinglePlayerCmdSig SetupSinglePlayerCmdSig {get; set;}

		private void Awake()
		{
			GameContext.InjectionBinder.injector.Inject(this);
		}

		private void Start () 
		{
			SinglePlayerButton.onClick.AddListener(SetupSinglePlayerCmdSig.Dispatch);
		}

		private void OnDestroy()
		{
			SinglePlayerButton.onClick.RemoveAllListeners();
		}
	}
}