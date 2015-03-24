using UnityEngine;
using UnityEngine.UI;
using System;

namespace LocalMultiPlayer
{
	public class EnterGameNamePanel : MonoBehaviour 
	{
	    public event Action<string> GameNameEntered;

	    public int MinGameNameLen = 5;
	    public InputField GameNameInputField;
	    public Button NextButton;

	    private void Start()
	    {
	        GameNameInputField.onValueChange.AddListener(OnGameNameChanged);
	        NextButton.onClick.AddListener(OnNextButtonClicked);
	        NextButton.gameObject.SetActive(false);
	    }

	    private void OnDestroy()
	    {
	        GameNameInputField.onValueChange.RemoveAllListeners();
	        NextButton.onClick.RemoveAllListeners();
	        GameNameEntered = null;
	    }

	    private void OnGameNameChanged(string newName)
	    {
	        NextButton.gameObject.SetActive(newName.Length >= MinGameNameLen);
	    }

	    private void OnNextButtonClicked()
	    {
	        if (GameNameEntered != null)
	        {
	            GameNameEntered(GameNameInputField.text);
	        }
	    }
	}
}
