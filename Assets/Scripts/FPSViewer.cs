using UnityEngine;
using TMPro;

public class FPSViewer : MonoBehaviour
{
	private const float WAIT_TIME = 1f;

	[SerializeField] private TMP_Text _fpsText = null;

	private int CurrentFPS => (int)(1f / Time.deltaTime);


	System.Collections.IEnumerator Start()
	{
		float timer = WAIT_TIME;

		while (true)
		{
			timer -= Time.deltaTime;

			// Relance le test
			if (timer < 0)
			{
				timer = WAIT_TIME;

				if (!_fpsText) { yield return null; }
				_fpsText.text = $"{CurrentFPS}";
			}
			yield return null;
		}
	}
}