using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerShoot : CharacterShoot
{
	[SerializeField] private Transform _cursor = null;              // Target position
	[SerializeField]
	private PlayerCanon[] _playerCanons = new PlayerCanon[2];
	[SerializeField] private uint _maxAmmo = 4;
	[SerializeField] private AudioSource _blockSound = null;        // Audio when a turret has no ammo
	[SerializeField] private EnemyShoot _enemyShoot = null;

	public PlayerCanon[] PlayerCanons
	{
		get => _playerCanons;
		set
		{
			if (_playerCanons.Length != value.Length) { return; }

			_playerCanons = value;
		}
	}

	private InputHandler _inputs = null;

	#region Unity Methods
	// Verification array
	protected override void Start()
	{
		base.Start();
		_inputs = GetComponent<InputHandler>();

		if (!_cursor)
		{
			Debug.LogError("Cursor is undefined.");
			return;
		}

		if (!_blockSound)
		{
			Debug.LogError("Block Sound is undefined.");
			return;
		}

		CalibrateMaxAmmo();
	}

	private void OnEnable() => RoundSystem.Register(ResetCanon);
	private void OnDisable() => RoundSystem.Unregister(ResetCanon);

	// Take last inputs
	private void Update() => ReadLastInputs();

	// Use of a rigidbody
	private void FixedUpdate() => ShootTurret();
	#endregion

	#region Methods
	private void CalibrateMaxAmmo() => _maxAmmo = (uint)_enemyShoot.CountCanons;

	public void ResetCanon()
	{
		for (int i = 0; i < _playerCanons.Length; i++)
		{
			_playerCanons[i].MaxAmmos(_maxAmmo);
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
			// Play block sound when a turret doesn't have ammos
			else if (currentCanon.Input && currentCanon.Ammos <= 0 && _blockSound)
			{
				_blockSound.Play();
			}

			currentCanon.Input = false;
		}
	}
	#endregion
}
