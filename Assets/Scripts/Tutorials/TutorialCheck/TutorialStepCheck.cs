// Check each step
public abstract class TutorialStepCheck : UnityEngine.MonoBehaviour
{
	private Tutorial _tutorial = null;
	protected bool _valid = false;									// Step valid or not

	private void OnEnable()
	{
		if (!_tutorial)
		{
			enabled = false;
		}
	}

	// Active check for a select steps
	public virtual void Select(Tutorial tutorial, TutorialSteps current)
	{
		_tutorial = tutorial;
		enabled = true;
	}

	public virtual void Deselect(Tutorial tutorial, TutorialSteps previous)
	{
	}

	// Invoke next step
	protected virtual void ValidStep()
	{
		enabled = false;
		if (_tutorial)
		{
			_tutorial.NextSteps();
		}
	}
}
