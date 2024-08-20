using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInformationController : MonoBehaviour
{
    public Angle8Controller angle8Controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInformation()
    {
        StartCoroutine(SetInformationActive());
    }
    IEnumerator SetInformationActive()
    {
        yield return new WaitForSeconds(5 * 10f);

        ang.SetInformationActive();
        this.transform.Find("features").Find("drawing").GetComponent<MonoBehaviour>().enabled = false;
    }
}
