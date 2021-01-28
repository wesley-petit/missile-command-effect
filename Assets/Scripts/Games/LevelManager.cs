using UnityEngine;

// Give to other Manager or controllers some
// buildings references => act as a main function
public class LevelManager : MonoBehaviour
{
	[SerializeField]
	private ScoreBuilding[] _scoreBuildings =                       // Buildings which add a score
		new ScoreBuilding[0];

	private ScoreManager Score => ScoreManager.Instance;

	#region Unity Methods

	#region Callbacks
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
	#endregion

	private void Start()
	{
		if (_scoreBuildings.Length <= 0)
		{
			Debug.LogError($"Buildings are undefined in {name}.");
			return;
		}

		StartManagers();
	}
	#endregion

	private void StartManagers()
	{
		ushort intactBuilding = 0;
		ushort currentScore = 0;

		// Set Building Score an intact building
		foreach (var oneScoreBuilding in _scoreBuildings)
		{
			if (!oneScoreBuilding.Building) { continue; }
			// To know if a building was destroy or not
			if (!oneScoreBuilding.Building.IsIntact) { continue; }

			intactBuilding++;
			currentScore += oneScoreBuilding.Score;
		}

		GameState.Reset();
		GameState.SetGameState(intactBuilding);
		// Add the correct score for each building
		Score.BuildingModifier = currentScore;
	}

	// Refresh Manager if a buildings has been destroyed
	private void RefreshManager()
	{
		// Remove one building
		GameState.ReduceNumberBuilding();

		// Refresh Score
		ushort currentScore = 0;
		foreach (var oneScoreBuilding in _scoreBuildings)
		{
			if (!oneScoreBuilding.Building.IsIntact) { continue; }

			currentScore += oneScoreBuilding.Score;
		}
		Score.BuildingModifier = currentScore;
	}
}
