using UnityEngine;
using UnityEngine.UI;

// Manage settings function (balance audios, profil value...)
// Set audio mixer volume in Start and play Main menu music
public class SettingsUI : MonoBehaviour
{
	// Different volume to change
	private const string MUSIC_VOLUME = "MusicVolume";
	private const string FX_VOLUME = "FXVolume";
	private const float MAX_VOLUME = 1f;            // Max Volume between 0.0001 and 1 (cast in db)
	private const float MIN_VOLUME = 0.001f;        // Min Volume between 0.0001 and 1 (cast in db)
	private const int MAX_FREQUENCY = 2;            // Max Frequency Missile
	private const int MIN_FREQUENCY = 1;            // Min Frequency Missile
	private const int MAX_SPEED = -2;               // Max Speed Missile
	private const int MIN_SPEED = -5;               // Min Speed Missile

	[Header("Modifiers")]
	[SerializeField] private SettingsHandler _settings = null;
	[SerializeField] private UnityEngine.Audio.AudioMixer _audioMixer = null;
	[SerializeField] private AudioSource _music = null;

	[Header("Main Menu")]
	[SerializeField] private Button _openButton = null;
	[SerializeField] private Button _closeButton = null;
	[SerializeField] private GameObject _mainMenu = null;
	[SerializeField] private GameObject _settingsMenu = null;

	[Header("Settings UI")]
	[SerializeField] private Button _saveButton = null;
	[SerializeField] private Button _resetButton = null;
	[SerializeField] private Toggle _particleToggle = null;
	[SerializeField] private Slider _musicSlider = null;
	[SerializeField] private Slider _fxSlider = null;
	[SerializeField] private Slider _frequencySlider = null;
	[SerializeField] private Slider _speedSlider = null;

	private ProfilSettings _initialProfil = null;

	private void Awake()
	{
		if (!_settings)
		{
			Debug.LogError($"Settings is undefined in {name}");
			return;
		}

		// Reset by default and load save profil

		_settings.Reset();
		_settings.Load();
		_initialProfil = new ProfilSettings(_settings.Current);

		SetSliders();
	}

	private void Start()
	{
		if (!_audioMixer)
		{
			Debug.LogError($"Audio Mixer is undefined");
			return;
		}
		// Balance audios then play music
		BalanceAudios();
		_music.Play();
	}

	private void OnEnable()
	{
		AddListeners();
		ResetValue();
	}

	private void OnDisable() => RemoveListeners();

	#region UI Listeners
	private void AddListeners()
	{
		// Open / Close
		_openButton.onClick.AddListener(Open);
		_closeButton.onClick.AddListener(Close);

		// Save / Reset
		_saveButton.onClick.AddListener(Save);
		_resetButton.onClick.AddListener(Reset);

		// Settings Button
		_particleToggle.onValueChanged.AddListener(OnParticleChange);

		// Audio Settings
		_musicSlider.onValueChanged.AddListener(OnMusicChange);
		_fxSlider.onValueChanged.AddListener(OnFxChange);

		// Missile Settings
		_frequencySlider.onValueChanged.AddListener(OnFrequencyChange);
		_speedSlider.onValueChanged.AddListener(OnSpeedChange);
	}

	private void RemoveListeners()
	{
		// Open / Close
		_openButton.onClick.RemoveListener(Open);
		_closeButton.onClick.RemoveListener(Close);

		// Save / Reset
		_saveButton.onClick.RemoveListener(Save);
		_resetButton.onClick.RemoveListener(Reset);

		// Settings Button
		_particleToggle.onValueChanged.RemoveListener(OnParticleChange);

		// Audio Settings
		_musicSlider.onValueChanged.RemoveListener(OnMusicChange);
		_fxSlider.onValueChanged.RemoveListener(OnFxChange);

		// Missile Settings
		_frequencySlider.onValueChanged.RemoveListener(OnFrequencyChange);
		_speedSlider.onValueChanged.RemoveListener(OnSpeedChange);
	}
	#endregion

	#region Reset
	private void Reset()
	{
		_settings.Reset();
		ResetValue();
		_initialProfil = new ProfilSettings(_settings.Current);
		_settings.Save();
	}

	// Set initial value in all options
	private void ResetValue()
	{
		_particleToggle.isOn = _settings.Current.ShowParticule;
		_fxSlider.value = _settings.Current.FXVolume;
		_musicSlider.value = _settings.Current.MusicVolume;
		_frequencySlider.value = _settings.Current.FrequencyMultiplier;
		_speedSlider.value = -_settings.Current.SpeedMultiplier;
	}

	private void SetSliders()
	{
		// Audios Settings
		_musicSlider.maxValue = MAX_VOLUME;
		_fxSlider.maxValue = MAX_VOLUME;
		_musicSlider.minValue = MIN_VOLUME;
		_fxSlider.minValue = MIN_VOLUME;

		// Missile Settings
		_speedSlider.maxValue = MAX_SPEED;
		_speedSlider.minValue = MIN_SPEED;
		_frequencySlider.maxValue = MAX_FREQUENCY;
		_frequencySlider.minValue = MIN_FREQUENCY;
	}

	private void BalanceAudios()
	{
		if (_settings.Current.FXVolume == 0 || _settings.Current.MusicVolume == 0)
		{
			Debug.LogError($"0 create infinity in Log10");
			return;
		}

		// Cast 0 to 1 in a DB value with Log10 and * 20
		_audioMixer.SetFloat(FX_VOLUME, Mathf.Log10(_settings.Current.FXVolume) * 20);
		_audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(_settings.Current.MusicVolume) * 20);
	}
	#endregion

	#region Change Settings
	private void Open()
	{
		_settingsMenu.SetActive(true);
		_mainMenu.SetActive(false);
		// Initial profil
		_initialProfil = new ProfilSettings(_settings.Current);
		ResetValue();
	}

	private void Close()
	{
		_settingsMenu.SetActive(false);
		_mainMenu.SetActive(true);

		// Initial profil / Reset value
		_settings.Reset(_initialProfil);
		BalanceAudios();
	}

	private void Save()
	{
		_settings.Save();
		_initialProfil = new ProfilSettings(_settings.Current);
	}

	private void OnParticleChange(bool value) => _settings.Current.ShowParticule = value;
	private void OnMusicChange(float value)
	{
		_settings.Current.MusicVolume = value;
		BalanceAudios();
	}
	private void OnFxChange(float value)
	{
		_settings.Current.FXVolume = value;
		BalanceAudios();
	}
	private void OnFrequencyChange(float value) => _settings.Current.FrequencyMultiplier = (ushort)value;
	private void OnSpeedChange(float value) => _settings.Current.SpeedMultiplier = (ushort)-value;
	#endregion
}
