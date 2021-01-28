using UnityEngine;

// Profil settings use to save and load data
[System.Serializable]
public class ProfilSettings
{
	[SerializeField] private MovementType _movementType = MovementType.XR;
	[SerializeField] private bool _showParticule = true;
	[Range(0.0001f, 1)]
	[SerializeField] private float _fxVolume = 1f;
	[Range(0.0001f, 1)]
	[SerializeField] private float _musicVolume = 1f;
	[Range(1, 2)]
	[SerializeField] private ushort _frequencyMultiplier = 2;   // Impact missile frequency / Higher => More missile
	[Range(2, 5)]
	[SerializeField] private ushort _speedMultiplier = 3;       // Impact missile speed / Higher => Slow Missile

	public ProfilSettings(ProfilSettings cloneSettings)
	{
		if (cloneSettings == null) { return; }

		_movementType = cloneSettings.MovementType;
		_showParticule = cloneSettings.ShowParticule;
		_fxVolume = cloneSettings.FXVolume;
		_musicVolume = cloneSettings.MusicVolume;
		_frequencyMultiplier = cloneSettings.FrequencyMultiplier;
		_speedMultiplier = cloneSettings.SpeedMultiplier;
	}

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
}