using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public int Manager;
   

    private void Start()
    {
        firstPersonCamera.SetActive(false);
     
    }

    public void ManageCameras()
    {
        if(Manager == 0)
        {
            thirdPerson();
            Manager = 1;
        }
        else
        {
            firstPerson();
            Manager = 0;
        }
    }

    public void thirdPerson()
    {
        thirdPersonCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
    }

    public void firstPerson()
    {
        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
    }
    


}

