using UnityEngine;

// Apply value and reset to default
[CreateAssetMenu(fileName = "New Handler", menuName = "Profil Settings/Handler")]
public class SettingsHandler : ScriptableObject
{
	[SerializeField] private ProfilScriptable _current = null;    // User profil
	[SerializeField] private ProfilScriptable _default = null;    // Default Profil

	public ProfilSettings Current => _current.Profil;
	private ProfilSettings Default => _default.Profil;

	// Reset with default value
	public void Reset()
	{
		Current.MovementType = Default.MovementType;
		Current.ShowParticule = Default.ShowParticule;
		Current.FXVolume = Default.FXVolume;
		Current.MusicVolume = Default.MusicVolume;

		Current.ProfilChange();
	}

	#region Save Load
	[ContextMenu("Save")]
	public void Save()
	{
		string json = JsonUtility.ToJson(Current, true);
		FileManagement.Write(FileNameConst.SETTINGS, json);
	}

	[ContextMenu("Load")]
	public void Load()
	{
		string json = FileManagement.Read(FileNameConst.SETTINGS);
		if (json == "") { return; }

		_current.Profil = JsonUtility.FromJson<ProfilSettings>(json);
		Current.ProfilChange();
	}
	#endregion
}
