using UnityEngine;

// A collidable or destroyable building
// Yes it's a funny class
public class CollidableBuilding : MonoBehaviour, ICollidable
{
	public GameObject destroyedVersion;

	public System.Action OnDestroyBuilding = null;
	public bool IsIntact => gameObject.activeSelf;

	public void Hit() => Hide();

	public void Show() => gameObject.SetActive(true);

	public void Hide()
	{
		gameObject.SetActive(false);
		OnDestroyBuilding?.Invoke();

		GameObject destruct_building = Instantiate(destroyedVersion, transform.position, transform.rotation);
		destruct_building.transform.SetParent(null, false);
	}

}
