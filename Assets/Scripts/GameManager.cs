using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int level = 3;

    public Card firstCard;
    public Card secondCard;

    public GameObject FirstTxt;
    public GameObject SecondTxt;
    public GameObject ThirdTxt;
    public GameObject FourthTxt;
    public GameObject NotMatchTxt;

    public Text timeTxt;

    public GameObject failEndTxt;
    public GameObject clearEndTxt;
    public Text resultTxt;
    public Text matchTryTxt;
    public Text scoreTxt;

    AudioSource audioSource;
    public AudioClip clip;

    public Animator anim;

    public int cardCount = 0;
    int matchTryCount = 0;
    float time = 30.0f;
    float score = 100.0f;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Warning(20.0f));
        timeTxt.rectTransform.anchoredPosition = new Vector3(0, 250 + 50 * level, 0);
    }

    IEnumerator Warning(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isWarn", true);
    }


    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        score -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if(time <= 0.0f)
        {
            endGame(false);
        }
    }

    public void Matched()
    {
        matchTryCount++;
        if(matchTryCount > 4 * level) score -= 1;
        
        if (firstCard.idx == secondCard.idx)
        {
            if (firstCard.idx == 0 || firstCard.idx == 1 || firstCard.idx == 2)
            {
                FirstTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }
            if (firstCard.idx == 3 || firstCard.idx == 4 || firstCard.idx == 5)
            {
                SecondTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }
            if (firstCard.idx == 6 || firstCard.idx == 7 || firstCard.idx == 8)
            {
                ThirdTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }
            if (firstCard.idx == 9 || firstCard.idx == 10 || firstCard.idx == 11)
            {
                FourthTxt.SetActive(true);
                Invoke("HideName", 0.5f);
            }

            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount == 0)
            {
                endGame(true);
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

    void endGame(bool isClear)
    {
        if(isClear) 
        {
            clearEndTxt.SetActive(true);
            resultTxt.text = "성공";
            resultTxt.gameObject.SetActive(true);
            matchTryTxt.text = "매칭 시도: " + matchTryCount.ToString();
            matchTryTxt.gameObject.SetActive(true);
            scoreTxt.text = "점수: " + score.ToString("N2");
            scoreTxt.gameObject.SetActive(true);
        }
        else
        {
            failEndTxt.SetActive(true);
            resultTxt.gameObject.SetActive(true);
        }
        Time.timeScale = 0.0f;
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