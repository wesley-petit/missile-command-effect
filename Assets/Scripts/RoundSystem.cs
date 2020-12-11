using UnityEngine;

public class RoundSystem : MonoBehaviour
{
	[SerializeField] private int _maxPlayTime = 70;
	[SerializeField] private int _maxScoreTime = 10;
	[SerializeField] private EnemyShoot _enemyShoot = null;         // Stop turrets in a score time
	[SerializeField] private PlayerShoot _playerShoot = null;       // Player shoot to reset ammos

	private Timer _timer = new Timer();
	private bool _play = true;

	#region Unity Methods
	private void Start()
	{
		if (!_enemyShoot)
		{
			Debug.LogError("Enemy Shoot is undefined");
			return;
		}

		if (!_playerShoot)
		{
			Debug.LogError("Player Shoot is undefined");
			return;
		}

		_timer.MaxTime = _maxPlayTime;
		SetEnemyState(true);
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
			_timer.MaxTime = _maxPlayTime;
			SetEnemyState(true);
			_playerShoot?.ResetCanon();
		}
		else
		{
			_timer.MaxTime = _maxScoreTime;
			SetEnemyState(false);
		}
	}

	// Enable or disable enemy
	private void SetEnemyState(bool shootState)
	{
		if (!_enemyShoot)
			return;

		_enemyShoot.IsInPlayTime = shootState;
	}
	#endregion
}