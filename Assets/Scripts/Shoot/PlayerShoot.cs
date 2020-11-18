using UnityEngine;

public class PlayerShoot : CharacterShoot
{
	[SerializeField] private Transform _cursor = null;

	private InputController _inputs = new InputController();
	private bool[] _canShoot = new bool[2];                         // Fill with inputs

	#region Unity Methods
	// Verification array
	protected override void Start()
	{
		base.Start();

#if UNITY_EDITOR
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
	#endregion

	#region Private Methods
	private void TakeInputs()
	{
		_canShoot[0] = VerifyInput(_canShoot[0], _inputs.LeftShoot);
		_canShoot[1] = VerifyInput(_canShoot[1], _inputs.RightShoot);
	}

	/// If there is already an input, we keep it
	/// Else read input
	private bool VerifyInput(bool inputStock, bool newInput) => inputStock ? inputStock : newInput;

	protected override void ShootTurret()
	{
		for (int i = 0; i < _shootCanons.Length; i++)
		{
			if (_canShoot.Length <= i) { break; }

			if (_canShoot[i])
				_turret.Shoot(_shootCanons[i], _shootCanons[i].position, _cursor.position);

			_canShoot[i] = false;
		}
	}
	#endregion
}
