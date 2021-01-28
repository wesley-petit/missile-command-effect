using UnityEngine;

// Change building material follow a motif
public class ChangeBuildingMaterialWithRythm : ChangeMaterialWithRythm
{
	[SerializeField] private int[] _matMotifs = new int[0];         // Contains index to change material
	[SerializeField] private Material[] _mats = new Material[0];    // Contains materials motifs

	private int _index = 0;

	protected override void Start()
	{
		base.Start();

		if (_matMotifs.Length <= 0)
		{
			Debug.LogError($"Material Motifs is undefined in {name}.");
			return;
		}

		if (_mats.Length <= 0)
		{
			Debug.LogError($"Materials are undefined in {name}.");
			return;
		}
	}

	private void Update()
	{
		if (AudioManager.IsInPace)
		{
			ApplyChange(GetMaterial());
		}
	}

	// Get Material with index
	private Material GetMaterial()
	{
		// Material with index
		// Avoid empty array
		if (_matMotifs.Length <= 0 || _mats.Length <= 0) { return null; }

		// Reset motif index
		_index++;
		if (_matMotifs.Length <= _index) { _index = 0; }

		// Material index
		int materialIndex = _matMotifs[_index];
		if (_mats.Length <= materialIndex) { materialIndex = 0; }

		return _mats[materialIndex];
	}
}
