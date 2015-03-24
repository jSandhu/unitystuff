using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class JoiningStatusPanel : MonoBehaviour 
	{
		public event Action Closed;

		public Text JoiningStatusText;
		public Button CloseButton;

		private void Start()
		{
			CloseButton.onClick.AddListener(OnCloseClicked);
		}

		private void OnDestroy()
		{
			CloseButton.onClick.RemoveAllListeners();
		}


		public void SetStatusText(string text, bool showCloseButton = false)
		{
			JoiningStatusText.text = text;
			CloseButton.gameObject.SetActive(showCloseButton);
		}


		private void OnCloseClicked()
		{
			if (Closed != null)
			{
				Closed();
			}
		}
	}
}