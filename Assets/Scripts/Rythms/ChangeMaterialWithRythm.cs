using UnityEngine;
using System.Collections.Generic;

// Give color to change material and change in rythm
public abstract class ChangeMaterialWithRythm : MonoBehaviour
{
	[SerializeField] protected List<MaterialProperties> _changeMaterials = new List<MaterialProperties>();

	protected AudioSync AudioManager => AudioSync.Instance;

	protected virtual void Start()
	{
		if (_changeMaterials.Count <= 0)
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

	public void Add(MaterialProperties materialProperties)
	{
		if (!materialProperties) { return; }
		_changeMaterials.Add(materialProperties);
	}

	public void Remove(MaterialProperties materialProperties)
	{
		if (!materialProperties) { return; }
		_changeMaterials.Remove(materialProperties);
	}
}
