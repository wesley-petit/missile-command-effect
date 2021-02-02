using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour
{
	[SerializeField] private TMPro.TMP_Text _clone = null;
	[SerializeField] private Transform _cloneContainer = null;
	[SerializeField] private Color _normalColor = new Color();
	[SerializeField] private Color _validColor = new Color();

	private List<TMPro.TMP_Text> _allClones = new List<TMPro.TMP_Text>();

	public void NewCheckList(CheckInputsCount checkInputs)
	{
		// Clone if there is no text
		if (_allClones.Count <= 0)
		{
			CreateClones(checkInputs.InputNumber);
		}

		ResetAllText();
		
		// Update each clone
		for (int i = 0; i < checkInputs.InputNumber; i++)
		{
			// Update
			UpdateText(i, checkInputs);
		}
	}

	public void ResetAllText()
	{
		foreach (var reset in _allClones)
		{
			reset.gameObject.SetActive(false);
			reset.fontStyle = TMPro.FontStyles.Normal;
			reset.color = _normalColor;
		}
	}

	private void CreateClones(int cloneNumber)
	{
		for (int i = 0; i < cloneNumber; i++)
		{
			var clone = Instantiate(_clone, _cloneContainer);
			_allClones.Add(clone);
			clone.gameObject.SetActive(false);
		}
	}

	public void UpdateText(int textIndex, CheckInputsCount checkInput)
	{
		var actualCount = checkInput.GetInputMetrics.Shoot[textIndex];
		var maxCount = checkInput.MaxInputCount[textIndex];
		var validCount = checkInput.ValidInputCount[textIndex];

		if (maxCount <= 0) { return; }

		// Display actual count or max to avoid overflow
		var actualShootDisplay = actualCount < maxCount ? actualCount : maxCount;
		TMPro.TMP_Text current = _allClones[textIndex];
		current.text = $"{actualShootDisplay} / {maxCount} shoot - {GetCanonName(textIndex)} Turret";
		current.gameObject.SetActive(true);

		if (validCount)
		{
			current.fontStyle = TMPro.FontStyles.Strikethrough;
			current.color = _validColor;
		}
	}

	private string GetCanonName(int i)
	{
		switch (i)
		{
			case 0:
				return "Left";

			case 1:
				return "Right";
		}

		return "";
	}
}
