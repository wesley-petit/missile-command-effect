using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	// Scene to load
	[SerializeField] private SceneIndexes _game = SceneIndexes.GAME;
	[SerializeField] private SceneIndexes _settings = SceneIndexes.SETTINGS;
	[SerializeField] private SceneIndexes _mainMenu = SceneIndexes.MAIN_MENU;

	public void Play() => ChangeScene(_game);

	public void Settings() => ChangeScene(_settings);

	public virtual void ReturnToMainMenu() => ChangeScene(_mainMenu);

	public void ChangeScene(SceneIndexes playScene) => SceneManager.LoadScene((int)playScene);

	public void Quit() => Application.Quit();
}