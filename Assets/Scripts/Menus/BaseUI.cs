using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thirdLayer;
    public GameObject secondLayer;
    void Start()
    {
        Debug.Log(gameObject.name);
    }
    public void ManageAnimation(bool cursorState)
    {
      if(cursorState)
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
    // Update is called once per frame
    void Update()
    {

    }
}
