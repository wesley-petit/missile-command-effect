using UnityEngine;

// Give color to change material and change in rythm
public abstract class ChangeMaterialWithRythm : MonoBehaviour
{
	[SerializeField] protected MaterialProperties[] _changeMaterials = null;

	protected AudioSync AudioManager => AudioSync.Instance;

	protected virtual void Start()
	{
		if (_changeMaterials.Length <= 0)
		{
			Debug.LogError($"Change Material is undefined in {name}.");
			return;
		}
	}

	// Apply MAterial for each element
	protected void ApplyChange(Material newMat)
	{
		if (!newMat) { return; }

		foreach (var change in _changeMaterials)
		{
			change.ChangeMaterial(newMat);
		}
	}
}
