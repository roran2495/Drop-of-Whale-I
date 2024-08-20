using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInformationController : MonoBehaviour
{
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
        yield return new WaitForSeconds(5 * 60f);

        this.transform.Find("information").gameObject.SetActive(true);
        this.transform.Find("drawing").GetComponent<Sprit
    }
}
