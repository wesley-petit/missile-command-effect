public class CheckBuildingDestroy : TutorialStepCheck
{
	[UnityEngine.SerializeField]
	private uint _maxBuildingDestroy = 1;                       // Max number to validate a step

	private uint _destroyNumber = 0;                            // Increase in each building explosion

	// Active check for a select steps
	public override void Select(Tutorial tutorial, TutorialSteps current)
	{
		base.Select(tutorial, current);

		foreach (var building in current.GetBuildings)
			building.OnDestroyBuilding += IncreaseDestroy;
	}

	public override void Deselect(Tutorial tutorial, TutorialSteps previous)
	{
		base.Deselect(tutorial, previous);

		foreach (var building in previous.GetBuildings)
			building.OnDestroyBuilding -= IncreaseDestroy;
	}

	private void IncreaseDestroy()
	{
		_destroyNumber++;

		if (_maxBuildingDestroy <= _destroyNumber && !_valid)
		{
			_valid = true;
			ValidStep();
		}
	}
}
