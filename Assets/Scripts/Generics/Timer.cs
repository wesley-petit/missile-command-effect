using System;
using UnityEngine;

[Serializable]
public class Timer
{
	public float MaxTime { get; set; }								// Max Time to launch a event

	private Action OnTimerEnd;										// Event To Call
	private float _time = 0f;

	public void UpdateTimer()
	{
		_time += Time.deltaTime;

		if (MaxTime < _time)
		{
			_time -= MaxTime;
			TimerEnd();	
		}
	}

	#region Callbacks
	public void Register(Action toAdd) => OnTimerEnd += toAdd;

	public void Unregister(Action toRemove) => OnTimerEnd -= toRemove;

	private void TimerEnd() => OnTimerEnd?.Invoke();
	#endregion
}