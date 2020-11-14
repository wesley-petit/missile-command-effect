using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorActions : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
      Vector3 temp = Input.mousePosition;
      temp.z = 1f;
      this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
