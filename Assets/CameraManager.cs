using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Video;

public class CameraManager : MonoBehaviour
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();   

    public static CinemachineVirtualCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        newCamera.Priority = 10;
        ActiveCamera = newCamera;

        foreach (CinemachineVirtualCamera cam in cameras) 
        { 
            if(cam != newCamera)
            {
                cam.Priority = 0;

            }
        }
    }

    public static void Register(CinemachineVirtualCamera camare) 
    {
        cameras.Add(camare);
    }

    public static void Unregister(CinemachineVirtualCamera camera) 
    {
        cameras.Remove(camera);
    }
}
