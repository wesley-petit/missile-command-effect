using UnityEngine;

[System.Serializable]
public struct PlayerCanon
{
	[SerializeField] private CollidableBuilding _canon;

	public bool Input { get; set; }                                 // Fill with inputs
	public int Ammos { get; set; }

	public CollidableBuilding GetCanon => _canon;
	public Transform GetTransform => _canon.transform;
	public Vector3 GetPosition => _canon.transform.position;
	public bool CanShoot => Input && 0 < Ammos && _canon.gameObject.activeSelf; // Canon hiding or not

	public void ReduceAmmos() => Ammos--;
}