using UnityEngine;

// Profil settings for several platform (PC, XR ...)
[CreateAssetMenu(fileName = "New Profil", menuName = "Profil Settings/Profil")]
public class ProfilScriptable : ScriptableObject
{
	[SerializeField] private ProfilSettings _profil;

	public ProfilSettings Profil
	{
		get => _profil;
		set => _profil = value;
	}
}
