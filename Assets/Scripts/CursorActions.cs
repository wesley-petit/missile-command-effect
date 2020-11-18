using UnityEngine;

public class CursorActions : MonoBehaviour
{
    [SerializeField] private Camera myCamera = null;

    private void Update()
    {
      Vector3 temp = Input.mousePosition;
      temp.z = 1f;
      transform.position = myCamera.ScreenToWorldPoint(temp);
    }
}
