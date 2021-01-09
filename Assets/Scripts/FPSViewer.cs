using UnityEngine;
using TMPro;

// Broken class
public class FPSViewer : MonoBehaviour
{
	private const float WAIT_TIME = 1f;

	[SerializeField] private TMP_Text _fpsText = null;

	System.Collections.IEnumerator Start()
	{
		while (true)
		{
			if (!_fpsText) { yield return null; }

			_fpsText.text = $"{1 / Time.unscaledDeltaTime}";

			yield return new WaitForSeconds(WAIT_TIME);
		}
	}
}