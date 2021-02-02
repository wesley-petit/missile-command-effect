using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputHandler))]
public class PlayerShoot : CharacterShoot
{
	[SerializeField] private Transform _cursor = null;              // Target position
	[SerializeField]
	private PlayerCanon[] _playerCanons = new PlayerCanon[2];
	[SerializeField] private uint _maxAmmo = 4;
	[SerializeField] private AudioSource _blockSound = null;        // Audio when a turret has no ammo
	[SerializeField] private EnemyShoot _enemyShoot = null;
    [SerializeField] private XRController controller1;
    [SerializeField] private XRController controller2;
    [SerializeField] private XRRayInteractor interactor1;
    [SerializeField] private XRRayInteractor interactor2;
 
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
                VibrationControls();
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
    private void GetControllers() {
        if (controller1 == null || controller2 == null) {
            var controllers = FindObjectsOfType<XRController>();
            if (controllers.Length > 0) {
                controller1 = controllers[0];
                interactor1 = controller1.gameObject.GetComponent<XRRayInteractor>();
            }
            if (controllers.Length > 1) {
                controller2 = controllers[1];
                interactor2 = controller2.gameObject.GetComponent<XRRayInteractor>();
            }
        }
    }
 
    private void VibrationControls() {
        GetControllers();
        if (interactor1.enabled) {
            controller1.inputDevice.SendHapticImpulse(0, 1f, 0.5f);
        } else if (interactor2.enabled) {
            controller2.inputDevice.SendHapticImpulse(0, 1f, 0.5f);
        }
    }
	#endregion
}
