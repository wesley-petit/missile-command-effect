using UnityEngine;

public class PlayerCanon : MonoBehaviour
{
	[SerializeField] private CollidableBuilding _building = null;
	[SerializeField] private Transform _canon = null;               // Canon origin
	[SerializeField] private Transform _turretHeading = null;       // Change turret heading in the bullet direction
	[SerializeField] private float _addToYAngle = 90f;              // Rotation to angle

	private Vector3 _startRotation = new Vector3(-22f, 0f, 90f);

	#region Public fields
	public bool Input { get; set; }                                 // Fill with inputs
	public int Ammos { get; set; }

	public CollidableBuilding GetBuilding => _building;
	public Vector3 GetPosition => _canon.position;
	public bool CanShoot => Input && 0 < Ammos && _building.IsIntact; // Canon hiding or not
	#endregion

	private void Start() => _startRotation = _turretHeading.rotation.eulerAngles;

	public void ReduceAmmos() => Ammos--;

	public void RotateTurret(float angle) => _turretHeading.rotation = Quaternion.Euler(_startRotation.x, _addToYAngle - angle, _startRotation.z);
}