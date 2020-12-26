// Score for a building
[System.Serializable]
public struct ScoreBuilding
{
	[UnityEngine.SerializeField] private CollidableBuilding _collidableBuilding;
	[UnityEngine.SerializeField] private ushort _score;

	public CollidableBuilding Building => _collidableBuilding;
	public ushort Score => _score;

	public ScoreBuilding(CollidableBuilding collidableBuilding, ushort score)
	{
		_collidableBuilding = collidableBuilding;
		_score = score;
	}
}