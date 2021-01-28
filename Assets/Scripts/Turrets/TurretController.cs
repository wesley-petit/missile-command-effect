using System.Collections.Generic;
using UnityEngine;

// Shoot a bullet
// Manage a pool system
public class TurretController : MonoBehaviour
{
	[SerializeField, Range(0f, 50f)] private float _speed = 10f;
	[SerializeField] private Bullet _canonBall = null;
	[SerializeField] private Transform _bulletContainer = null;     // Stock all bullet shoot by this turret
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

	private readonly Queue<Bullet> _availableProjectile             // Pool system
		= new Queue<Bullet>();

	// Contains shoot datas such as direction or angle
	private struct ShootDatas
	{
		public Vector3 Direction { get; set; }
		public float ShootAngle { get; set; }
		public Quaternion Rotation { get; set; }
	}

	#region Shoot Variants
	// Clone a bullet or take it in the pool system
	public void Shoot(Vector3 canonPosition, Vector3 targetPosition)
	{
		if (!_bulletContainer)
			return;

		Bullet bullet = GetBullet();

		// Distance, angle ...
		ShootDatas shootDatas = CalculateShoot(canonPosition, targetPosition);
		// Position bullet
		InitializeBullet(canonPosition, bullet, shootDatas.Rotation);
		LaunchBullet(bullet, shootDatas.Direction);
	}

	public void Shoot(Vector3 canonPosition, Vector3 targetPosition, PlayerCanon canon)
	{
		if (!_bulletContainer)
			return;

		Bullet bullet = GetBullet();

		// Distance, angle ...
		ShootDatas shootDatas = CalculateShoot(canonPosition, targetPosition);
		// Turret rotation
		canon.RotateTurret(shootDatas.ShootAngle);
		// Position bullet
		InitializeBullet(canonPosition, bullet, shootDatas.Rotation);
		LaunchBullet(bullet, shootDatas.Direction);
	}

	public void Shoot(Vector3 canonPosition, Vector3 targetPosition, ChangeMaterialWithRythm changeMaterial)
	{
		if (!_bulletContainer)
			return;

		Bullet bullet = GetBullet();

		// Set change material in first shoot
		MaterialProperties materialProperties = bullet.GetComponent<MaterialProperties>();
		if (materialProperties || !materialProperties.ChangeMaterialWithRythm)
		{
			materialProperties.enabled = false;
			materialProperties.ChangeMaterialWithRythm = changeMaterial;
			materialProperties.enabled = true;
		}

		// Distance, angle ...
		ShootDatas shootDatas = CalculateShoot(canonPosition, targetPosition);
		// Position bullet
		InitializeBullet(canonPosition, bullet, shootDatas.Rotation);
		LaunchBullet(bullet, shootDatas.Direction);
	}

	#endregion

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
			bullet.ResetPhysics();
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
	private ShootDatas CalculateShoot(Vector3 canonPosition, Vector3 targetPosition)
	{
		ShootDatas shootDatas = new ShootDatas();
		// Look at the bullet at the cursor, rotation on Z only
		shootDatas.Direction = (targetPosition - canonPosition).normalized;
		shootDatas.ShootAngle = Mathf.Atan2(shootDatas.Direction.y, shootDatas.Direction.x) * Mathf.Rad2Deg;
		shootDatas.Rotation = Quaternion.Euler(0f, 0f, shootDatas.ShootAngle - 90);

		return shootDatas;
	}

	private void InitializeBullet(Vector3 canonPosition, Bullet bullet, Quaternion rotation)
	{
		bullet.InitializeTransform(_bulletContainer, canonPosition, rotation);
		bullet.gameObject.SetActive(true);
	}

	private void LaunchBullet(Bullet bullet, Vector2 direction)
	{
		// Reset physics
		bullet.ResetPhysics();
		bullet.Rigidbody2D.AddForce(direction * _speed, _forceMode2D);
	}
	#endregion
}
