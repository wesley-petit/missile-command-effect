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

	private readonly Queue<Bullet> _availableProjectile = new Queue<Bullet>();                       // Pool system

	/// Shoot a new bullet
	/// or take a one in the pool system
	public void Shoot(Vector3 canonPosition, Vector3 targetPosition, PlayerCanon canon = null)
	{
		if (!_bulletContainer)
			return;

		Bullet bullet = GetBullet();

		CalculateShoot(canonPosition, targetPosition, out Vector2 direction, out float shootAngle, out Quaternion rotation);

		if (canon)
			canon.RotateTurret(shootAngle);

		InitializeBullet(canonPosition, bullet, rotation);

		FinalShoot(bullet, direction);
	}

	// Add bullet in the pool system
	public void AddBullet(Bullet bullet)
	{
		if (!bullet)
			return;

		_availableProjectile.Enqueue(bullet);
	}

	#region Shoot Step
	// Get a bullet or a pool system
	private Bullet GetBullet()
	{
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

		return bullet;
	}

	// Return direction, shoot angle and rotation between a canon and his target
	private void CalculateShoot(Vector3 canonPosition, Vector3 targetPosition, out Vector2 direction, out float shootAngle, out Quaternion rotation)
	{
		// Look at the bullet at the cursor, rotation on Z only
		direction = (targetPosition - canonPosition).normalized;
		shootAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		rotation = Quaternion.Euler(0f, 0f, shootAngle - 90);
	}

	private void InitializeBullet(Vector3 canonPosition, Bullet bullet, Quaternion rotation)
	{
		bullet.InitializeTransform(_bulletContainer, canonPosition, rotation);
		bullet.gameObject.SetActive(true);
	}

	private void FinalShoot(Bullet bullet, Vector2 direction)
	{
		// Reset physics
		bullet.ResetPhysics();
		bullet.Rigidbody2D.AddForce(direction * _speed, _forceMode2D);
	}
	#endregion
}
