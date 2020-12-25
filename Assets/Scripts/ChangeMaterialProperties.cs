using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change material and emissive
public class ChangeMaterialProperties : MonoBehaviour
{
	private Material _objectMaterial = null;

	private void Awake() => _objectMaterial = GetComponent<MeshRenderer>().materials[0];

	public void ChangeColor(Color color) => _objectMaterial.SetColor("_Color", color);
}
