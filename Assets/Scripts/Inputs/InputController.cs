using UnityEngine;

public class InputController : MonoBehaviour
{
	public bool LeftShoot { get; private set; }
	public bool RightShoot { get; private set; }

	private Controls _controls = null;

	#region UnityMethods
	private void Awake()
	{
		_controls = new Controls();
		_controls.Player.LeftShoot.started += ctx => LeftShoot = true;
		_controls.Player.LeftShoot.canceled += ctx => LeftShoot = false;

		_controls.Player.RightShoot.started += ctx => RightShoot = true;
		_controls.Player.RightShoot.canceled += ctx => RightShoot = false;
	}

	private void OnEnable() => _controls.Enable();

	private void OnDisable() => _controls.Disable();

	private void FixedUpdate() => ResetInputs();
	#endregion

	private void ResetInputs()
	{
		LeftShoot = false;
		RightShoot = false;
	}
}
