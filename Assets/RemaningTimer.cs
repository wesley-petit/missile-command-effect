using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemaningTimer : MonoBehaviour
{
    [SerializeField]
    private Camera m_FirstCamera;
    private bool phaseOne=false;
    private bool phaseTwo=false;
    private bool phaseThree=false;
    private bool phaseFour=false;
    void Start()
    {
        LineRenderer ownLine=this.GetComponent<LineRenderer>();
        float CameraWidth=m_FirstCamera.pixelWidth;
        float CameraHeight=m_FirstCamera.pixelHeight;
        ownLine.SetPosition(0, new Vector3(CameraWidth*-6.45f/901, CameraHeight*4.18f/584,0f));
        ownLine.SetPosition(1, new Vector3(CameraWidth*-6.45f/901, CameraHeight*-4.21f/584, 0f));
        ownLine.SetPosition(2, new Vector3(CameraWidth*6.45f/901, CameraHeight*-4.21f/584, 0f));
        ownLine.SetPosition(3, new Vector3(CameraWidth*6.45f/901, CameraHeight*4.18f/584, 0f));
        ownLine.SetPosition(4, new Vector3(CameraWidth*-6.45f/901, CameraHeight*4.18f/584, 0f));
        decreaseSize();
    }
    void decreaseSize()
    {
        LineRenderer ownLine=this.GetComponent<LineRenderer>();
        if(ownLine.GetPosition(0).y<=-4.1f || phaseOne)
        {
            phaseOne=true;
            if(ownLine.GetPosition(1).x>7f || phaseTwo)
            {
                // Deuxieme trait mort
                phaseTwo=true;
                Debug.Log("Phase Two lol");
                if(ownLine.GetPosition(2).y>3.93f)
                {
                    Debug.Log("Walking");
                    if(ownLine.GetPosition(3).x>-7.28f)
                    {
                        Debug.Log("Simulator");
                       ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(3).x-0.5f, ownLine.GetPosition(3).y, 0f));
                       ownLine.SetPosition(1, new Vector3(ownLine.GetPosition(3).x-0.5f, ownLine.GetPosition(3).y, 0f));
                       ownLine.SetPosition(2, new Vector3(ownLine.GetPosition(3).x-0.5f, ownLine.GetPosition(3).y, 0f));
                       ownLine.SetPosition(3, new Vector3(ownLine.GetPosition(3).x-0.5f, ownLine.GetPosition(3).y, 0f)); 
                       Debug.Log("Quatrieme trait mort");
                   }
                }
                else
                {
                    ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(2).x, ownLine.GetPosition(2).y+0.5f, 0f));
                    ownLine.SetPosition(1, new Vector3(ownLine.GetPosition(2).x, ownLine.GetPosition(2).y+0.5f, 0f));
                    ownLine.SetPosition(2, new Vector3(ownLine.GetPosition(2).x, ownLine.GetPosition(2).y+0.5f, 0f)); 
                }
            }
            else
            {
                if(!phaseTwo)
                {
                    ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(1).x+0.5f, ownLine.GetPosition(1).y, 0f));
                    ownLine.SetPosition(1, new Vector3(ownLine.GetPosition(1).x+0.5f, ownLine.GetPosition(1).y, 0f));
                }
            }
        }
        else
        {
            if(!phaseOne)
            {
                ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(0).x, ownLine.GetPosition(0).y-0.5f, 0f));
            }
        }
    }
    void Update()
    {
        decreaseSize();
    }
}
