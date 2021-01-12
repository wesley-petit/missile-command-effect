using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCanon : MonoBehaviour
{
	[SerializeField] private CollidableBuilding _building = null;
	[SerializeField] private Transform _canon = null;               // Canon origin
	[SerializeField] private Transform _turretHeading = null;       // Change turret heading in the bullet direction
	[SerializeField] private float _offsetAngleY = 90f;             // Rotation to angle
	[SerializeField] private Slider _ammoCounter = null;
	[SerializeField] private GameObject _ammoBye = null;

	private Vector3 _startRotation = new Vector3(-22f, 0f, 90f);

	#region Public fields
	public bool Input { get; set; }                                 // Fill with inputs
	public uint Ammos { get; set; }

	public CollidableBuilding GetBuilding => _building;
	public Vector3 GetPosition => _canon.position;
	public bool CanShoot => Input && 0 < Ammos && _building.IsIntact; // Canon hiding or not
	#endregion

	private void Start()
	{
		DOTween.Init();
		_startRotation = _turretHeading.rotation.eulerAngles;
	}

	public void ReduceAmmos()
	{
		Ammos--;
		RefreshUI();
	}

	public void RefreshUI()
	{
		if (!_ammoBye) { return; }
		if (!_ammoCounter) { return; }

		GameObject ammoTemp = Instantiate(_ammoBye, new Vector3(_ammoCounter.transform.position.x / 1.8f, -3.5f, 0), Quaternion.identity);
		ammoTemp.transform.DOMove(new Vector3(ammoTemp.transform.position.x, 1.5f, 0), 0.5f);
		ammoTemp.transform.DOScale(new Vector3(ammoTemp.transform.localScale.x, 0, 0), 1);
		StartCoroutine(DestroyAmmoBye(ammoTemp));
		RefreshUI();
		_ammoCounter.value = Ammos;
	}

	IEnumerator DestroyAmmoBye(GameObject TempAmmo)
	{
		yield return new WaitForSeconds(1);
		Destroy(TempAmmo);
	}

	public void RotateTurret(float angle) => _turretHeading.rotation = Quaternion.Euler(_startRotation.x, _offsetAngleY - angle, _startRotation.z);
}