using UnityEngine;

// Change children name with parent name
public class SetChildrenName : MonoBehaviour
{
	[SerializeField] private string _childrenName = "";

	private void OnValidate() => SetName();

	[ContextMenu("Set Children Name", false, 0)]
	public void SetName()
	{
		Transform parent = transform;

		if (string.IsNullOrEmpty(_childrenName))
			_childrenName = parent.name;

		for (int i = 0; i < transform.childCount; i++)
			parent.GetChild(i).name = $"{_childrenName}_{i}";
	}
}
