using UnityEngine;

// Set audio mixer volume in Start and play Main menu music
[RequireComponent(typeof(AudioSource))]
public class AudioMixerVolume : MonoBehaviour
{
	// Different volume to change
	private const string MUSIC_VOLUME = "MusicVolume";
	private const string FX_VOLUME = "FXVolume";

	[SerializeField]
	private UnityEngine.Audio.AudioMixer _audioMixer = null;
	[SerializeField]
	private SettingsHandler _settingsHandler = null;// Audio volume

	private void Start()
	{
		if (!_settingsHandler)
		{
			Debug.LogError($"Settings Handler is undefined in {name}");
			return;
		}

		_settingsHandler.Load();

		if (!_audioMixer)
		{
			Debug.LogError($"Audio Mixer is undefined");
			return;
		}

		BalanceAudios();

		// TODO Play music
		//GetComponent<AudioSource>().Play();
	}

	private void OnEnable()
	{
		if (!_settingsHandler) { return; }
		_settingsHandler.Current.Register(BalanceAudios);
	}

	private void OnDisable()
	{
		if (!_settingsHandler) { return; }
		_settingsHandler.Current.Unregister(BalanceAudios);
	}

	public void SetFXVolume(float volume)
	{
		_settingsHandler.Current.FXVolume = volume;
		_settingsHandler.Current.ProfilChange();
	}

	public void SetMusicVolume(float volume)
	{
		_settingsHandler.Current.MusicVolume = volume;
		_settingsHandler.Current.ProfilChange();
	}

	private void BalanceAudios()
	{
		_audioMixer.SetFloat(FX_VOLUME, _settingsHandler.Current.FXVolume);
		_audioMixer.SetFloat(MUSIC_VOLUME, _settingsHandler.Current.MusicVolume);
	}
}
