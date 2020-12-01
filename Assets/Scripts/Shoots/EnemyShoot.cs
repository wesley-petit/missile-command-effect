using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : CharacterShoot
{
	[SerializeField]
	private List<Transform> _targets                                // Target can be turret or building
		= new List<Transform>();
	[SerializeField]
	protected Transform[] _shootCanons                              // Shoot Canon / Shoot Origin, in the same order (left to right) as the inputs
		= new Transform[0];
	[SerializeField] private AudioSource _audios = null;            // Music source
	[SerializeField] private int _BPM = 160;

	private bool CanShoot => _timeToShoot < _time;
	private float GetTime => _audios.time;                          // Synchronize shoot with musics tracks

	private float _timeToShoot = 0f;                                // BPM in seconds
	private float _previousAudioTime = 0f;
	private float _delta = 0f;                                      // Difference with previous and actual audio time
	private float _time = 0f;

	private float debugPrev= 0f;

	private RandomElement<Transform> _targetRandom                  // Choose random Target 
		= new RandomElement<Transform>();
	private RandomElement<Transform> _canonRandom                   // Choose random Canon 
		= new RandomElement<Transform>();
	private Transform _target = null;
	private Transform _canon = null;

	#region Unity Methods
	protected override void Start()
	{
		base.Start();

		if (_shootCanons.Length <= 0)
		{
			Debug.LogError($"Shoot Canon are undefined in {gameObject.name}.");
			return;
		}

		if (_targets.Count <= 0)
		{
			Debug.LogError($"Targets are undefined in {gameObject.name}.");
			return;
		}

		ResetTimeToShoot();
		_time = _timeToShoot;
		PrepareNextShoot();
	}

	private void Update()
	{
		_delta = GetTime - _previousAudioTime;
		_time += _delta;
		Debug.Log($"Delta : {_delta}");

		if (CanShoot)
		{
			// It will always have a little value in rest
			_time -= _timeToShoot;

			ShootTurret();
			PrepareNextShoot();
		}

		_previousAudioTime = GetTime;
	}
	#endregion

	#region Prepare and Shoot
	// Prepare the next Shoot
	private void PrepareNextShoot()
	{
		_target = _targetRandom.Choose(_targets.ToArray());
		_canon = _canonRandom.Choose(_shootCanons);
	}

	protected override void ShootTurret() => _turret.Shoot(_canon, _canon.position, _target.position);
	#endregion

	// BPM dependant, BPM changes => TimeToShoot changes
	private void ResetTimeToShoot() => _timeToShoot = 60f / _BPM;
}