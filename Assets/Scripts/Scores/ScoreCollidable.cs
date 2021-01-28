using UnityEngine;

// Increase score if it collides with something
public class ScoreCollidable : MonoBehaviour, ICollidable
{
	[SerializeField] private ushort _addScore = 10;

	public void Hit() => ScoreManager.Instance.BulletModifier += _addScore;
}
