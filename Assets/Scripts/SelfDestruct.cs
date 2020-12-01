using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	[SerializeField] private float _destroyTime = 1f;

	public float DestroyTime
	{
		get => _destroyTime;
		set => _destroyTime = value;
	}

	private void Start() => Destroy(gameObject, _destroyTime);
}
