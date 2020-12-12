using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InputController : MonoBehaviour
{
	private InputDevice _targetDevice = new InputDevice();
	[SerializeField]
	private XRController _controller = null;

	public bool LeftShoot { get; private set; }
	public bool RightShoot { get; private set; }

	#region UnityMethods
	private void Start()
	{
		_controller.SendHapticImpulse(1f, 1f);

		List<InputDevice> devices = new List<InputDevice>();
		InputDeviceCharacteristics rightCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
		InputDevices.GetDevicesWithCharacteristics(rightCharacteristics, devices);

		if (0 < devices.Count)
		{
			_targetDevice = devices[0];
		}
	}

	private void Update()
	{
		LeftShoot = Input.GetMouseButtonDown(0);
		RightShoot = Input.GetMouseButtonDown(1);

		if (_targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton) && triggerButton)
		{
			LeftShoot = triggerButton;
		}

		if (_targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton) && primaryButton)
		{
			RightShoot = primaryButton;
		}
	}
	#endregion
}
