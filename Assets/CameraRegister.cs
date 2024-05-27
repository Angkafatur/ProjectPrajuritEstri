using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraManager.Register(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        CameraManager.Register(GetComponent < CinemachineVirtualCamera>());
    }
}
