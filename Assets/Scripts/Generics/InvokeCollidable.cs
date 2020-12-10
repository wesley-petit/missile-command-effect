using UnityEngine;

// Launch collidable in a trigger or collider
public class InvokeCollidable : MonoBehaviour
{
	// Hit an object
	private void OnCollisionEnter2D(Collision2D col)
	{
		var collidable = col.gameObject.GetComponent<ICollidable>();
		if (collidable == null) return;
		collidable.Hit();
	}

	// Object enter in the zone
	private void OnTriggerEnter2D(Collider2D col)
	{
		var collidable = col.gameObject.GetComponent<ICollidable>();
		if (collidable == null) return;
		collidable.Hit();
	}
}
