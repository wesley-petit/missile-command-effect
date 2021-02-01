public class BuildingsInstance : TutorialInstance
{
	// Hide previous buildings and display current
	protected override void UpdateSteps(TutorialSteps current, TutorialSteps previous, uint currentIndex)
	{
		// Hide buildings
		if (previous.GetBuildings != null)
		{
			foreach (var building in previous.GetBuildings)
				building.Hide();
		}

		// Show destroy buildings
		foreach (var building in current.GetBuildings)
			building.Show();
	}
}
