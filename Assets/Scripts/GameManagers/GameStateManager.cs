using UnityEngine;

// Manage Win or Lose Event
public static class GameStateManager
{
	public static System.Action OnWinEvent = null;
	public static System.Action OnLoseEvent = null;

	private static int _numberOfIntactBuilding = 0;

	private static bool IsLosing => _numberOfIntactBuilding <= 0;

	#region Public Methods
	// Define the number of intact building to know if the player lose or win
	public static void SetGameState(int intactBuildings)
	{
		_numberOfIntactBuilding = intactBuildings;

		if (IsLosing)
		{
			Lose();
		}
	}

	public static void ReduceNumberBuilding()
	{
		_numberOfIntactBuilding--;

		if (IsLosing)
		{
			Lose();
		}
	}

	public static void Win()
	{
		OnWinEvent?.Invoke();
		Debug.Log("Win");
	}
	#endregion

	private static void Lose()
	{
		OnLoseEvent?.Invoke();
		Debug.Log("Lose");
	}
}