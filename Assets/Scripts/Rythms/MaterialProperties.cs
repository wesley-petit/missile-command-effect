using UnityEngine;

// Change material and emissive
public class MaterialProperties : MonoBehaviour
{
	[SerializeField] protected int _materialIndex = 0;
	[SerializeField] protected MeshRenderer _meshs = null;

	// Use with a pool system to allow a dynamic change material
	public ChangeMaterialWithRythm ChangeMaterialWithRythm { get; set; }

	private Material[] _materials = null;

	private void Awake() => _materials = _meshs.materials;

	// Register to a change rythm
	#region Register with change rythm
	private void OnEnable()
	{
		if (ChangeMaterialWithRythm)
		{
			ChangeMaterialWithRythm.Add(this);
		}
	}

	private void OnDisable()
	{
		if (ChangeMaterialWithRythm)
		{
			ChangeMaterialWithRythm.Remove(this);
		}
	}
	#endregion

	public void ChangeMaterial(Material newMaterial)
	{
		// Set the new material on the GameObject
		_materials[_materialIndex] = newMaterial;
		_meshs.materials = _materials;
	}
}
