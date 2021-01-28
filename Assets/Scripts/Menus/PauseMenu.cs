using UnityEngine;

// Inherit of main menu to have the same methodes (change scene, ...)
public class PauseMenu : MainMenu
{
	[SerializeField] private GameObject _containerPauseMenu = null; // Pause menu to display or hide
	[SerializeField]
	private InputHandler[] _inputs = new InputHandler[0];           // Block inputs

	public bool IsPause { get; private set; }                       // If we are on play or not

	private float _normalTime = 1f;                                 // Reset time in a normal way
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

		if (_inputs.Length <= 0)
		{
			Debug.LogError("Inputs are undefined. I can switch between UI and Player maps.");
		}

		_controls.Player.Menu.performed += cxt => TogglePause();
	}

	#region Callbacks
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
	#endregion

	private void TogglePause()
	{
		// End game avoid to display pause menu in another win / lose screen
		if (!IsPause && !GameState.EndGame)
		{
			Pause();
		}
		else
		{
			Resume();
		}
	}

	private void Pause()
	{
		AudioSync.Instance.Pause();
		DisplayOrHideMenu(true, 0.0f);
		ChangeInputMaps(false);
	}

	// Use by ui to resume the game
	public void Resume()
	{
		AudioSync.Instance.Play();
		DisplayOrHideMenu(false, _normalTime);
		ChangeInputMaps(true);
	}

	public override void ReturnToMainMenu()
	{
		Time.timeScale = _normalTime;
		base.ReturnToMainMenu();
	}

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
	private void ChangeToUIMaps() => ChangeInputMaps(false);
	// Change between player and ui maps
	private void ChangeInputMaps(bool state)
	{
		foreach (var input in _inputs)
		{
			input.enabled = state;
		}
	}
}
