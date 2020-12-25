using UnityEngine;
using UnityEngine.InputSystem;

// Inherit of main menu to have the same methodes (change scene, ...)
public class PauseMenu : MainMenu
{
	[SerializeField] private GameObject _containerPauseMenu = null; // Pause menu to display or hide
	[SerializeField] private PlayerInput _playerInputs = null;

	public bool IsPause { get; private set; }                       // If we are on play or not

	private float _normalTime = 60.0f;                              // Reset time in a normal way
	private Controls _controls = null;

	private void Awake()
	{
		_controls = new Controls();

		_normalTime = Time.timeScale;
		DisplayOrHideMenu(false, _normalTime);
	}

	private void Start()
	{
		if (!_containerPauseMenu)
		{
			Debug.LogError("There is no container to display or hide.");
			return;
		}

		if (!_playerInputs)
		{
			Debug.LogError("Player Inputs is undefined. I can switch between UI and Player maps.");
		}

		_controls.Player.Menu.performed += cxt => TogglePause();
	}

	private void OnEnable()
	{
		_controls?.Enable();
		GameState.OnWinEvent += ChangeToUIMaps;
		GameState.OnLoseEvent += ChangeToUIMaps;
	}

	private void OnDisable()
	{
		_controls?.Disable();
		GameState.OnWinEvent -= ChangeToUIMaps;
		GameState.OnLoseEvent -= ChangeToUIMaps;
	}

	private void TogglePause()
	{
		if (!IsPause && !GameState.EndGame)
		{
			DisplayOrHideMenu(true, 0.0f);
			AudioSync.Instance.Pause();
			ChangeInputMaps(InputMaps.UI);
		}
		else
		{
			Resume();
			AudioSync.Instance.Play();
			ChangeInputMaps(InputMaps.PLAYER);
		}
	}

	// Use by ui to resume the game
	public void Resume() => DisplayOrHideMenu(false, _normalTime);

	// Stop time and display menu
	// Reset time and hide menu
	private void DisplayOrHideMenu(bool pauseState, float timeScale)
	{
		if (!_containerPauseMenu) { return; }

		IsPause = pauseState;
		Time.timeScale = timeScale;
		_containerPauseMenu.SetActive(pauseState);
	}

	// Callbacks in OnWinEvent / OnLoseEvent
	private void ChangeToUIMaps() => ChangeInputMaps(InputMaps.UI);
	// Change between player and ui maps
	private void ChangeInputMaps(string inputMaps) => _playerInputs.SwitchCurrentActionMap(inputMaps);
}
