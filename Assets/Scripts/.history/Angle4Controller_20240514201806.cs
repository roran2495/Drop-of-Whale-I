using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle4Controller : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 保证是触发视图4跳转
        if (mainCamera.transform.position.x == 0 && mainCamera.transform.position.y == 15)
        {
        }
    }
}
