using System.Collections.Generic;
using UnityEngine;

// Manage a pool system
public class TurretController : MonoBehaviour
{
	[SerializeField, Range(0f, 10000f)] private float _speed = 10000f;
	[SerializeField] private Bullet _canonBall = null;
	[SerializeField] private ForceMode2D _forceMode2D = ForceMode2D.Impulse;

	private Queue<Bullet> _availableProjectile = new Queue<Bullet>();   // Pool system

	public float Speed
	{
		get => _speed;
		set => _speed = value;
	}

	/// Shoot a new bullet
	/// or take a one in the pool system
	public void Shoot(Transform parent, Vector3 canonPosition, Vector3 targetPosition)
	{
		if (!parent)
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

		// Look at the bullet at the cursor, rotation on Z only
		Vector3 direction = (targetPosition - canonPosition).normalized;
		float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		bullet.InitializeTransform(parent, Quaternion.Euler(0f, 0f, rotZ - 90));
		bullet.gameObject.SetActive(true);

		bullet.Rigidbody2D.AddForce(direction * _speed, _forceMode2D);
	}

	public void AddBullet(Bullet bullet)
	{
		if (!bullet)
			return;

		_availableProjectile.Enqueue(bullet);
	}
}
