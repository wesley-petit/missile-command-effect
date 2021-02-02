using UnityEngine;
using UnityEngine.UI;

// Display tutorial advancement, tutorial text and checklist
// Change steps button
[RequireComponent(typeof(Tutorial))]
public class UITutorialInstance : TutorialInstance
{
	[SerializeField]
	private Slider _progressionSlider = null;                   // Show advancement
	[SerializeField] private Button _previousButton = null;     // Launch previous steps
	[SerializeField] private Button _nextButton = null;         // Launch next steps
	[SerializeField] private Button _playButton = null;
	[SerializeField] private TMPro.TMP_Text _explainText = null;// Explain each steps
	[SerializeField] private Image _controls = null;            // Controls image

	private float _interval = 0;

	protected override void OnEnable()
	{
		base.OnEnable();

		if (_tutorial)
		{
			// Button change steps
			//_nextButton.onClick.AddListener(_tutorial.NextSteps);
			//_previousButton.onClick.AddListener(_tutorial.PreviousSteps);
			_playButton.gameObject.SetActive(false);

			// Slider Interval
			_interval = _progressionSlider.maxValue / _tutorial.CountSteps;
		}
	}

	protected override void UpdateSteps(TutorialSteps current, TutorialSteps previous, uint currentIndex)
	{
		_explainText.text = current.GetTutorialText;
		// Update Progression value
		_progressionSlider.value = _interval * currentIndex;

		// Hide button in off limit
		_previousButton.gameObject.SetActive(currentIndex != 0);

		// Last Steps
		bool lastSteps = _tutorial.CountSteps - 1 <= currentIndex;
		_nextButton.gameObject.SetActive(!lastSteps);
		_playButton.gameObject.SetActive(lastSteps);

		// TODO
		_controls.gameObject.SetActive(currentIndex == 1);
	}
}
