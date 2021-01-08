using UnityEngine;

// Target for player shoot
[RequireComponent(typeof(XRMovement))]
public class Cursor : MonoBehaviour
{
	[SerializeField]
	private MovementType _typeSelected = MovementType.XR;           // Movement type by default
	[SerializeField]
	private ReticleType[] _reticles = new ReticleType[0];           // Reticles by type to hide other reticles

	private CursorMovement _movementSelected = null;

	[System.Serializable]
	private struct ReticleType                                      // Each movement type has his own reticle
	{
		[SerializeField] private MovementType _movementType;
		[SerializeField] private GameObject[] _reticles;

		public MovementType GetMovementType => _movementType;
		public void Hide()
		{
			foreach (var reticle in _reticles)
			{
				reticle.SetActive(false);
			}
		}
	}

	private void Start()
	{
		SelectMovement();
		HideOtherReticles();
	}

	// Get the component with a selected movement type
	private void SelectMovement()
	{
		CursorMovement[] cursorMovements = GetComponents<CursorMovement>();

		// Seach the precious one
		foreach (var cursor in cursorMovements)
		{
			if (cursor.GetMovementType == _typeSelected)
			{
				_movementSelected = cursor;
				_movementSelected.InitMovement();
				break;
			}
		}

		if (!_movementSelected)
		{
			Debug.LogError($"Movement Selected is undefined, because there is no component with movement type {_typeSelected}");
			return;
		}
	}

	// We hide others than the selected
	private void HideOtherReticles()
	{
		foreach (var reticle in _reticles)
		{
			if (reticle.GetMovementType == _typeSelected) { continue; }
			reticle.Hide();
		}
	}

	private void Update() => _movementSelected.UpdateMovement();
}
