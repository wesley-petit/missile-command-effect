using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Enemy shoot in music rythm
public class EnemyShoot : CharacterShoot
{
	[SerializeField]
	private List<CollidableBuilding> _targets =                     // Target can be a player turret or a building
		new List<CollidableBuilding>();
	[SerializeField]
	private Transform[] _canons = new Transform[0];                 // Canon / Origin, in the same order (left to right)
	[SerializeField] private int _musicTimeToReachTarget = 8;       // Time for a missile to reach his target
	[SerializeField] private RoundSystem _roundSystem = null;       // If we are in a play or score time

	private float[,] _speedToReachTargets = new float[0, 0];        // Speed Canons to targets
	private readonly RandomElement _targetRandom = new RandomElement();      // Choose a random Target
	private readonly RandomElement _canonRandom = new RandomElement();       // Choose a random Canon
	private Transform _currentTarget = null;
	private Transform _currentCanon = null;

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

		if (!_roundSystem)
		{
			Debug.LogError($"Round System is undefined in {name}.");
			return;
		}

		SetSpeedToTargets();
		PrepareNextShoot();
	}

	private void Update()
	{
		if (!_roundSystem) { return; }

		if (AudioSync.Instance.IsInPace && _roundSystem.IsInPlay)
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
		// Audio sync to shoot in music pace
		float time = AudioSync.Instance.ShootTime * _musicTimeToReachTarget;
		// Give the delay for a bullet to hit a target
		_roundSystem.CalculateMaxTime(time);

		for (int i = 0; i < _canons.Length; i++)
		{
			for (int y = 0; y < _targets.Count; y++)
			{
				// Speed with each distance (canon-target)
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
		int iCanon = _canonRandom.Choose(_canons.Length);
		int iTarget = _targetRandom.Choose(_targets.Count);

		// Verify Index out of bound
		if (0 <= iCanon && iCanon < _canons.Length && 0 <= iTarget && iTarget < _targets.Count)
		{
			_currentCanon = _canons[iCanon];
			_currentTarget = _targets[iTarget].transform;

			// Take the speed for the next shot
			_turret.Speed = _speedToReachTargets[iCanon, iTarget];
		}
	}

	protected override void ShootTurret()
	{
		if (_currentCanon && _currentTarget)
			_turret.Shoot(_currentCanon.position, _currentTarget.position);
	}
	#endregion
}
