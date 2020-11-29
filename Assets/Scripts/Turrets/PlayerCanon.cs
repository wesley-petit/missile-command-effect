using UnityEngine;

[System.Serializable]
public struct PlayerCanon
{
	[SerializeField] private Transform _canon;

	public bool Input { get; set; }                                 // Fill with inputs
	public int Ammos { get; set; }

	public Transform GetCanon => _canon;
	public Vector3 GetPosition => _canon.position;
	public bool CanShoot => Input && 0 < Ammos;

	public void ReduceAmmos() => Ammos--;
}