// Define a basic round
[System.Serializable]
public struct Round
{
	[UnityEngine.SerializeField] private float _time;
	[UnityEngine.SerializeField] private Difficulty _difficulty;

	public float Time => _time;
	public Difficulty Difficulty => _difficulty;
}

// Round difficulty
public enum Difficulty { WEAK, STRONG }