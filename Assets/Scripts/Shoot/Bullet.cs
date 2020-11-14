using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rb2D = null;
	[SerializeField] private float _offScreen = 700;                    // Add to pool in a certain offset
	[SerializeField] private Transform _heading = null;					// GFX to change rotation

	public TurretController Turret { get; set; }
	public Rigidbody2D Rigidbody2D => _rb2D;
	public Transform Heading => _heading;

	private bool IsOffScreen => _offScreen < transform.position.magnitude;

	private void Update()
	{
		if (IsOffScreen)
		{
			AddToPool();
		}
	}

	// Hit
	private void OnCollisionEnter2D(Collision2D col)
	{
	}

	public void SetParent(Transform parent)
	{
		_heading.parent = parent;
		_heading.position = parent.position;
		_heading.rotation = parent.rotation;
	}

	// OffScreen or Hit
	private void AddToPool()
	{
		Turret.AddBullet(this);
		ResetPhysics();
		gameObject.SetActive(false);
	}

	private void ResetPhysics()
	{
		_rb2D.velocity = Vector2.zero;
		_rb2D.angularVelocity = 0f;
	}
}
