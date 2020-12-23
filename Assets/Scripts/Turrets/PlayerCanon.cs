using UnityEngine;

// Struct for each player canon with a selected inputs
[System.Serializable]
public struct PlayerCanon
{
	[SerializeField] private CollidableBuilding _canon;
	[SerializeField] private Transform _turretHeading;				// Change turret heading in the bullet direction 

	public bool Input { get; set; }                                 // Fill with inputs
	public int Ammos { get; set; }

	public CollidableBuilding GetCanon => _canon;
	public Transform GetTransform => _canon.transform;
	public Transform GetHeading => _turretHeading;
	public Vector3 GetPosition => _canon.transform.position;
	public bool CanShoot => Input && 0 < Ammos && _canon.IsIntact; // Canon hiding or not

	public void ReduceAmmos() => Ammos--;
}