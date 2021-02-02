using UnityEngine;

// Change round between play or score
public class RoundSystem : MonoBehaviour
{
	public static RoundSystem Instance { get; private set; }

	[SerializeField] private Round[] _rounds = new Round[0];        // Time / Difficulty for each round

	public bool IsInPlay { get; private set; }
	public Difficulty RoundDifficulty { get; private set; }                     // Current Difficulty

	private static System.Action OnChangeRound = null;                          // Callbacks to invoke a play round
	private readonly Timer _timer = new Timer();
	private uint _currentRound = 0;

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

	// Switch score to play round
	private void Start()
	{
		IsInPlay = true;
		SwitchPhase();
	}

	private void OnEnable()
	{
		_timer.Register(SwitchPhase);
		GameState.OnLoseEvent += BlockRound;
	}

	private void OnDisable()
	{
		_timer.Unregister(SwitchPhase);
		GameState.OnLoseEvent -= BlockRound;
	}

	private void Update()
	{
		if (!GameState.EndGame)
		{
			_timer.UpdateTimer();
		}
	}
	#endregion

	#region Callbacks
	public static void Register(System.Action add)
	{
		if (add == null) { return; }

		OnChangeRound += add;
	}

	public static void Unregister(System.Action remove)
	{
		if (remove == null) { return; }

		OnChangeRound -= remove;
	}
	#endregion

	// Launch a new wave
	private void SwitchPhase()
	{
		// Max round
		if (_rounds.Length <= _currentRound)
		{
			GameState.Win();
			BlockRound();
			return;
		}

		// Change time
		_timer.MaxTime = _rounds[_currentRound].Time;
		RoundDifficulty = _rounds[_currentRound].Difficulty;
		OnChangeRound?.Invoke();

		_currentRound++;
	}

	private void BlockRound() => IsInPlay = false;
}