// Hide / Display canons
[UnityEngine.RequireComponent(typeof(Tutorial))]
public class PlayerShootInstance : TutorialInstance
{
	[UnityEngine.SerializeField] private PlayerShoot _playerShoot = null;       // Change player canon and reset ammos

	// Change Canon
	// Disable shoot if it's empty
	protected override void UpdateSteps(TutorialSteps current, TutorialSteps previous, uint currentIndex)
	{
		if (!_playerShoot) { return; }

		// Disable and hide
		_playerShoot.enabled = false;

		// Hide buildings
		foreach (var canon in _playerShoot.PlayerCanons)
		{
			canon.GetBuilding.Hide();
		}

		// Change canon and reset
		if (current.GetPlayerCanons.Length != 0)
		{
			_playerShoot.PlayerCanons = current.GetPlayerCanons;
			_playerShoot.ResetCanon();
			_playerShoot.enabled = true;
		}
	}
}
