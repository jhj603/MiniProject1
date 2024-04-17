using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;    
    public GameObject endTxt;

    AudioSource audioSource;

    public int cardCount = 0;
    float timeStack = 0f;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timeStack += Time.deltaTime;

        timeTxt.text = timeStack.ToString("N2");
    }

    public void Matched()       
    {
        if (firstCard.idx == secondCard.idx) 
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;     

            if (0 == cardCount)
            {
                Time.timeScale = 0f;
                endTxt.SetActive(true);
            }
        }
        else    
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
