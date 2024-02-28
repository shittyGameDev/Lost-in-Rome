using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject topDownCamera;
    public GameObject firstPersonCamera;
    public int Manager;


    public void ManageCameras()
    {
        if(Manager == 0)
        {
            firstPersonCam();
            Manager = 1;
        }
        else
        {
            topDownCam();
            Manager = 0;
        }
    }

    public void topDownCam()
    {
        topDownCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
    }

    public void firstPersonCam()
    {
        topDownCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
    }
}
