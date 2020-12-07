using UnityEngine;

public class RoundSystem : MonoBehaviour
{
	[SerializeField] private int _maxPlayTime = 70;
	[SerializeField] private int _maxScoreTime = 10;
	[SerializeField]
	private TurretController[] _turrets = new TurretController[0];  // To stop turrets in a score time / Not the shoot to keep it synchronize
	[SerializeField] private PlayerShoot _playerShoot = null;        // Player shoot to reset ammos

	private Timer _timer = new Timer();
	private bool _play = true;

	#region Unity Methods
	private void Start()
	{
		_timer.MaxTime = _maxPlayTime;
		SetTurretState(true);
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
			Debug.Log("Play");
			_timer.MaxTime = _maxPlayTime;
			SetTurretState(true);
			_playerShoot?.ResetAmmos();
		}
		else
		{
			Debug.Log("Stop");
			_timer.MaxTime = _maxScoreTime;
			SetTurretState(false);
		}
	}

	// Enable or disable all turrets
	private void SetTurretState(bool shootState)
	{
		foreach (var turret in _turrets)
		{
			turret.IsPlayTime = shootState;
		}
	}
	#endregion
}