using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormalBtn : MonoBehaviour
{
    public GameObject goLock;
    public GameObject goUnlock;

    int needLevel = 4;

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

            goUnlock.GetComponent<Button>().onClick.AddListener(() => NormalChoose());
        }
    }
    public void NormalChoose()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Level = 4;
    }
}
