using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject infor;
    public GameObject button;
    public GameObject menu;
    public Transform collectList;
    private List<ColliderState> colliderStates = new List<ColliderState>();
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        button.SetActive(true);
        infor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            HandleAppPaused();
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 应用被暂停，例如用户上划退出
            HandleAppPaused();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // 应用失去焦点，例如用户上划退出
            HandleAppPaused();
        }
    }

    void HandleAppPaused()
    {
        // 显示菜单
        menu.SetActive(true);
        Time.timeScale = 0f;
        SaveColliderStates();
        DisableAllColliders();
        DisableEventTriggersRecursive(collectList);
    }
    public void HandleButton1Click()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        RestoreColliderStates();
        EnableEventTriggersRecursive(collectList);
    }
    public void HandleButton2Click()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
        // 还需要缓存进度信息
    }
    public void HandleButton3Click()
    {
        button.SetActive(false);
        infor.SetActive(true);
    }
    public void HandleButton4Click()
    {
        button.SetActive(true);
        infor.SetActive(false);
    }

    private void SaveColliderStates()
    {
        colliderStates.Clear();
        Collider[] colliders = FindObjectsOfType<Collider>();
        foreach (Collider collider in colliders)
        {
            colliderStates.Add(new ColliderState(collider, collider.enabled));
        }
    }
    private void DisableAllColliders()
    {
        Collider[] colliders = FindObjectsOfType<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void RestoreColliderStates()
    {
        foreach (ColliderState state in colliderStates)
        {
            state.collider.enabled = state.isEnabled;
        }
        ClearColliderStates();
    }
    private void ClearColliderStates()
    {
        colliderStates.Clear();
    }
    void DisableEventTriggersRecursive(Transform parent)
    {
        // 禁用当前对象上的 EventTrigger
        EventTrigger[] eventTriggers = parent.GetComponents<EventTrigger>();
        foreach (EventTrigger eventTrigger in eventTriggers)
        {
            eventTrigger.enabled = false;
        }

        // 递归禁用子对象的 EventTrigger
        foreach (Transform child in parent)
        {
            DisableEventTriggersRecursive(child);
        }
    }
    void EnableEventTriggersRecursive(Transform parent)
    {
        // 禁用当前对象上的 EventTrigger
        EventTrigger[] eventTriggers = parent.GetComponents<EventTrigger>();
        foreach (EventTrigger eventTrigger in eventTriggers)
        {
            eventTrigger.enabled = true;
        }

        // 递归禁用子对象的 EventTrigger
        foreach (Transform child in parent)
        {
            EnableEventTriggersRecursive(child);
        }
    }
}
