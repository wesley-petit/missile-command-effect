using UnityEngine;

public class PlayerShoot : CharacterShoot
{
	[SerializeField] private Transform _cursor = null;
	[SerializeField] private PlayerCanon[] _playerCanons = new PlayerCanon[2];
	[SerializeField] private int _maxAmmo = 4;

	private InputController _inputs = new InputController();

	#region Unity Methods
	// Verification array
	protected override void Start()
	{
		base.Start();
		ResetAmmos();
	}

	// Take last inputs
	private void Update() => TakeInputs();

	// Use of a rigidbody
	private void FixedUpdate() => ShootTurret();
	#endregion

	public void ResetAmmos()
	{
		for (int i = 0; i < _playerCanons.Length; i++)
		{
			_playerCanons[i].Ammos = _maxAmmo;
		}
	}

	#region Private Methods
	private void TakeInputs()
	{
		_playerCanons[0].Input = VerifyInput(_playerCanons[0].Input, _inputs.LeftShoot);
		_playerCanons[1].Input = VerifyInput(_playerCanons[1].Input, _inputs.RightShoot);
	}

	/// If there is already an input, we keep it
	/// Else read input
	private bool VerifyInput(bool inputStock, bool newInput) => inputStock ? inputStock : newInput;

	protected override void ShootTurret()
	{
		for (int i = 0; i < _playerCanons.Length; i++)
		{
			PlayerCanon currentCanon = _playerCanons[i];

			if (currentCanon.CanShoot)
			{
				_turret.Shoot(currentCanon.GetCanon, currentCanon.GetPosition, _cursor.position);
				currentCanon.ReduceAmmos();
			}

			currentCanon.Input = false;
			_playerCanons[i] = currentCanon;
		}
	}
	#endregion
}
