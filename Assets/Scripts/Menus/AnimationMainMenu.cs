using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationMainMenu : MonoBehaviour
{
    private GameObject _vrCamera;
    void Start()
    {
        DOTween.Init();
        _vrCamera=GameObject.Find("VR Camera");
    }

    void Update()
    {
      gameObject.transform.DOLocalRotate(new Vector3(_vrCamera.transform.rotation.x, _vrCamera.transform.rotation.y, _vrCamera.transform.rotation.z), 0.5f);
    }
}
