using UnityEngine;
using TMPro;

public class RebindingDisplay : MonoBehaviour
{
	[SerializeField] private TMP_Text _actionDisplayNameText = null;		// Action Display
	[SerializeField] private TMP_Text _bindingDisplayNameText = null;		// Binding Display
	[SerializeField] private GameObject _startRebindObject = null;			// Button show the current input
	[SerializeField] private GameObject _waitingForInputObject = null;      // UI Waiting input

	public void SetActionDisplay(string actionDisplay) => _actionDisplayNameText.text = actionDisplay;

	// Change UI to give instructions
	public void SetDisplay(bool showWaiting)
	{
		_startRebindObject.SetActive(!showWaiting);
		_waitingForInputObject.SetActive(showWaiting);
	}

	public void SetBindingText(string currentBinding) => _bindingDisplayNameText.text = currentBinding;
}
