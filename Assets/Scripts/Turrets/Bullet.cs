using UnityEngine;

public class Bullet : MonoBehaviour, ICollidable
{
	[SerializeField] private Rigidbody2D _rb2D = null;
	[SerializeField] private float _offScreen = 30f;                    // Add to pool in a certain offset
	[SerializeField] private Transform _heading = null;                 // GFX to change rotation
	[SerializeField] private Explosion _explosionPrefab = null;			// Explosion

	public TurretController Turret { get; set; }
	public Rigidbody2D Rigidbody2D => _rb2D;

	private bool IsOffScreen => _offScreen < transform.position.magnitude;

	private void Update()
	{
		if (IsOffScreen)
		{
			AddToPool();
		}
	}

	public void Hit() => AddToPool();

	public void InitializeTransform(Transform parent, Vector3 canonPosition, Quaternion rotationToDirection)
	{
		_heading.parent = parent;
		_heading.position = canonPosition;
		_heading.rotation = rotationToDirection;
	}

	#region Private Methods
	// OffScreen or Hit
	private void AddToPool()
	{
		ResetPhysics();
		gameObject.SetActive(false);

		Turret.AddBullet(this);
		Instantiate(_explosionPrefab, transform.position, transform.rotation);
	}

	private void ResetPhysics()
	{
		_rb2D.velocity = Vector2.zero;
		_rb2D.angularVelocity = 0f;
	}
	#endregion
}
