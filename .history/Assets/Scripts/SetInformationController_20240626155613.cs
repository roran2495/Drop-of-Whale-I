using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInformationController : MonoBehaviour
{
    public SetInformationController2 setInformationController2;
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

        
        this.transform.Find("features").Find("drawing").GetComponent<MonoBehaviour>().enabled = false;
    }
}
