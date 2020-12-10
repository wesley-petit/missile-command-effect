using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	[SerializeField] private float _destroyTime = 0.5f;

	public float DestroyTime
	{
		get => _destroyTime;
		set => _destroyTime = value;
	}

	private void Start() => Destroy(gameObject, _destroyTime);
}
