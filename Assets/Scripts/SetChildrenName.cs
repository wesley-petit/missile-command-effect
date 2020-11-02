using UnityEngine;

// Utilise le nom d'un gameObject pour définir celui des enfants
public class SetChildrenName : MonoBehaviour
{
	[SerializeField] private string _childrenName = "";

	private void OnValidate() => SetName();

	[ContextMenu("Set Children Name", false, 0)]
	public void SetName()
	{
		Transform parent = transform;

		if (_childrenName.CompareTo("") == 0)
			_childrenName = parent.name;

		for (int i = 0; i < transform.childCount; i++)
			parent.GetChild(i).name = $"{_childrenName}_{i}";
	}
}
