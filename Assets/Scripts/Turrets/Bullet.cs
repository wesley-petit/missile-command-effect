using UnityEngine;

// Player or Enemy bullet
public class Bullet : MonoBehaviour, ICollidable
{
	[SerializeField] private Rigidbody2D _rb2D = null;
	[SerializeField] private float _offScreen = 30f;                // Add to pool in a certain offset
	[SerializeField] private Transform _heading = null;             // GFX to change rotation
	[SerializeField] private Explosion _explosionPrefab = null;     // Explosion
	[SerializeField]
	private BulletParticles[] _bulletParticles      // Let particle when bullet his alive 
		= new BulletParticles[0];

	public TurretController Turret { get; set; }
	public Rigidbody2D Rigidbody2D => _rb2D;

	private bool IsOffScreen => _offScreen < transform.position.magnitude;

	private bool _alreadyHit = false;                               // Launch explosion only on the first collision
	private BulletParticles _currentBulletParticle = null;

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

		LinkWithParticles();
	}

	public void ResetPhysics()
	{
		_rb2D.velocity = Vector2.zero;
		_rb2D.angularVelocity = 0f;
	}

	// Select one particle which have no particle / inactive
	private void LinkWithParticles()
	{
		foreach (var particle in _bulletParticles)
		{
			if (particle.ParticleCount <= 0)
			{
				_currentBulletParticle = particle;
				_currentBulletParticle.Link();
				_currentBulletParticle.gameObject.SetActive(true);
				break;
			}
		}
	}

	// OffScreen or Hit
	private void AddToPool()
	{
		ResetPhysics();

		// Let particle alive
		if (_currentBulletParticle)
			_currentBulletParticle.Unlink();

		gameObject.SetActive(false);
		Turret.AddBullet(this);
	}
}
