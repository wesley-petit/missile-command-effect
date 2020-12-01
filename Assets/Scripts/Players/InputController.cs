using UnityEngine;

public class InputController
{
	public bool LeftShoot => Input.GetMouseButtonDown(0);
	public bool RightShoot => Input.GetMouseButtonDown(1);
}
