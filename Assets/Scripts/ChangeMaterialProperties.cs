using UnityEngine;

// Change material and emissive
public class ChangeMaterialProperties : MonoBehaviour
{
	[SerializeField] private int _materialIndex = 0;
	private Material _objectMaterial = null;

	private void Awake() => _objectMaterial = GetComponent<MeshRenderer>().materials[_materialIndex];

	public void ChangeColor(Color color) => _objectMaterial.SetColor("_Color", color);
}
