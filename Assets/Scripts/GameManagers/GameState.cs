// Manage Win or Lose Event
public static class GameState
{
	public static System.Action OnWinEvent = null;
	public static System.Action OnLoseEvent = null;

	// Win or lose
	public static bool EndGame { get; private set; } = false;

	private static bool IsLosing => _numberOfIntactBuilding <= 0;

	private static int _numberOfIntactBuilding = 0;

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

	public static void Reset() => EndGame = false;

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
		// If win and lose event are both invoke in the same time
		if (EndGame) { return; }

		EndGame = true;
		OnWinEvent?.Invoke();
	}
	#endregion

	private static void Lose()
	{
		// If win and lose event are both invoke in the same time
		if (EndGame) { return; }

		EndGame = true;
		OnLoseEvent?.Invoke();
	}
}