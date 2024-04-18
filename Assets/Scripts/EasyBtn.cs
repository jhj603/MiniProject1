using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyBtn : MonoBehaviour
{
    public void EasyChoose()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Level = 3;
    }
}
