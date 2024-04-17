using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
     public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LevelUpRetry()
    {
        GameManager.level++;
        SceneManager.LoadScene("MainScene");
    }
}