using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerShoot : CharacterShoot
{
	[SerializeField] private Transform _cursor = null;              // Target position
	[SerializeField]
	private PlayerCanon[] _playerCanons = new PlayerCanon[2];
	[SerializeField] private uint _maxAmmo = 4;

	private InputHandler _inputs = null;

	#region Unity Methods
	// Verification array
	protected override void Start()
	{
		base.Start();
		_inputs = GetComponent<InputHandler>();
	}

	private void OnEnable() => RoundSystem.RegisterOnPlay(ResetCanon);
	private void OnDisable() => RoundSystem.UnregisterOnPlay(ResetCanon);

	// Take last inputs
	private void Update() => ReadLastInputs();

	// Use of a rigidbody
	private void FixedUpdate() => ShootTurret();
	#endregion

	#region Private Methods
	private void ResetCanon()
	{
		for (int i = 0; i < _playerCanons.Length; i++)
		{
			_playerCanons[i].Ammos = _maxAmmo;
			_playerCanons[i].GetBuilding.Show();
		}
	}

	private void ReadLastInputs()
	{
		_playerCanons[0].Input = VerifyInput(_playerCanons[0].Input, _inputs.LeftShoot);
		_playerCanons[1].Input = VerifyInput(_playerCanons[1].Input, _inputs.RightShoot);
	}

	/// If there is already an input, we keep it
	/// Else read input
	private bool VerifyInput(bool inputStock, bool newInput) => inputStock ? inputStock : newInput;

	protected override void ShootTurret()
	{
		foreach (var currentCanon in _playerCanons)
		{
			if (currentCanon.CanShoot)
			{
				_turret.Shoot(currentCanon.GetPosition, _cursor.position, currentCanon);
				currentCanon.ReduceAmmos();
			}

			currentCanon.Input = false;
		}
	}
	#endregion
}
