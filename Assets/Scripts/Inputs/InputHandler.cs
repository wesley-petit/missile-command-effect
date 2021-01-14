using UnityEngine;
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
	public Vector2 XRMovement { get; private set; }

	private Controls _controls = null;
	private RaycastHit _hit = new RaycastHit();                     // RaycastHit use by the XRRay

	private void Awake() => _controls = new Controls();

	private void Start()
	{
		_controls.Player.LeftShoot.performed += cxt => LeftShoot = true;
		_controls.Player.RightShoot.performed += cxt => RightShoot = true;

		_controls.Player.Movement.performed += cxt => Movement = cxt.ReadValue<Vector2>();
		_controls.Player.Movement.canceled += cxt => Movement = Vector2.zero;

		_controls.Player.LeftHand.performed += cxt => XRMovement = XRPoint(_leftRay);
		_controls.Player.RightHand.performed += cxt => XRMovement = XRPoint(_rightRay);
	}

	private Vector3 XRPoint(XRRayInteractor ray)
	{
		ray.GetCurrentRaycastHit(out _hit);
		return _hit.point;
	}

	private void OnEnable() => _controls?.Enable();

	private void OnDisable() => _controls?.Disable();

	private void FixedUpdate() => ResetInputs();

	// Reset inputs for the next read input => it's easier for player input
	private void ResetInputs()
	{
		LeftShoot = false;
		RightShoot = false;
	}
}
