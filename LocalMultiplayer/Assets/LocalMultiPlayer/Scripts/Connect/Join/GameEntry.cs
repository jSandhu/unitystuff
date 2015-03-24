using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	[RequireComponent(typeof(Button))]
	public class GameEntry : MonoBehaviour 
	{
		public event Action<HostData> Clicked;

		public Text GameNameText;

		[HideInInspector] public HostData HostData;

		private Button buttonComponent;

		private void Start()
		{
			buttonComponent = GetComponent<Button>();
			buttonComponent.onClick.AddListener(OnEntryClicked);
		}

		private void OnDestroy()
		{
			Clicked = null;
			buttonComponent.onClick.RemoveAllListeners();
			buttonComponent = null;
		}

		private void OnEntryClicked()
		{
			if (Clicked != null)
			{
				Clicked(HostData);
			}
		}
	}
}