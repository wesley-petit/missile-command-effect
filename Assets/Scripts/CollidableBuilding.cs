using UnityEngine;

// A collidable or destroyable building
// Yes it's a funny class
public class CollidableBuilding : MonoBehaviour, ICollidable
{
    public GameObject destroyedVersion;

    public System.Action OnDestroyBuilding = null;
    public bool IsIntact => gameObject.activeSelf;

    private void Awake()
    {
        if (destroyedVersion)
        {
            destroyedVersion.SetActive(false);
        }
    }

    public void Hit() => Hide();

    public void Show() => gameObject.SetActive(true);

    public void Hide()
    {
        gameObject.SetActive(false);
        OnDestroyBuilding?.Invoke();

        if (destroyedVersion)
        {
            destroyedVersion.SetActive(true);
            destroyedVersion.transform.SetParent(null);
        }
    }

}
