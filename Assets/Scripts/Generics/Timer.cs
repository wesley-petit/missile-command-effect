// Create a generic timer common in most cases
public class Timer
{
	public float MaxTime { get; set; }                              // Max Time to launch a event

	private System.Action OnTimerEnd;                               // Event To Call
	private float _time = 0f;

	public void UpdateTimer()
	{
		_time += UnityEngine.Time.deltaTime;

		if (MaxTime < _time)
		{
			_time -= MaxTime;
			TimerEnd();
		}
	}

	#region Callbacks
	public void Register(System.Action toAdd) => OnTimerEnd += toAdd;

	public void Unregister(System.Action toRemove) => OnTimerEnd -= toRemove;

	private void TimerEnd() => OnTimerEnd?.Invoke();
	#endregion
}