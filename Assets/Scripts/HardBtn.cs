using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class HardBtn : MonoBehaviour
{
    public GameObject goLock;
    public GameObject goUnlock;

    int needLevel = 5;
    bool isUnlock = false;

    // Start is called before the first frame update
    void Start()
    {
        goLock.SetActive(true);
        goUnlock.SetActive(false);

        goUnlock.GetComponent<Button>().onClick.AddListener(() => HardChoose());
    }

    private void Update()
    {
        if (!isUnlock && (needLevel <= GameManager.HighLevel))
        {
            goLock.SetActive(false);
            goUnlock.SetActive(true);

            isUnlock = true;
        }
    }

    public void HardChoose()
    {
        SceneManager.LoadScene("MainScene");

        GameManager.HighLevel = needLevel;
    }
}
