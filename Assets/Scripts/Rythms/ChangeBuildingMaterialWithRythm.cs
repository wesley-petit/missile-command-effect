using UnityEngine;

// Change building material follow a motif
public class ChangeBuildingMaterialWithRythm : ChangeMaterialWithRythm
{
    [SerializeField] private Material _normalMaterial = null;
    [SerializeField] private Material _epicMaterial = null;         // Epic material on each building

    private int _epicIndex = 0;                                     // Index in current material
    private MaterialProperties _currentEpic = null;                 // Current material with epic material

    protected override void Start()
    {
        base.Start();

        if (!_normalMaterial)
        {
            Debug.LogError($"Normal Material is undefined in {name}.");
            return;
        }

        if (!_epicMaterial)
        {
            Debug.LogError($"Epic Material is undefined in {name}.");
            return;
        }

        ApplyChange(_normalMaterial);
        ApplyChangeOnEpic();
    }

    private void Update()
    {
        if (AudioManager.IsInPace)
        {
            ApplyChangeOnEpic();
        }
    }

    // Apply epic material
    private void ApplyChangeOnEpic()
    {
        if (!_epicMaterial) { return; }
        if (!_normalMaterial) { return; }
        if (_changeMaterials.Count <= 0) { return; }

        if (_currentEpic && _currentEpic.isActiveAndEnabled)
        {
            _currentEpic.ChangeMaterial(_normalMaterial);
        }

        // Increase effect
        _epicIndex++;
        if (_changeMaterials.Count <= _epicIndex)
        {
            _epicIndex = 0;
        }

        // Change material
        _currentEpic = _changeMaterials[_epicIndex];
        if (_currentEpic && _currentEpic.isActiveAndEnabled)
        {
            _currentEpic.ChangeMaterial(_epicMaterial);
        }
    }
}
