using UnityEngine;

// Manage differents tutorial steps
public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialSteps[] _tutorialSteps = new TutorialSteps[0];
    [SerializeField] private FadeScreen _fadeScreens = null;

    public int CountSteps => _tutorialSteps.Length;
    private bool StepOutOfBound => _indexSteps < 0 || _tutorialSteps.Length <= _indexSteps;
    private uint ResetOutOfBound => _indexSteps < 0 ? 0 : _indexSteps - 1;

    private uint _indexSteps = 0;
    private TutorialSteps _previousStep = new TutorialSteps();      // Previous Tutorial steps to hide buildings
    private System.Action<TutorialSteps, TutorialSteps, uint> OnChangeSteps = null;
    private bool _changingStep = false;                             // Allow one changing step in same time

    private void Start()
    {
        ChangeSteps();
        _fadeScreens.Open();
    }

    private void OnEnable() => _fadeScreens.OnFadeScreen += ChangeSteps;

    private void OnDisable() => _fadeScreens.OnFadeScreen -= ChangeSteps;

    #region Interface to change steps
    public void NextSteps()
    {
        // Do not close curtain in last step
        if (_indexSteps < _tutorialSteps.Length - 1 && !_changingStep)
        {
            _indexSteps++;
            WaitScreen();
        }
    }

    public void PreviousSteps()
    {
        if (!_changingStep)
        {
            _indexSteps--;
            WaitScreen();
        }
    }

    // Close curtain, set stage then open curtain
    private void WaitScreen()
    {
        _changingStep = true;

        // Destroy all bullets
        foreach (var bullet in FindObjectsOfType<Bullet>())
            bullet.Hit();

        _fadeScreens.CloseThenOpen();

        if (_previousStep.GetStepCheck)
        {
            _previousStep.GetStepCheck.Deselect(this, _previousStep);
        }
    }
    #endregion

    #region Callbacks
    public void Register(System.Action<TutorialSteps, TutorialSteps, uint> add)
    {
        if (add == null) { return; }

        OnChangeSteps += add;
    }

    public void Unregister(System.Action<TutorialSteps, TutorialSteps, uint> remove)
    {
        if (remove == null) { return; }

        OnChangeSteps -= remove;
    }
    #endregion

    private void ChangeSteps()
    {
        // Reset index in a out of bound
        if (StepOutOfBound)
        {
            _indexSteps = ResetOutOfBound;
            return;
        }

        TutorialSteps current = _tutorialSteps[_indexSteps];

        if (_previousStep.GetStepCheck)
        {
            _previousStep.GetStepCheck.Deselect(this, _previousStep);
        }

        // Initialize step check
        if (current.GetStepCheck)
        {
            current.GetStepCheck.Select(this, current);
        }

        OnChangeSteps?.Invoke(current, _previousStep, _indexSteps);

        _previousStep = _tutorialSteps[_indexSteps];
        _changingStep = false;
    }
}
