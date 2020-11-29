using UnityEngine;

public abstract class CharacterShoot : MonoBehaviour
{
	[SerializeField] protected TurretController _turret = null;

	// Verification Array
	protected virtual void Start()
	{
		if (!_turret)
		{
			Debug.LogError($"Turret is undefined in {gameObject.name}.");
			return;
		}
	}

	protected abstract void ShootTurret();
}
