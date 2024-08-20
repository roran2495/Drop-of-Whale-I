using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle1DetailController : MonoBehaviour
{
    public GameObject curtain;
    private boolean flagCurtain;    // open : false   ;     close: true
    public GameObject pillow1;
    public GameObject pillow2;
    public GameObject broom1;
    public GameObject broom2;
    public GameObject carpet1;
    public GameObject carpet2;
    public GameObject letter;
    public GameObject phone;
    public GameObject shoeCabinet;
    // Start is called before the first frame update
    void Start()
    {
        curtain.SetActive(true);
        flagCurtain = false;
        pillow1.SetActive(true);
        pillow2.SetActive(false);
        broom1.SetActive(true);
        broom2.SetActive(false);
        carpet1.SetActive(true);
        carpet2.SetActive(false);
        letter.SetActive(false);
        phone.SetActive(false);
        shoeCabinet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
