using UnityEngine;

public class InputMetrics : MonoBehaviour
{
	public uint[] Shoot { get; private set; } = { 0, 0 };

	private InputHandler _inputs = null;

	private void Start() => _inputs = gameObject.AddComponent<InputHandler>();

	private void FixedUpdate()
	{
		if (_inputs.LeftShoot)
		{
			Shoot[0]++;
		}

		if (_inputs.RightShoot)
		{
			Shoot[1]++;
		}
	}

	public void ResetCount() => Shoot = new uint[2];
}
