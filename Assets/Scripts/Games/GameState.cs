// Manage Win or Lose Event
public static class GameState
{
	public static System.Action OnWinEvent = null;
	public static System.Action OnLoseEvent = null;

	public static bool EndGame { get; private set; } = false;       // Win or lose
	private static bool LosingCondition => _numberOfIntactBuilding <= 0;

	private static ushort _numberOfIntactBuilding = 0;

	#region Public Methods
	public static void Reset() => EndGame = false;

	// Define the number of intact building to know if the player lose or win
	public static void SetGameState(ushort intactBuildings)
	{
		_numberOfIntactBuilding = intactBuildings;

		if (LosingCondition)
		{
			Lose();
		}
	}
	public static void ReduceNumberBuilding()
	{
		_numberOfIntactBuilding--;

		if (LosingCondition)
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