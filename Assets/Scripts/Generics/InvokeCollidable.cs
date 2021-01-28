using UnityEngine;

// Launch collidable in a trigger or collider
public class InvokeCollidable : MonoBehaviour
{
	// Hit an object
	private void OnCollisionEnter2D(Collision2D col) => LaunchCollidable(col.gameObject);

	// Object enter in the zone
	private void OnTriggerEnter2D(Collider2D col) => LaunchCollidable(col.gameObject);

	private void LaunchCollidable(GameObject colGo)
	{
		var collidables = colGo.GetComponents<ICollidable>();
		if (collidables == null || collidables.Length <= 0) return;

		foreach (var oneCollidable in collidables)
			oneCollidable.Hit();
	}
}
