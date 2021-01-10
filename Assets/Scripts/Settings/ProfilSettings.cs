using UnityEngine;

// Profil settings use to save and load data
[System.Serializable]
public class ProfilSettings
{
	[SerializeField] private MovementType _movementType = MovementType.XR;
	[SerializeField] private bool _showParticule = true;
	[Range(-80f, 20f)]
	[SerializeField] private float _fxVolume = 0f;
	[Range(-80f, 20f)]
	[SerializeField] private float _musicVolume = 0f;
	[Range(1, 3)]
	[SerializeField] private ushort _frequencyMultiplier = 2;   // Impact missile frequency / Higher => More missile
	[Range(2, 5)]
	[SerializeField] private ushort _speedMultiplier = 3;       // Impact missile speed / Higher => Slow Missile

	#region Fields
	public MovementType MovementType
	{
		get => _movementType;
		set => _movementType = value;
	}
	public bool ShowParticule
	{
		get => _showParticule;
		set => _showParticule = value;
	}
	public float FXVolume
	{
		get => _fxVolume;
		set => _fxVolume = value;
	}
	public float MusicVolume
	{
		get => _musicVolume;
		set => _musicVolume = value;
	}
	public ushort FrequencyMultiplier
	{
		get => _frequencyMultiplier;
		set => _frequencyMultiplier = value;
	}
	public ushort SpeedMultiplier
	{
		get => _speedMultiplier;
		set => _speedMultiplier = value;
	}
	#endregion

	#region Callbacks
	private System.Action OnProfilChange = null;

	public void ProfilChange() => OnProfilChange?.Invoke();

	public void Register(System.Action toAdd)
	{
		if (toAdd == null) { return; }
		OnProfilChange += toAdd;
	}

	public void Unregister(System.Action toRemove)
	{
		if (toRemove == null) { return; }
		OnProfilChange -= toRemove;
	}
	#endregion
}