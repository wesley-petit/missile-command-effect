using System.Collections.Generic;
using UnityEngine;

// ShootWithRythm
public class EnemyShoot : CharacterShoot
{
    private const int COUNT_TO_REACH_TARGET = 8;                    // Time for a missile to reach his target

    [SerializeField]
    private List<Transform> _targets = new List<Transform>();       // Target can be a player turret or a building
    [SerializeField]
    protected Transform[] _canons = new Transform[0];               // Canon / Origin, in the same order (left to right)
    [SerializeField] private AudioSource _audios = null;            // Music tracks
    [SerializeField] private int _BPM = 40;

    private bool CanShoot => _shootTime < _time;
    private float GetTime => _audios.time;                          // Synchronize shoot with musics tracks

    private float _shootTime = 0f;                                  // BPM in seconds
    private float _previousAudioTime = 0f;
    private float _delta = 0f;                                      // Difference with previous and actual audio time
    private float _time = 0f;
    private float[,] _speedToReachTargets = new float[0, 0];        // Speed Canons to targets

    private RandomElement _targetRandom = new RandomElement();      // Choose a random Target
    private RandomElement _canonRandom = new RandomElement();       // Choose a random Canon
    private Transform _currentTarget = null;
    private Transform _currentCanon = null;

    #region Unity Methods
    protected override void Start()
    {
        base.Start();

        if (_canons.Length <= 0)
        {
            Debug.LogError($"Canons are undefined in {gameObject.name}.");
            return;
        }

        if (_targets.Count <= 0)
        {
            Debug.LogError($"Targets are undefined in {gameObject.name}.");
            return;
        }

        ResetTimeToShoot();
        _time = _shootTime;

        SetSpeedToTargets();

        PrepareNextShoot();
    }

    private void Update()
    {
        _delta = GetTime - _previousAudioTime;
        _time += _delta;

        if (CanShoot)
        {
            Debug.Log($"{_time} {_shootTime}");
            // It will always have a little value in rest
            _time -= _shootTime;

            ShootTurret();
            PrepareNextShoot();
        }

        _previousAudioTime = GetTime;
    }
    #endregion

    #region Time
    // BPM dependant, BPM changes => TimeToShoot changes
    private void ResetTimeToShoot() => _shootTime = 60f / _BPM;

    // Set speed bullet with the distance between canon and target
    private void SetSpeedToTargets()
    {
        _speedToReachTargets = new float[_canons.Length, _targets.Count];
        float distance;
        float time = _shootTime * COUNT_TO_REACH_TARGET;

        for (int i = 0; i < _canons.Length; i++)
        {
            for (int y = 0; y < _targets.Count; y++)
            {
                distance = Vector2.Distance(_canons[i].position, _targets[y].position);
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

        if (0 <= iCanon && iCanon < _canons.Length && 0 <= iTarget && iTarget < _targets.Count)
        {
            _currentCanon = _canons[iCanon];
            _currentTarget = _targets[iTarget];

            _turret.Speed = _speedToReachTargets[iCanon, iTarget];
        }
    }

    protected override void ShootTurret()
    {
        if (_currentCanon && _currentTarget)
        {
            _turret.Shoot(_currentCanon, _currentCanon.position, _currentTarget.position);
        }
    }
    #endregion
}
