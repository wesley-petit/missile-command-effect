using UnityEngine;

public class PlaceholderMetrics : MonoBehaviour
{
	private void Start()
	{
		MetricsLogger.Instance.Log(MetricsKey.PLAYER_POS, 1, 2, "grass");
		MetricsLogger.Instance.Log(MetricsKey.PLAYER_WEAPON_CHANGE, "Knife");
		MetricsLogger.Instance.SetMetricFrequency(MetricsKey.PLAYER_POS, 2f);
	}

	private void Update() => MetricsLogger.Instance.Log(MetricsKey.PLAYER_POS, "1");
}
