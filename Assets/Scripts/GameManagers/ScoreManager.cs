using UnityEngine;

// Increase Score and manage bullet combos
public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance { get; private set; }

	[SerializeField] private int _bulletScoreToCombos = 30;         // Bullet score to add an score combos
	[SerializeField] private IntEvent OnIncreaseScore = null;       // Callbacks for the UI

	#region Fields
	public int BuildingModifier
	{
		get => _buildingModifier;
		set
		{
			if (value <= 0) { return; }

			_buildingModifier = value;
		}
	}                                  // Score to add in each iteration when a building is intact
	public int BulletModifier
	{
		get => _bulletModifier;
		set
		{
			if (value <= 0) { return; }

			_bulletModifier = value;
		}
	}                                   // Score to add in each iteration when a enemy bullet was destroyed

	private bool HasACombos => _bulletScoreToCombos < _bulletModifier;
	#endregion

	private int _currentScore = 0;
	private int _buildingModifier = 0;
	private int _bulletModifier = 0;
	private AudioSync _audioSync = null;                            // Increase the score in a strong time
	private RoundSystem _roundSystem = null;                        // Increase score only on a play round

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError($"Two singletons of the same types {typeof(ScoreManager)}.");
			Destroy(this);
		}
		Instance = this;
	}

	private void Start()
	{
		_audioSync = AudioSync.Instance;
		_roundSystem = RoundSystem.Instance;
	}

	private void Update()
	{
		if (_audioSync.IsInStrongTime && _roundSystem.IsInPlay)
		{
			IncreaseScore();
		}
	}

	private void IncreaseScore()
	{
		_currentScore += _buildingModifier;

		// Combos
		if (HasACombos)
		{
			int combosMultiplier = _bulletModifier / _bulletScoreToCombos;
			_bulletModifier *= combosMultiplier;
		}

		// Add score and reset bullet modifier for next increase
		_currentScore += _bulletModifier;
		_bulletModifier = 0;

		OnIncreaseScore?.Invoke(_currentScore);
	}
}
