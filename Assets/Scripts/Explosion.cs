
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] private SelfDestruct _selfDestruct = null;
	[SerializeField] private Animator _animator = null;

	private void Awake()
	{
		if (!_animator)
			return;

		if (!_selfDestruct)
			return;

		// Set destroy time with clip length
		_selfDestruct.DestroyTime = _animator.GetCurrentAnimatorClipInfo(0).Length;
	}
}
