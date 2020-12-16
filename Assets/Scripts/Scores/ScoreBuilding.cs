// Score for a building
[System.Serializable]
public struct ScoreBuilding
{
	[UnityEngine.SerializeField] private CollidableBuilding _collidableBuilding;
	[UnityEngine.SerializeField] private int _score;

	public CollidableBuilding Building => _collidableBuilding;
	public int Score => _score;

	public ScoreBuilding(CollidableBuilding collidableBuilding, int score)
	{
		_collidableBuilding = collidableBuilding;
		_score = score;
	}
}