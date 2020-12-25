using UnityEngine;

// Display win or lose Screen
public class UIGameState : MonoBehaviour
{
	[SerializeField] private GameObject _winPannel = null;
	[SerializeField] private GameObject _losePannel = null;
	[SerializeField] private GameObject _canvas = null;             // UI to replay a game or return in the menu

	private void Start()
	{
		if (!_winPannel)
		{
			Debug.LogError("Win Pannel is undefined.");
			return;
		}

		if (!_losePannel)
		{
			Debug.LogError("Lose Pannel is undefined.");
			return;
		}

		if (!_canvas)
		{
			Debug.LogError("Canvas is undefined.");
		}

		_winPannel.SetActive(false);
		_losePannel.SetActive(false);
		_canvas.SetActive(false);
	}

	#region Register Callbacks
	private void OnEnable()
	{
		GameState.OnWinEvent += DisplayWinScreen;
		GameState.OnLoseEvent += DisplayLoseScreen;
	}

	private void OnDisable()
	{
		GameState.OnWinEvent -= DisplayWinScreen;
		GameState.OnLoseEvent -= DisplayLoseScreen;
	}
	#endregion

	private void DisplayWinScreen()
	{
		_winPannel.SetActive(true);
		Common();
	}

	private void DisplayLoseScreen()
	{
		_losePannel.SetActive(true);
		Common();
	}

	private void Common() => _canvas.SetActive(true);
}
