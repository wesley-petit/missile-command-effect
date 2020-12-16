using UnityEngine;
using UnityEngine.InputSystem;

// Interface between player input and script that use it
public class InputHandler : MonoBehaviour
{
	public bool LeftShoot { get; private set; }
	public bool RightShoot { get; private set; }
	public Vector2 Movement { get; private set; }

	private void FixedUpdate() => ResetInputs();

	#region Read Inputs
	public void SetLeftShoot(InputAction.CallbackContext cxt)
	{
		if (!cxt.started) { return; }

		LeftShoot = cxt.started;
	}
	public void SetRightShoot(InputAction.CallbackContext cxt)
	{
		if (!cxt.started) { return; }

		RightShoot = cxt.started;
	}
	public void SetMovement(InputAction.CallbackContext cxt) => Movement = cxt.ReadValue<Vector2>();
	#endregion

	private void ResetInputs()
	{
		LeftShoot = false;
		RightShoot = false;
	}
}
