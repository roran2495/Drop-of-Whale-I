using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class StartController : MonoBehaviour
{
    public GameObject logo1;
    public GameObject logo2;
    public GameObject background;
    public GameObject infor;
    public tmp
    // Start is called before the first frame update
    void Start()
    {
        logo1.SetActive(false);
        logo2.SetActive(false);
        infor.SetActive(false);
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
        StartCoroutine(StartGames());
    }
    public void HandleButton2Click()
    {

    }
    public void HandleButton3Click()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    IEnumerator StartGames()
    {
        infor.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainScenes");
    }
}
