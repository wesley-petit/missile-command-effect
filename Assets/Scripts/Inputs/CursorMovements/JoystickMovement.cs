using UnityEngine;

// Movement with Joystick / Keyboard
public class JoystickMovement : CursorMovement
{
	[SerializeField] private float _speed = 5f;
	[SerializeField]
	private Vector2 _clamp = new Vector2(6f, 4.25f);                // Clamp to block off screen
	[SerializeField] private float _posZ = 0f;                      // To hide the cursor in other instance

	private Vector2 _cursorPosition = Vector2.zero;

	public void Awake() => _movementType = MovementType.JOYSTICK;

	public override void InitMovement()
	{
		_inputs = GetComponent<InputHandler>();
		transform.position = new Vector3(transform.position.x,
										transform.position.y,
										_posZ);
		_cursorPosition = transform.position;
	}

	public override void UpdateMovement()
	{
		if (_inputs.Movement == Vector2.zero)
			return;

		Vector2 movement = _inputs.Movement * _speed * Time.deltaTime;
		_cursorPosition += new Vector2(movement.x, movement.y);

		// Clamp value
		_cursorPosition.x = Mathf.Clamp(_cursorPosition.x, -_clamp.x, _clamp.x);
		_cursorPosition.y = Mathf.Clamp(_cursorPosition.y, -_clamp.y, _clamp.y);

		transform.position = _cursorPosition;
	}
}
