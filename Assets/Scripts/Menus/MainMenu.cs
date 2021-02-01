using UnityEngine;
using UnityEngine.SceneManagement;

// Change scene and index reference for each scene
public class MainMenu : MonoBehaviour
{
	// Scene to load
	[SerializeField] private SceneIndexes _game = SceneIndexes.GAME;
	[SerializeField] private SceneIndexes _mainMenu = SceneIndexes.MAIN_MENU;
	[SerializeField] private SceneIndexes _tutorials = SceneIndexes.TUTORIALS;

	public virtual void Play() => ChangeScene(_game);

	public virtual void Tutorials() => ChangeScene(_tutorials);

	public virtual void ReturnToMainMenu() => ChangeScene(_mainMenu);

	public virtual void ChangeScene(SceneIndexes playScene) => SceneManager.LoadScene((int)playScene);

	public virtual void Quit() => Application.Quit();
}