using UnityEngine;

// Give music time to other instances
[RequireComponent(typeof(AudioSource))]
public class AudioSync : MonoBehaviour
{
	public static AudioSync Instance { get; private set; }

	[SerializeField] private ushort _BPM = 80;
	[SerializeField] private ushort _strongTime = 4;                // Time 4 is a strong time

	#region Fields
	public float ShootTime { get; private set; }                    // BPM in seconds
	public bool IsInPace { get; private set; }
	public bool IsInStrongTime { get; private set; }

	private bool NextPace => ShootTime < _time;
	private float GetTime => _audios.time;                          // Synchronize shoot with musics tracks
	#endregion

	private AudioSource _audios = null;                             // Music tracks
	private float _previousAudioTime = 0f;                          // Audio time in music tracks
	private float _delta = 0f;                                      // Difference with previous and actual audio time
	private float _time = 0f;                                       // Real time
	private ushort _musicTime = 0;                                  // Time in music

	#region Unity Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError($"Two singletons of the same types {typeof(AudioSync)}.");
			Destroy(this);
		}
		Instance = this;

		ResetTimeToShoot();
		_audios = GetComponent<AudioSource>();
	}

	private void Update()
	{
		// Take time in music track and not delta time to get
		// a better synchronization
		_delta = GetTime - _previousAudioTime;
		_time += _delta;

		if (NextPace)
		{
			InPace();
		}
		// Toggle value
		else if (IsInPace || IsInStrongTime)
		{
			IsInPace = false;
			IsInStrongTime = false;
		}

		_previousAudioTime = GetTime;
	}
	#endregion

	public void Play() => _audios.Play();
	public void Pause() => _audios.Pause();

	// BPM dependant, BPM changes => ShootTime changes
	private void ResetTimeToShoot() => ShootTime = 60f / _BPM;

	private void InPace()
	{
		// It will always have a little value in rest
		_time -= ShootTime;
		IsInPace = true;

		// Increase a time Music
		_musicTime++;
		// String Time
		if (_musicTime % _strongTime == 0)
		{
			IsInStrongTime = true;
			_musicTime -= _strongTime;
		}
	}
}
