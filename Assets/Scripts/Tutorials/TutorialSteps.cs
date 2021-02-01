using UnityEngine;

[System.Serializable]
public struct TutorialSteps
{
	[SerializeField] private string _tutorialText;                  // Explain text
	[SerializeField]
	private CollidableBuilding[] _collidableBuildings;              // Buildings to display
	[SerializeField] private PlayerCanon[] _playerCanons;           // Player Canons to display / Empty => block shoot
	[SerializeField] private TutorialStepCheck _stepCheck;          // Valide a step
	[SerializeField] private bool _canShoot;                        // Enemy can shoot or not

	public string GetTutorialText => _tutorialText;
	public CollidableBuilding[] GetBuildings => _collidableBuildings;
	public PlayerCanon[] GetPlayerCanons => _playerCanons;
	public TutorialStepCheck GetStepCheck => _stepCheck;
	public bool CanShoot => _canShoot;
}
