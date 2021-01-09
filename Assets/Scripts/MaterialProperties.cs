using UnityEngine;

// Change material and emissive
public class MaterialProperties : MonoBehaviour
{
	[SerializeField] private int _materialIndex = 0;

	private MeshRenderer _meshs = null;
	private Material[] _materials = null;

	private void Awake()
	{
		_meshs = GetComponent<MeshRenderer>();
		_materials = _meshs.materials;
	}

	public void ChangeMaterial(Material newMaterial)
	{
		// Set the new material on the GameObject
		_materials[_materialIndex] = newMaterial;
		_meshs.materials = _materials;
	}
}
