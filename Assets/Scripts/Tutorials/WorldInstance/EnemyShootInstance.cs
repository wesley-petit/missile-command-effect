public class EnemyShootInstance : TutorialInstance
{
	[UnityEngine.SerializeField] private EnemyShoot _enemyShoot = null;         // Active enemy canon

	protected override void UpdateSteps(TutorialSteps current, TutorialSteps previous, uint currentIndex)
	{
		if (_enemyShoot)
		{
			_enemyShoot.enabled = current.CanShoot;

			// Change target
			if (current.CanShoot)
			{
				_enemyShoot.enabled = false;
				_enemyShoot.Targets.Clear();

				// Add buildings
				foreach (var building in current.GetBuildings)
					_enemyShoot.Targets.Add(building);

				_enemyShoot.SetSpeedToTargets();
				_enemyShoot.enabled = true;
			}
		}
	}
}
