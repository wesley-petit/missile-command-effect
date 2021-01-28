using UnityEngine;
using TMPro;
using DG.Tweening;
// Increase Score and manage bullet combos
public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance { get; private set; }

	[SerializeField] private ushort _bulletScoreToCombos = 30;      // Bullet score to add an score combos
	[SerializeField] private IntEvent OnIncreaseScore = null;       // Callbacks for the UI
	private GameObject _scoreTxt;
	#region Fields
	public ushort BuildingModifier
	{
		get => _buildingModifier;
		set
		{
			if (value <= 0) { return; }

			_buildingModifier = value;
		}
	}                               // Score to add in each iteration when a building is intact
	public ushort BulletModifier
	{
		get => _bulletModifier;
		set
		{
			if (value <= 0) { return; }

			_bulletModifier = value;
		}
	}                                 // Score to add in each iteration when a enemy bullet was destroyed

	private bool HasACombos => _bulletScoreToCombos < _bulletModifier;
	#endregion

	private ushort _currentScore = 0;
	private ushort _buildingModifier = 0;
	private ushort _bulletModifier = 0;
	private AudioSync _audioSync = null;
	private RoundSystem _roundSystem = null;

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
        DOTween.Init();
		_audioSync = AudioSync.Instance;
		_roundSystem = RoundSystem.Instance;
		_scoreTxt = GameObject.Find("Scored");
	}

	private void Update()
	{
		// Increase the score in a strong time and in a play round
		if (_audioSync.IsInStrongTime && _roundSystem.IsInPlay)
		{
            IncreaseScore();
		}
	}

	private void IncreaseScore()
	{
		_currentScore += _buildingModifier;
		_scoreTxt.GetComponent<TextMeshProUGUI>().text=_currentScore.ToString();
		// Combos
		if (HasACombos)
		{
			ushort combosMultiplier = (ushort)(_bulletModifier / _bulletScoreToCombos);
			_bulletModifier *= combosMultiplier;
		}
		Debug.Log(_currentScore);
		// Add score and reset bullet modifier for next increase
		_currentScore += _bulletModifier;
		_bulletModifier = 0;
		_scoreTxt.transform.DOPunchScale (new Vector3 (0.15f, 0.15f, 0.15f), .20f).SetLoops(1);
		OnIncreaseScore?.Invoke(_currentScore);
	}
}
