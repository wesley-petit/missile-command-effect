using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class CursorMovement : MonoBehaviour
{
	[SerializeField] private float _speed = 5f;

	private InputHandler _inputs = null;

	private void Start() => _inputs = GetComponent<InputHandler>();

	private void Update() => Move(_inputs.Movement);

	private void Move(Vector2 movement)
	{
		if (movement == Vector2.zero)
			return;

		movement *= _speed * Time.deltaTime;

		transform.position = new Vector3(transform.position.x + movement.x,
								   transform.position.y + movement.y,
								   0f);
	}
}
