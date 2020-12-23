using System.Collections.Generic;
using UnityEngine;

// Shoot a bullet
// Manage a pool system
public class TurretController : MonoBehaviour
{
	[SerializeField, Range(0f, 50f)] private float _speed = 10f;
	[SerializeField] private Bullet _canonBall = null;
	[SerializeField] private Transform _bulletContainer = null;                             // Stock all bullet shoot by this turret
	[SerializeField] private ForceMode2D _forceMode2D = ForceMode2D.Impulse;

	public float Speed
	{
		get => _speed;
		set
		{
			if (_speed <= 0) { return; }
			_speed = value;
		}
	}
	public Quaternion RotationToDirection { get; private set; }                             // Bullet Direction

	private Queue<Bullet> _availableProjectile = new Queue<Bullet>();                       // Pool system
	private Vector2 _direction = Vector2.zero;

	/// Shoot a new bullet
	/// or take a one in the pool system
	public void Shoot(Vector3 canonPosition, Vector3 targetPosition)
	{
		if (!_bulletContainer)
			return;

		Bullet bullet;

		// If a bullet is in the pool System
		if (0 < _availableProjectile.Count)
		{
			bullet = _availableProjectile.Dequeue();
		}
		// Create a new bullet
		else
		{
			bullet = Instantiate(_canonBall);
			bullet.Turret = this;
		}

		RotationToDirection = CalculateRotation(canonPosition, targetPosition);
		bullet.InitializeTransform(_bulletContainer, canonPosition, RotationToDirection);
		bullet.gameObject.SetActive(true);

		// Reset physics
		bullet.ResetPhysics();
		bullet.Rigidbody2D.AddForce(_direction * _speed, _forceMode2D);
	}

	private Quaternion CalculateRotation(Vector3 canonPosition, Vector3 targetPosition)
	{
		// Look at the bullet at the cursor, rotation on Z only
		_direction = (targetPosition - canonPosition).normalized;
		float rotZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
		return Quaternion.Euler(0f, 0f, rotZ - 90);
	}

	// Add bullet in the pool system
	public void AddBullet(Bullet bullet)
	{
		if (!bullet)
			return;

		_availableProjectile.Enqueue(bullet);
	}
}
