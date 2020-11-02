using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	[SerializeField] private TurretController[] _turrets				// Turrets in the semae order as the inputs (left to right)
		= new TurretController[0];
	[SerializeField] private Transform _cursor;

	private InputController _inputs = new InputController();
	private bool[] _canShoot = new bool[2];                             // Fill with inputs

	// Verification array
	private void Start()
	{
#if UNITY_EDITOR
		if (_turrets.Length <= 0)
		{
			Debug.LogError($"Turrets are undefined in {gameObject.name}.");
			return;
		}

		if (_canShoot.Length < _turrets.Length)
		{
			Debug.LogWarning($"They are more turret than associate Inputs.");
			return;
		}
#endif
	}

	private void Update()
	{
		TakeInputs();
		ShootTurret();
	}

	private void TakeInputs()
	{
		_canShoot[0] = _inputs.LeftShoot;
		_canShoot[1] = _inputs.RightShoot;
	}

	private void ShootTurret()
	{
		for (int i = 0; i < _turrets.Length; i++)
		{
			if (_canShoot.Length <= i) { break; }

			if (_canShoot[i])
			{
				Vector2 direction = _cursor.position - _turrets[i].transform.position;
				_turrets[i].Shoot(direction.normalized);
			}
		}
	}
}
