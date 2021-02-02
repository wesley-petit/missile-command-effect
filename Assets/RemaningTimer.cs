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
    [SerializeField]
    private float timeRoundSec;
    private float timeRound;
    void Start()
    {
        float tempTime=RoundSystem.Instance.TotalTimeRounds/20f;
        timeRound=0.02f/tempTime;
        LineRenderer ownLine=this.GetComponent<LineRenderer>();
        float CameraWidth=m_FirstCamera.pixelWidth;
        float CameraHeight=m_FirstCamera.pixelHeight;
        ownLine.SetPosition(0, new Vector3(CameraWidth*-6.45f/901, CameraHeight*4.18f/584,0f));
        ownLine.SetPosition(1, new Vector3(CameraWidth*-6.45f/901, CameraHeight*-4.21f/584, 0f));
        ownLine.SetPosition(2, new Vector3(CameraWidth*6.45f/901, CameraHeight*-4.21f/584, 0f));
        ownLine.SetPosition(3, new Vector3(CameraWidth*6.45f/901, CameraHeight*4.18f/584, 0f));
        ownLine.SetPosition(4, new Vector3(CameraWidth*-6.45f/901, CameraHeight*4.18f/584, 0f));
        ownLine.sortingOrder = 1;
        ownLine.material = new Material(Shader.Find("Sprites/Default"));
        ownLine.material.color = Color.red; 
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
                phaseTwo=true;
                if(ownLine.GetPosition(2).y>3.93f)
                {
                    if(ownLine.GetPosition(3).x>-7.28f)
                    {
                       ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(3).x-timeRound, ownLine.GetPosition(3).y, 0f));
                       ownLine.SetPosition(1, new Vector3(ownLine.GetPosition(3).x-timeRound, ownLine.GetPosition(3).y, 0f));
                       ownLine.SetPosition(2, new Vector3(ownLine.GetPosition(3).x-timeRound, ownLine.GetPosition(3).y, 0f));
                       ownLine.SetPosition(3, new Vector3(ownLine.GetPosition(3).x-timeRound, ownLine.GetPosition(3).y, 0f)); 
                   }
                }
                else
                {
                    ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(2).x, ownLine.GetPosition(2).y+timeRound, 0f));
                    ownLine.SetPosition(1, new Vector3(ownLine.GetPosition(2).x, ownLine.GetPosition(2).y+timeRound, 0f));
                    ownLine.SetPosition(2, new Vector3(ownLine.GetPosition(2).x, ownLine.GetPosition(2).y+timeRound, 0f)); 
                }
            }
            else
            {
                if(!phaseTwo)
                {
                    ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(1).x+timeRound, ownLine.GetPosition(1).y, 0f));
                    ownLine.SetPosition(1, new Vector3(ownLine.GetPosition(1).x+timeRound, ownLine.GetPosition(1).y, 0f));
                }
            }
        }
        else
        {
            if(!phaseOne)
            {
                ownLine.SetPosition(0, new Vector3(ownLine.GetPosition(0).x, ownLine.GetPosition(0).y-timeRound, 0f));
            }
        }
    }
    void Update()
    {
        decreaseSize();
    }
}
