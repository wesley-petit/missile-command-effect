// Position with left or right ray (point a position)
public class XRMovement : CursorMovement
{
	public void Awake() => _movementType = MovementType.XR;

	public override void InitMovement() => _inputs = GetComponent<InputHandler>();

	public override void UpdateMovement()
	{
		if (_inputs.XRMovement == UnityEngine.Vector2.zero)
			return;

		transform.position = _inputs.XRMovement;
	}
}
