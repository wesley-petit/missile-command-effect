using UnityEngine;

// Give color to change material and change in rythm
public class ChangeMaterialWithRythm : MonoBehaviour
{
	[SerializeField] private ChangeMaterialProperties _changeMaterial = null;
	[SerializeField] private int[] _colorMotifs = new int[0];       // Contains index to change color
	[SerializeField] private Color[] _colors = new Color[0];        // Contains colors

	private AudioSync AudioManager => AudioSync.Instance;

	private int _index = 0;

	private void Start()
	{
		if (!_changeMaterial)
		{
			Debug.LogError($"Change Material is undefined in {name}.");
			return;
		}

		if (_colorMotifs.Length <= 0)
		{
			Debug.LogError($"Color Motifs is undefined in {name}.");
			return;
		}

		if (_colors.Length <= 0)
		{
			Debug.LogError($"Colors are undefined in {name}.");
			return;
		}
	}

	private void Update()
	{
		if (AudioManager.IsInPace)
		{
			ChangeInRythm(AudioManager.IsInStrongTime);
		}
	}

	private void ChangeInRythm(bool strongTime)
	{
		// Avoid empty array
		if (_colorMotifs.Length <= 0 || _colors.Length <= 0) { return; }

		// Reset motif index
		_index++;
		if (_colorMotifs.Length <= _index) { _index = 0; }

		// Color index
		int colorIndex = _colorMotifs[_index];
		if (_colors.Length <= colorIndex) { colorIndex = 0; }

		_changeMaterial?.ChangeColor(_colors[colorIndex]);

		// Give an emissive color in a strong time
		if (strongTime)
		{
			Debug.Log("Emissive");
		}
	}
}
