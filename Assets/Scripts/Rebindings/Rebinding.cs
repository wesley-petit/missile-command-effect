using UnityEngine;
using UnityEngine.InputSystem;

// Script created in most part by DapperDino
// Rebind an input in a selected action
public class Rebinding : MonoBehaviour
{
	[Header("Inputs references")]
	[SerializeField]
	private InputActionReference _actionReference = null;           // Input to rebind
	[SerializeField]
	private PlayerInput _playerInputs = null;                       // Action to switch between Player and UI inputs

	[Header("Callbacks")]
	[SerializeField]
	private BoolEvent OnUpdateRebindingState = null;                // Toggle UI for waiting inputs
	[SerializeField] private StringEvent OnUpdateNameAction = null;
	[SerializeField] private StringEvent OnUpdateBinding = null;

	private InputActionRebindingExtensions.RebindingOperation rebindingOperation;   // Current Rebinding

	private void Start()
	{
		OnUpdateRebindingState?.Invoke(false);

		if (!_actionReference)
		{
			Debug.LogError("Action Reference is undefined");
			return;
		}

		if (!_playerInputs)
		{
			Debug.LogError("Player Inputs is undefined");
			return;
		}

		OnUpdateNameAction?.Invoke(_actionReference.action.name);
		InvokeBindingDisplay();

		StartRebinding();
	}

	public void StartRebinding()
	{
		if (!_actionReference) { return; }
		if (!_playerInputs) { return; }

		OnUpdateRebindingState?.Invoke(true);

		_playerInputs.SwitchCurrentActionMap(InputMaps.UI);

		// Wait for input
		rebindingOperation = _actionReference.action.PerformInteractiveRebinding()
			.OnMatchWaitForAnother(0.1f)
			.OnComplete(operation => RebindCompleted())
			.Start();
	}

	private void RebindCompleted()
	{
		InvokeBindingDisplay();

		// Reset the rebinding
		rebindingOperation.Dispose();
		rebindingOperation = null;

		_playerInputs.SwitchCurrentActionMap(InputMaps.PLAYER);

		OnUpdateRebindingState?.Invoke(false);
	}

	private void InvokeBindingDisplay()
	{
		//Take the first element typed after waiting for input
		InputAction action = _actionReference.action;

		if (action.controls.Count <= 0) { return; }

		int controlBindingIndex = action.GetBindingIndexForControl(action.controls[0]);
		string controlBindingName = action.GetBindingDisplayString(controlBindingIndex);

		OnUpdateBinding?.Invoke(controlBindingName);
	}
}