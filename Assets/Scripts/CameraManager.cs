using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCam;
    [SerializeField]
    private GameObject SubCam;

    void Start () {
        //MainCam = GameObject.Find("MainCamera");
        //SubCam = GameObject.Find("SubCamera");

        SubCam.SetActive(false);
    }

    void Update () {
        if(Input.GetKeyDown("space")){
            if(MainCam.activeSelf){
                MainCam.SetActive (false);
                SubCam.SetActive (true);
            }else{
                MainCam.SetActive (true);
                SubCam.SetActive (false);
            }
        }
    }
}
