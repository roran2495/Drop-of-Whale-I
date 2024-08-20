using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle8Controller : MonoBehaviour
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
        // 保证是触发视图8跳转
        if (mainCamera.position.x == 60 && mainCamera.position.y == 0)
        {
            
        }
        
    }
}
