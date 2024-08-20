using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject infor;
    public GameObject button;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }
    void HandleButton1Click()
    {

    }
    void HandleButton2Click()
    {
        SceneManager.LoadScene("Start");
    }
    void HandleButton3Click()
    {
        button.SetActive(false);
        infor.SetActive(true);
    }
    void HandleButton4Click()
    {
        button.SetActive(true);
        infor.SetActive(false);
    }
}
