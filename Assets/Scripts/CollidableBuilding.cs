using UnityEngine;

public class CollidableBuilding : MonoBehaviour, ICollidable
{
	public void Hit() => Hide();

	public void Show() => gameObject.SetActive(true);

	public void Hide() => gameObject.SetActive(false);
}
