// Struct for each player canon with a selected inputs
[System.Serializable]
public struct PlayerCanon
{
	[UnityEngine.SerializeField] private CollidableBuilding _canon;

	public bool Input { get; set; }                                 // Fill with inputs
	public int Ammos { get; set; }

	public CollidableBuilding GetCanon => _canon;
	public UnityEngine.Transform GetTransform => _canon.transform;
	public UnityEngine.Vector3 GetPosition => _canon.transform.position;
	public bool CanShoot => Input && 0 < Ammos && _canon.IsIntact; // Canon hiding or not

	public void ReduceAmmos() => Ammos--;
}