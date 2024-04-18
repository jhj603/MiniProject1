using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HardBtn : MonoBehaviour
{
    public GameObject goLock;
    public GameObject goUnlock;

    int needLevel = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (needLevel > GameManager.Level)
        {
            goLock.SetActive(true);
            goUnlock.SetActive(false);
        }
        else
        {
            goLock.SetActive(false);
            goUnlock.SetActive(true);

            goUnlock.GetComponent<Button>().onClick.AddListener(() => HardChoose());
        }
    }

    public void HardChoose()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Level = 5;
    }
}