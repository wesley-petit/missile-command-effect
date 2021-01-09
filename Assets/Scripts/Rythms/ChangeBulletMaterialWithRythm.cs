using UnityEngine;

public class ChangeBulletMaterialWithRythm : ChangeMaterialWithRythm
{
	[SerializeField]
	private Material _matInStrongTime = null;       // Material to use in a strong time
	[SerializeField]
	private Material _defaultMat = null;            // Material in normal use

	private bool _alreadyChange = false;

	private void Update()
	{
		if (AudioManager.IsInPace)
		{
			if (AudioManager.IsInStrongTime)
			{
				ApplyChange(_matInStrongTime);
				_alreadyChange = true;
			}
			else if (_alreadyChange)
			{
				ApplyChange(_defaultMat);
				_alreadyChange = false;
			}
		}
	}
}
