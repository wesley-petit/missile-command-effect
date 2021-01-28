using UnityEngine;

public class BaseUI : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject thirdLayer;
	public GameObject secondLayer;

	public void ManageAnimation(bool cursorState)
	{
		if (cursorState)
		{
			thirdLayer.GetComponent<Animator>().Play("thirdLayer");
			secondLayer.GetComponent<Animator>().Play("secondLayer");
		}
		else
		{
			secondLayer.GetComponent<Animator>().Play("secondLayerReverse");
			thirdLayer.GetComponent<Animator>().Play("thirdLayerReverse");
		}
	}
}
