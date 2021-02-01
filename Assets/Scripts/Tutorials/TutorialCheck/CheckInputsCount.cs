using UnityEngine;

// Check for each shoot l(eft and right) if we hit a valid count
public class CheckInputsCount : TutorialStepCheck
{
	[SerializeField] private InputMetrics _inputMetrics = null;
	[SerializeField] private uint[] _maxInputCount = new uint[2];
	[SerializeField] private CheckList _checklist = null;

	public InputMetrics GetInputMetrics => _inputMetrics;
	public uint[] MaxInputCount => _maxInputCount;
	public bool[] ValidInputCount { get; private set; } = new bool[2];
	public int InputNumber => _inputMetrics.Shoot.Length;

	private void Awake()
	{
		_inputMetrics.enabled = false;
		ValidInputCount = new bool[InputNumber];
	}

	private void FixedUpdate()
	{
		if (_valid) { return; }

		CheckEndCondition();
	}

	#region Check Input
	private void CheckEndCondition()
	{
		if (IsValidInputCount() && !_valid)
		{
			_valid = true;
			ValidStep();
		}
	}

	// Read input count and check valid count
	private bool IsValidInputCount()
	{
		bool allValid = true;

		// Check conditions For each inputs
		for (int i = 0; i < ValidInputCount.Length; i++)
		{
			// Not valid
			if (_inputMetrics.Shoot[i] < _maxInputCount[i])
			{
				allValid = false;
			}
			// Valid for the first time
			else if (!ValidInputCount[i])
			{
				ValidInputCount[i] = true;
			}

			_checklist.UpdateText(i, this);
		}

		return allValid;
	}
	#endregion

	#region Select Deselect
	public override void Select(Tutorial tutorial, TutorialSteps current)
	{
		base.Select(tutorial, current);

		_inputMetrics.ResetCount();
		_inputMetrics.enabled = true;

		_checklist.NewCheckList(this);
	}

	public override void Deselect(Tutorial tutorial, TutorialSteps previous)
	{
		base.Deselect(tutorial, previous);

		for (int i = 0; i < ValidInputCount.Length; i++)
			ValidInputCount[i] = false;

		_valid = false;
		enabled = false;
		_inputMetrics.ResetCount();
		_checklist.ResetAllText();
	}
	#endregion
}
