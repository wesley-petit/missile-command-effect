using System.Collections.Generic;
using UnityEngine;

// Enemy shoot in music rythm
public class EnemyShoot : CharacterShoot
{
	[SerializeField]
	private List<CollidableBuilding> _targets =                     // Target can be a player turret or a building
		new List<CollidableBuilding>();
	[SerializeField]
	public Transform[] _canons = new Transform[0];                 // Canon / Origin, in the same order (left to right)
	[SerializeField] private ushort _musicTimeToReachTarget = 4;    // Music time for a missile to reach his target
	[SerializeField] private ChangeMaterialWithRythm _changeMaterial;// Change bullet material
	[SerializeField]
	private SettingsHandler _settingsHandler = null;                // Adjust difficulty with speed multiplier

	private float[,] _speedToReachTargets = new float[0, 0];        // Speed Canons to targets
	private readonly RandomElement _randomTarget                    // Choose a random Target
		= new RandomElement();
	private readonly RandomElement _randomCanon                     // Choose a random Canon
		= new RandomElement();
	private Transform _currentTarget = null;
	private Transform _currentCanon = null;
	private AudioSync _audioSync = null;
	private RoundSystem _roundSystem = null;

	#region Unity Methods
	protected override void Start()
	{
		base.Start();

		if (_canons.Length <= 0)
		{
			Debug.LogError($"Canons are undefined in {name}.");
			return;
		}

		if (_targets.Count <= 0)
		{
			Debug.LogError($"Targets are undefined in {name}.");
			return;
		}

		if (!_settingsHandler)
		{
			Debug.LogError($"Settings Handler is undefined in {name}.");
			return;
		}

		if (!_changeMaterial)
		{
			Debug.LogError($"Change Material is undefined in {name}.");
			return;
		}

		// Adjust difficulty
		_musicTimeToReachTarget *= _settingsHandler.Current.SpeedMultiplier;

		SetSpeedToTargets();
		PrepareNextShoot();
		_audioSync = AudioSync.Instance;
		_roundSystem = RoundSystem.Instance;
	}

	private void Update()
	{
		if (_audioSync.IsInPace && _roundSystem.IsInPlay)
		{
			ShootTurret();
			PrepareNextShoot();
		}
	}
	#endregion

	#region Time
	// Set speed bullet with the distance between canon and target
	private void SetSpeedToTargets()
	{
		_speedToReachTargets = new float[_canons.Length, _targets.Count];
		float distance;

		// Real time to hit a target
		float time = AudioSync.Instance.ShootTime * _musicTimeToReachTarget;
		// Give the delay for a bullet to hit a target
		RoundSystem.Instance.CalculateMaxTime(time);

		// Speed with each distance (canon to target)
		for (int i = 0; i < _canons.Length; i++)
		{
			for (int y = 0; y < _targets.Count; y++)
			{
				distance = Vector2.Distance(_canons[i].position, _targets[y].transform.position);
				_speedToReachTargets[i, y] = distance / time;
			}
		}
	}
	#endregion

	#region Prepare and Shoot
	// Prepare the next Shoot
	private void PrepareNextShoot()
	{
		uint iCanon = _randomCanon.Choose(_canons.Length);
		uint iTarget = _randomTarget.Choose(_targets.Count);

		// Verify Index out of bound
		if (iCanon < _canons.Length && iTarget < _targets.Count)
		{
			_currentCanon = _canons[iCanon];
			_currentTarget = _targets[(int)iTarget].transform;

			// Take the speed for the next shot
			_turret.Speed = _speedToReachTargets[iCanon, iTarget];
		}
	}

	protected override void ShootTurret()
	{
		if (_currentCanon && _currentTarget)
			_turret.Shoot(_currentCanon.position, _currentTarget.position, _changeMaterial);
	}
	#endregion
}
