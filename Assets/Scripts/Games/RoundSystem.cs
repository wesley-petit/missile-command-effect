using UnityEngine;

// Change round between play or score
public class RoundSystem : MonoBehaviour
{
	[SerializeField] private float _maxPlayTime = 20f;
	[SerializeField] private float _maxScoreTime = 5f;
	[SerializeField] private uint _maxRoundNumber = 2;

	public bool IsInPlay { get; private set; }
	public float MaxLastShootTime
	{
		get => _maxLastShootTime;
		set
		{
			if (value <= 0f) { return; }
			_maxLastShootTime = value;
		}
	}                                       // Real Time for a missile to reach a target
	public float MaxScoreTime => _maxScoreTime;

	public static System.Action OnPlayRound = null;                         // Callbacks to invoke a play round
	public static System.Action OnScoreRound = null;                        // Callbacks to invoke a score round

	private readonly Timer _timer = new Timer();
	private float _maxLastShootTime = 0;
	private RoundPhase _currentPhase = RoundPhase.PLAY;
	private uint _currentRound = 0;

	private enum RoundPhase { PLAY, LAST_SHOOT, SCORE }

	#region Unity Methods
	private void Start()
	{
		// Switch score to play round
		_currentPhase = RoundPhase.SCORE;
		SwitchPhase();
	}

	private void OnEnable() => _timer.Register(SwitchPhase);

	private void OnDisable() => _timer.Unregister(SwitchPhase);

	private void Update()
	{
		if (!GameState.EndGame)
		{
			_timer.UpdateTimer();
		}
		// Switch to score round for the end of the game
		else if (_currentPhase != RoundPhase.SCORE)
		{
			SwitchPhase();
		}
	}
	#endregion

	public void CalculateMaxTime(float lastShootTime)
	{
		if (lastShootTime <= 0f) { return; }

		lastShootTime -= _maxScoreTime;

		if (_maxPlayTime <= lastShootTime)
			return;

		if (lastShootTime < 0f)
			lastShootTime = 0f;

		_maxLastShootTime = lastShootTime;
		_maxPlayTime = _maxPlayTime - _maxLastShootTime;
	}

	// Launch a new wave or Display Scores
	private void SwitchPhase()
	{
		switch (_currentPhase)
		{
			case RoundPhase.PLAY:
				ChangePhase(RoundPhase.LAST_SHOOT, _maxLastShootTime);
				break;

			case RoundPhase.LAST_SHOOT:
				ChangePhase(RoundPhase.SCORE, _maxScoreTime);
				OnScoreRound?.Invoke();
				break;

			case RoundPhase.SCORE:
				if (_maxRoundNumber < _currentRound)
				{
					GameState.Win();
					break;
				}

				ChangePhase(RoundPhase.PLAY, _maxPlayTime);
				_currentRound++;
				OnPlayRound?.Invoke();
				break;

			default:
				break;
		}

		IsInPlay = _currentPhase == RoundPhase.PLAY;
	}

	private void ChangePhase(RoundPhase nextPhase, float maxTime)
	{
		_currentPhase = nextPhase;
		_timer.MaxTime = maxTime;
	}
}