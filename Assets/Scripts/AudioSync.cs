using UnityEngine;

public class AudioSync : MonoBehaviour
{
    [SerializeField] private AudioSource _audios = null;            // Music tracks
    [SerializeField] private int _BPM = 40;

    public float ShootTime { get; private set; }                    // BPM in seconds
    public bool IsInPace { get; private set; }

    private bool NextPace => ShootTime < _time;
    private float GetTime => _audios.time;                          // Synchronize shoot with musics tracks

    private float _previousAudioTime = 0f;
    private float _delta = 0f;                                      // Difference with previous and actual audio time
    private float _time = 0f;

	private void Start() => ResetTimeToShoot();

	private void Update()
	{
        _delta = GetTime - _previousAudioTime;
        _time += _delta;

        if (NextPace)
        {
            // It will always have a little value in rest
            _time -= ShootTime;
            IsInPace = true;
        }
        else
		{
            IsInPace = false;
		}

        _previousAudioTime = GetTime;
    }

    // BPM dependant, BPM changes => ShootTime changes
    private void ResetTimeToShoot() => ShootTime = 60f / _BPM;
}
