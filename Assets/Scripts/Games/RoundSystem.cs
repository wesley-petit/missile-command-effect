using UnityEngine;

// Change round between play or score
public class RoundSystem : MonoBehaviour
{
	public static RoundSystem Instance { get; private set; }

	[SerializeField] private float _maxPlayTime = 20f;              // Real time where the ennemy can shoot
	[SerializeField] private float _maxScoreTime = 5f;              // Real time where we display the score / let the player
	[SerializeField] private ushort _maxRoundNumber = 2;            // Max round to win

	public bool IsInPlay { get; private set; }
	public float MaxLastShootTime
	{
		get => _maxLastShootTime;
		set
		{
			if (value <= 0f) { return; }
			_maxLastShootTime = value;
		}
	}                               // Real Time for a missile to reach a target
	public float MaxScoreTime => _maxScoreTime;

	private static System.Action OnPlayRound = null;                // Callbacks to invoke a play round
	private static System.Action OnScoreRound = null;               // Callbacks to invoke a score round
	private readonly Timer _timer = new Timer();
	private float _maxLastShootTime = 0;
	private RoundPhase _currentPhase = RoundPhase.PLAY;
	private uint _currentRound = 0;

	private enum RoundPhase { PLAY, LAST_SHOOT, SCORE }

	#region Unity Methods
	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError($"Two singletons of the same types {typeof(RoundSystem)}.");
			Destroy(this);
		}
		Instance = this;
	}

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

	// Max Time in timer
	public void CalculateMaxTime(float lastShootTime)
	{
		//TODO Always fire
		return;
		if (lastShootTime <= 0f) { return; }

		// Last shoot time is the real time for a bullet to reach a target
		// We substract the score to let bullets in a previous reach
		// theirs targets before a new round
		lastShootTime -= _maxScoreTime;

		if (_maxPlayTime <= lastShootTime)
			return;

		if (lastShootTime < 0f)
			lastShootTime = 0f;

		_maxLastShootTime = lastShootTime;
		// Real time where enemy shoot fires some bullet
		_maxPlayTime -= _maxLastShootTime;
	}

	#region Callbacks
	public static void RegisterOnPlay(System.Action toAdd) => OnPlayRound += toAdd;
	public static void RegisterOnScore(System.Action toAdd) => OnScoreRound += toAdd;
	public static void UnregisterOnPlay(System.Action toRemove) => OnPlayRound -= toRemove;
	public static void UnregisterOnScore(System.Action toRemove) => OnScoreRound -= toRemove;
	#endregion

	// Launch a new wave / display scores or block bullets incoming
	private void SwitchPhase()
	{
		// Life cycle
		// Play => Last Shoot => Score => Play
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
				if (_maxRoundNumber <= _currentRound)
				{
					GameState.Win();
					break;
				}

				ChangePhase(RoundPhase.PLAY, _maxPlayTime);
				_currentRound++;
				OnPlayRound?.Invoke();
				break;

			default:
				Debug.LogError($"Unknow phase {_currentPhase}");
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