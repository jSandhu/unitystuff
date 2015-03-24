using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class CreatingGamePanel : MonoBehaviour 
	{
		public event Action Closed;

		public Text GameCreationStatusText;
		public Button OkButton;

		private void Start()
		{
			OkButton.onClick.AddListener(OnOkButtonClicked);
		}

		private void OnDestroy()
		{
			OkButton.onClick.RemoveAllListeners();
			Closed = null;
		}

		private void OnOkButtonClicked()
		{
			if (Closed != null)
			{
				Closed();
			}
		}

		public void SetStatusText(string statusMessage, bool showOkButton = false)
		{
			GameCreationStatusText.text = statusMessage;
			OkButton.gameObject.SetActive(showOkButton);
		}

	}
}
