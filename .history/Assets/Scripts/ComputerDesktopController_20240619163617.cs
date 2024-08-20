using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerDesktopController : MonoBehaviour
{
    private GameObject desktopIconTxt;
    private GameObject desktopIconEmail;
    private GameObject txt;
    private GameObject email;
    // Start is called before the first frame update
    void Start()
    {
        desktopIconTxt = transform.Find("icon1").gameObject;
        desktopIconEmail = transform.Find("icon2").gameObject;
        txt = transform.Find("txt").gameObject;
        email = transform.Find("email").gameObject;
        txt.SetActive(false);
        email.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
