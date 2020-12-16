using UnityEngine;

// Change round between play or score
public class RoundSystem : MonoBehaviour
{
	public static RoundSystem Instance { get; private set; }

	[SerializeField] private int _maxPlayTime = 70;
	[SerializeField] private int _maxScoreTime = 10;

	public bool IsInPlay { get => _play; }

	public static System.Action OnPlayRound = null;                        // Callbacks to invoke a play round
	public static System.Action OnScoreRound = null;                       // Callbacks to invoke a score round

	private Timer _timer = new Timer();
	private bool _play = true;

	#region Unity Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError($"Two singletons of the same types {typeof(RoundSystem)}.");
			Destroy(this);
		}
		Instance = this;

		UseMaxPlayTime();
	}

	private void OnEnable() => _timer.Register(Switch);

	private void OnDisable() => _timer.Unregister(Switch);

	private void Update() => _timer.UpdateTimer();
	#endregion

	#region Private Methods
	// Launch a new wave or Display Scores
	private void Switch()
	{
		_play = !_play;

		if (_play)
		{
			OnPlayRound?.Invoke();
			UseMaxPlayTime();
		}
		else
		{
			OnScoreRound?.Invoke();
			UseMaxScoreTime();
		}
	}

	// Set Max time in a play or score time
	private void UseMaxPlayTime() => _timer.MaxTime = _maxPlayTime;
	private void UseMaxScoreTime() => _timer.MaxTime = _maxScoreTime;
	#endregion
}