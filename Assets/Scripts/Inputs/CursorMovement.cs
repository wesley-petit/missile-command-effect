using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Move cursor for controller player
[RequireComponent(typeof(InputHandler))]
public class CursorMovement : MonoBehaviour
{
	[SerializeField] private float _speed = 5f;
	[SerializeField]
	private Vector2 _clamp = new Vector2(7.5f, 5.5f);               // Clamp to block off screen
	[SerializeField] private float _posZ = 0f;                      // To hide the cursor in other instance

	private InputHandler _inputs = null;
	private Vector2 _cursorPosition = Vector2.zero;

	private void Start()
	{
		_inputs = GetComponent<InputHandler>();
		transform.position = new Vector3(transform.position.x,
										transform.position.y,
										_posZ);
		_cursorPosition = transform.position;
	}

	private void Update()
	{
		//Move(_inputs.Movement);
		XRMove(_inputs.XRMovement);
	}

	private void Move(Vector2 movement)
	{
		if (movement == Vector2.zero)
			return;

		movement *= _speed * Time.deltaTime;

		_cursorPosition += new Vector2(movement.x, movement.y);

		// Clamp value
		_cursorPosition.x = Mathf.Clamp(_cursorPosition.x, -_clamp.x, _clamp.x);
		_cursorPosition.y = Mathf.Clamp(_cursorPosition.y, -_clamp.y, _clamp.y);

		transform.position = _cursorPosition;
	}
	private void XRMove(Vector2 position) => transform.position = position;
}
