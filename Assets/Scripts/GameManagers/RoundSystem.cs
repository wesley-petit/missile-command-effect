using UnityEngine;

// Change round between play or score
public class RoundSystem : MonoBehaviour
{
	[SerializeField] private int _maxPlayTime = 20;
	[SerializeField] private int _maxScoreTime = 7;

	public bool IsInPlay { get => _play; }

	public static System.Action OnPlayRound = null;                        // Callbacks to invoke a play round
	public static System.Action OnScoreRound = null;                       // Callbacks to invoke a score round

	private readonly Timer _timer = new Timer();
	private bool _play = true;

	#region Unity Methods
	private void Awake() => UseMaxPlayTime();

	private void OnEnable() => _timer.Register(Switch);

	private void OnDisable() => _timer.Unregister(Switch);

	private void Update()
	{
		if (!GameState.EndGame)
		{
			_timer.UpdateTimer();
		}
		// Switch to score round for the end of the game
		else if (_play)
		{
			Switch();
		}
	}
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