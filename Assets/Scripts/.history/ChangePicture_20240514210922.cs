using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePicture : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject backArrow;
    private float moveDistanceX = 30f;
    // Start is called before the first frame update
    void Start()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        backArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 限制在视角1-3的时候使用
        if (transform.position.y == 0) 
        {

        }
    }

    // 将摄像机移动到左边
    void MoveCameraToLeft()
    {
        Vector3 currentPosition = transform.position;
        float newX = currentPosition.x - moveDistanceX;
        if (newX < -30) newX += 90;
        transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
    }

    // 将摄像机移动到右边
    void MoveCameraToRight()
    {
        Vector3 currentPosition = transform.position;
        float newX = currentPosition.x + moveDistanceX;
        if (newX > 30) newX -= 90;
        transform.position = new Vector3(newX, currentPosition.y, currentPosition.z);
    }

}
