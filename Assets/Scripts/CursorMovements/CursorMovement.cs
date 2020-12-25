// Set the movement value in the cursor
// Each children will have a movement type
[UnityEngine.RequireComponent(typeof(InputHandler))]
public abstract class CursorMovement : UnityEngine.MonoBehaviour
{
	protected InputHandler _inputs = null;
	protected MovementType _movementType = MovementType.NONE;

	public MovementType GetMovementType => _movementType;

	public abstract void InitMovement();
	public abstract void UpdateMovement();
}
