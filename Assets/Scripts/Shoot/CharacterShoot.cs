using UnityEngine;

public abstract class CharacterShoot : MonoBehaviour
{
	[SerializeField] protected TurretController _turret = null;
	[SerializeField]
	protected Transform[] _shootCanons								// Shoot Canon / Shoot Origin, in the same order (left to right) as the inputs
		= new Transform[0];

	// Verification Array
	protected virtual void Start()
	{
#if UNITY_EDITOR
		if (_shootCanons.Length <= 0)
		{
			Debug.LogError($"Shoot Canon are undefined in {gameObject.name}.");
			return;
		}

		if (!_turret)
		{
			Debug.LogError($"Turret is undefined in {gameObject.name}.");
			return;
		}
#endif
	}

	protected abstract void ShootTurret();
}
