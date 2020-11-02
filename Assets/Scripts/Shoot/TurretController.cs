using System.Collections.Generic;
using UnityEngine;

// Manage a pool system
public class TurretController : MonoBehaviour
{
	[SerializeField, Range(0f, 10000f)] private float _speed = 10000f;
	[SerializeField] private Bullet _canonBall = null;
	[SerializeField] private Transform _shootCanon = null;
	[SerializeField] private ForceMode2D _forceMode2D = ForceMode2D.Impulse;

	private Queue<Bullet> _availableProjectile = new Queue<Bullet>();   // Pool system

	/// Shoot a new bullet
	/// or take a one in the pool system
	public void Shoot(Vector2 direction)
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
			bullet = Instantiate(_canonBall, _shootCanon);
			bullet.Turret = this;
		}

		bullet.transform.position = _shootCanon.position;
		bullet.transform.rotation = _shootCanon.rotation;
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