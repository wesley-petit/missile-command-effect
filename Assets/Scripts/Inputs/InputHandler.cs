using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

// Interface between player input and script that use it
// Contains inputs which can be rebind or XR input like ray
// Doesn't contain input like menu or submit because it will 
// not be rebind (Controls class is much easier than Player Input)
public class InputHandler : MonoBehaviour
{
	[SerializeField] private XRRayInteractor _leftRay = null;       // XR to get the position where it's point
	[SerializeField] private XRRayInteractor _rightRay = null;      // XR to get the position where it's point

	public bool LeftShoot { get; private set; }
	public bool RightShoot { get; private set; }
	public Vector2 Movement { get; private set; }
	public Vector2 XRMovement { get => _hit.point; }

	private RaycastHit _hit = new RaycastHit();                     // RaycastHit use by the XRRay

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

	// Set XR Movement with left or right hand (button pressed in left or right hand)
	public void SetLeftHand()
	{
		if (!_leftRay) { return; }
		_leftRay.GetCurrentRaycastHit(out _hit);
	}
	public void SetRightHand()
	{
		if (!_rightRay) { return; }
		_rightRay.GetCurrentRaycastHit(out _hit);
	}
	#endregion

	// Reset inputs for the next read input => it's easier for player input
	private void ResetInputs()
	{
		LeftShoot = false;
		RightShoot = false;
	}
}
