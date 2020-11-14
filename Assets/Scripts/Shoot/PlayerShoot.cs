using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	[SerializeField] private TurretController _turret = null;
	[SerializeField] private Transform[] _shootCanons				// Shoot Canon / Origin of shoot, in the same order as the inputs (left to right)
		= new Transform[0];
	[SerializeField] private Transform _cursor = null;

	private InputController _inputs = new InputController();
	private bool[] _canShoot = new bool[2];							// Fill with inputs

	// Verification array
	private void Start()
	{
#if UNITY_EDITOR
		if (_shootCanons.Length <= 0)
		{
			Debug.LogError($"Shoot Canon are undefined in {gameObject.name}.");
			return;
		}

		if (!_turret)
		{
			Debug.LogError($"Turret is undefined in {gameObject.name}.");
			return;
		}

		if (_canShoot.Length < _shootCanons.Length)
		{
			Debug.LogWarning($"They are more turret than associate Inputs.");
			return;
		}
#endif
	}

	// Take last inputs
	private void Update() => TakeInputs();

	// Use of a rigidbody
	private void FixedUpdate() => ShootTurret();

	private void TakeInputs()
	{
		_canShoot[0] = VerifyInput(_canShoot[0], _inputs.LeftShoot);
		_canShoot[1] = VerifyInput(_canShoot[1], _inputs.RightShoot);
	}

	/// If there is already an input, we keep it
	/// Else read input
	private bool VerifyInput(bool inputStock, bool newInput) => inputStock ? inputStock : newInput;

	private void ShootTurret()
	{
		for (int i = 0; i < _shootCanons.Length; i++)
		{
			if (_canShoot.Length <= i) { break; }

			if (_canShoot[i])
			{
				Vector2 direction = _cursor.position - _shootCanons[i].transform.position;
				_turret.Shoot(_shootCanons[i], direction.normalized);
			}

			_canShoot[i] = false;
		}
	}
}
