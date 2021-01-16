using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

// Object and turret rotate
public class PlayerCanon : MonoBehaviour
{
	[SerializeField] private CollidableBuilding _building = null;
	[SerializeField] private Transform _canon = null;               // Canon origin
	[SerializeField] private Transform _turretHeading = null;       // Change turret heading in the bullet direction
	[SerializeField] private float _offsetAngleY = 90f;             // Rotation to angle
	[SerializeField] private Slider _ammoCounter = null;
	[SerializeField] private GameObject _SpawnerFill = null;
	[SerializeField] private GameObject _ammoBye = null;

	private Vector3 _startRotation = new Vector3(-22f, 0f, 90f);

	#region Public fields
	public bool Input { get; set; }                                 // Fill with inputs
	public uint Ammos { get; private set; }

	public CollidableBuilding GetBuilding => _building;
	public Vector3 GetPosition => _canon.position;
	public bool CanShoot => Input && 0 < Ammos && _building.IsIntact; // Canon hiding or not
	#endregion

	private void Start()
	{
		DOTween.Init();
		_startRotation = _turretHeading.rotation.eulerAngles;
	}

	public void MaxAmmos(uint maxAmmos)
	{
		Ammos = maxAmmos;
		if (_ammoCounter)
		{
			_ammoCounter.maxValue = maxAmmos;
			_ammoCounter.value = maxAmmos;
		}
	}

	public void ReduceAmmos()
	{
		Ammos--;
		RefreshUI();
	}

	public void RotateTurret(float angle) => _turretHeading.rotation = Quaternion.Euler(_startRotation.x, _offsetAngleY - angle, _startRotation.z);

	private void RefreshUI()
	{
		if (!_ammoBye) { return; }
		if (!_ammoCounter) { return; }
		if (!_ammoCounter) { return; }

		GameObject ammoTemp = Instantiate(_ammoBye, new Vector3(_SpawnerFill.transform.position.x, _SpawnerFill.transform.position.y, 0), Quaternion.identity);
		ammoTemp.transform.DOMove(new Vector3(ammoTemp.transform.position.x, -3f, 0), 0.4f);
		ammoTemp.transform.DOScale(new Vector3(ammoTemp.transform.localScale.x, 0.1f, 0.1f), 0.5f);
		StartCoroutine(DestroyAmmoBye(ammoTemp));
		_ammoCounter.value = Ammos;
	}

	private IEnumerator DestroyAmmoBye(GameObject TempAmmo)
	{
		yield return new WaitForSeconds(0.4f);
		Destroy(TempAmmo);
	}

}
