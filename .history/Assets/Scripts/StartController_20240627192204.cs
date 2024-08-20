using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    public GameObject logo1;
    public GameObject logo2;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        logo1.SetActive(false);
        logo2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetLogo());
    }
    IEnumerator SetLogo()
    {
        yield return new WaitForSeconds(5.0f);

        logo1.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        logo2.SetActive(true);
        background.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("start/background0");
    }
    public void HandleButton1Click()
    {
        Screen.Load("");
    }

}
