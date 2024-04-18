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
    bool isUnlock = false;

    // Start is called before the first frame update
    void Start()
    {
        goLock.SetActive(true);
        goUnlock.SetActive(false);

        goUnlock.GetComponent<Button>().onClick.AddListener(() => NormalChoose());
    }

    private void Update()
    {
        if (!isUnlock && (needLevel <= GameManager.Level))
        {
            goLock.SetActive(false);
            goUnlock.SetActive(true);

            isUnlock = true;
        }
    }

    public void NormalChoose()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Level = 4;
    }
}
