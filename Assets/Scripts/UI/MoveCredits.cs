using UnityEngine;

public class MoveCredits : MonoBehaviour
{
    [Range(0f,2f)]
    [SerializeField] private float _speed = 0.5f;
    
    private void Update()
    {
        float movement = _speed * Time.deltaTime;
        transform.position += new Vector3(0f,movement, movement);
    }
}
