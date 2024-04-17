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

    public GameObject FirstTxt;
    public GameObject SecondTxt;
    public GameObject ThirdTxt;
    public GameObject FourthTxt;
    public GameObject NotMatchTxt;

    public Text timeTxt;
    public GameObject endTxt;

    AudioSource audioSource;

    public int cardCount = 0;
    float timeStack = 0f;

    public int clickCount = 0;

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
            if (firstCard.idx == 0 || firstCard.idx == 1)
            {
                FirstTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }
            if (firstCard.idx == 2 || firstCard.idx == 3)
            {
                SecondTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }
            if (firstCard.idx == 4 || firstCard.idx == 5)
            {
                ThirdTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }
            if (firstCard.idx == 6 || firstCard.idx == 7)
            {
                FourthTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }

            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            NotMatchTxt.SetActive(true);
            Invoke("HideName", 0.5f);

            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

        private void HideName()
        {
            FirstTxt.SetActive(false);
            SecondTxt.SetActive(false);
            ThirdTxt.SetActive(false);
            FourthTxt.SetActive(false);
            NotMatchTxt.SetActive(false);
        }
}