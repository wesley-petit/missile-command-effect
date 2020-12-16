using UnityEngine;

// Give to other Manager or controllers some
// buildings references => act as a main function
public class GameManager : MonoBehaviour
{
	[SerializeField]
	private ScoreBuilding[] _scoreBuildings =                       // Buildings which add a score
		new ScoreBuilding[0];

	#region Unity Methods
	// Register and unregister to callbacks
	private void OnEnable()
	{
		foreach (var oneScoreBuilding in _scoreBuildings)
		{
			if (!oneScoreBuilding.Building) { continue; }
			oneScoreBuilding.Building.OnDestroyBuilding += RefreshManager;
		}
	}

	private void OnDisable()
	{
		foreach (var oneScoreBuilding in _scoreBuildings)
		{
			if (!oneScoreBuilding.Building) { continue; }
			oneScoreBuilding.Building.OnDestroyBuilding -= RefreshManager;
		}
	}

	private void Start()
	{
		if (_scoreBuildings.Length <= 0)
		{
			Debug.LogError("Buildings are undefined.");
			return;
		}

		StartManagers();
	}
	#endregion

	private void StartManagers()
	{
		int intactBuilding = 0;
		int currentScore = 0;

		// Set Building Score an intact building
		foreach (var oneScoreBuilding in _scoreBuildings)
		{
			if (!oneScoreBuilding.Building) { continue; }
			// To know if a building was destroy or not
			if (!oneScoreBuilding.Building.IsIntact) { continue; }

			intactBuilding++;
			currentScore += oneScoreBuilding.Score;
		}

		GameStateManager.SetGameState(intactBuilding);
		// Add the correct score for each building
		ScoreManager.Instance.BuildingModifier = currentScore;
	}

	// Refresh Manager if a buildings has been destroyed
	private void RefreshManager()
	{
		// Remove one building
		GameStateManager.ReduceNumberBuilding();

		// Refresh Score
		int currentScore = 0;
		foreach (var oneScoreBuilding in _scoreBuildings)
		{
			if (!oneScoreBuilding.Building.IsIntact) { continue; }

			currentScore += oneScoreBuilding.Score;
		}
		ScoreManager.Instance.BuildingModifier = currentScore;
	}
}
