using UnityEngine;

// Player or Enemy bullet
public class Bullet : MonoBehaviour, ICollidable
{
	[SerializeField] private Rigidbody2D _rb2D = null;
	[SerializeField] private float _offScreen = 30f;                // Add to pool in a certain offset
	[SerializeField] private Transform _heading = null;             // GFX to change rotation
	[SerializeField] private Explosion _explosionPrefab = null;     // Explosion
	[SerializeField] private ParticlePool _particlePool = null;     // Manage particle

	public TurretController Turret { get; set; }
	public Rigidbody2D Rigidbody2D => _rb2D;

	private bool IsOffScreen => _offScreen < transform.position.magnitude;

	private bool _alreadyHit = false;                               // Launch explosion only on the first collision

	private void Update()
	{
		if (IsOffScreen)
		{
			AddToPool();
		}
	}

	public void Hit()
	{
		// Explose only one time
		if (_alreadyHit) { return; }
		_alreadyHit = true;

		AddToPool();
		Instantiate(_explosionPrefab, transform.position, transform.rotation);
	}

	public void InitializeTransform(Transform parent, Vector3 canonPosition, Quaternion rotationToDirection)
	{
		_heading.parent = parent;
		_heading.position = canonPosition;
		_heading.rotation = rotationToDirection;
		// Reset Hit
		_alreadyHit = false;

		if (_particlePool)
		{
			_particlePool.LinkWithParticles();
		}
	}

	public void ResetPhysics()
	{
		_rb2D.velocity = Vector2.zero;
		_rb2D.angularVelocity = 0f;
	}

	// OffScreen or Hit
	private void AddToPool()
	{
		ResetPhysics();

		if (_particlePool)
		{
			_particlePool.Unlink();
		}
		gameObject.SetActive(false);
		Turret.AddBullet(this);
	}
}
