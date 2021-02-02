using UnityEngine;

// Basic tutorial instance
[RequireComponent(typeof(Tutorial))]
public abstract class TutorialInstance : MonoBehaviour
{
	protected Tutorial _tutorial = null;

	protected virtual void OnEnable()
	{
		_tutorial = GetComponent<Tutorial>();

		if (_tutorial)
		{
			_tutorial.Register(UpdateSteps);
		}
	}

	protected virtual void OnDisable()
	{
		if (_tutorial)
		{
			_tutorial.Unregister(UpdateSteps);
		}
	}

	protected abstract void UpdateSteps(TutorialSteps current, TutorialSteps previous, uint currentIndex);
}
