using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : CharacterShoot
{
	[SerializeField]
	private List<Transform> _targets                                // Target can be turret or building
		= new List<Transform>();
	[SerializeField]
	protected Transform[] _shootCanons                              // Shoot Canon / Shoot Origin, in the same order (left to right) as the inputs
		= new Transform[0];

	private RandomElement<Transform> _targetRandom                  // Choose random Target 
		= new RandomElement<Transform>();
	private RandomElement<Transform> _canonRandom                   // Choose random Canon 
		= new RandomElement<Transform>();

	#region Unity Methods
	protected override void Start()
	{
		base.Start();

		if (_shootCanons.Length <= 0)
		{
			Debug.LogError($"Shoot Canon are undefined in {gameObject.name}.");
			return;
		}

		if (_targets.Count <= 0)
		{
			Debug.LogError($"Targets are undefined in {gameObject.name}.");
			return;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			ShootTurret();
	}
	#endregion

	protected override void ShootTurret()
	{
		Transform target = _targetRandom.Choose(_targets.ToArray());
		Transform canon = _canonRandom.Choose(_shootCanons);
		_turret.Shoot(canon, canon.position, target.position);
	}
}