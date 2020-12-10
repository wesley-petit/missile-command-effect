using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] private SelfDestruct _selfDestruct = null;
	[SerializeField] private Animator _animator = null;

	private void Start()
	{
		if (!_animator)
			return;

		if (!_selfDestruct)
			return;

		_selfDestruct.DestroyTime = _animator.GetCurrentAnimatorClipInfo(0).Length;
	}
}
