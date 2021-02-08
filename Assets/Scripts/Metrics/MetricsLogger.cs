using System.Collections.Generic;
using UnityEngine;

public class MetricsLogger : MonoBehaviour
{
	public static MetricsLogger Instance { get; private set; }

	[SerializeField] private bool _createANewFile = false;

	private string _metricsFileName = "";
	private float _timeInMsFromBeginning = 0.0f;                            // Horodatage

	// Frequency in register value
	private readonly Dictionary<string, float> _metricFrequency = new Dictionary<string, float>();
	private readonly Dictionary<string, float> _lastCall = new Dictionary<string, float>();

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError($"Two singletons of the same types {typeof(MetricsLogger)}.");
			Destroy(this);
		}
		Instance = this;

		InitializeFileName();
	}

	private void Update() => _timeInMsFromBeginning += Time.unscaledDeltaTime;

	// Add new key
	public void SetMetricFrequency(string key, float frequencyInSec)
	{
		if (string.IsNullOrEmpty(key) || frequencyInSec <= 0f) { return; }

		_metricFrequency[key] = frequencyInSec;
		_lastCall[key] = _timeInMsFromBeginning;
	}

	public void Log(string key, params object[] values)
	{
		if (string.IsNullOrEmpty(key) || values.Length <= 0) { return; }

		// Has a record frequency
		if (_metricFrequency.ContainsKey(key))
		{
			// Not the right time to save
			if (_timeInMsFromBeginning < _metricFrequency[key] + _lastCall[key]) { return; }

			_lastCall[key] = _timeInMsFromBeginning;
		}

		// Value
		string output = $"{_timeInMsFromBeginning}; {key}";

		foreach (var val in values)
			output += $"; {val}";

		output += "\n";

		FileManagement.Append(_metricsFileName, output);
	}

	private void InitializeFileName()
	{
		// Basic name
		_metricsFileName = FileNameConst.METRICS;

		// Add date time
		if (_createANewFile)
		{
			string date = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			_metricsFileName += date;
		}

		// Extension name
		_metricsFileName += ".csv";
	}
}
