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

    public GameObject WarningTxt;
    public GameObject WarningTimeTxt;

    public Text timeTxt;

    public GameObject failEndTxt;
    public GameObject clearEndTxt;
    public Text resultTxt;
    public Text matchTryTxt;
    public Text scoreTxt;

    AudioSource audioSource;

    public AudioClip matchClip;
    public AudioClip failClip;
    public AudioClip finishClip;
    public AudioClip changeBgmClip;

    public int cardCount = 0;

    public int clickCount = 0;

    public Animator anim;

    int matchTryCount = 0;
    float time = 30.0f;
    float score = 100.0f;

    float firstTime = 0f;
    bool isFirst = false;
    bool isChangeBGM = false;

    static int level = 3;
    public static int Level
    { 
        get { return level; }
        set { level = value; }
    }

    public void SetFirst()
    { 
        isFirst = true; 
        WarningTxt.SetActive(true); 
        WarningTimeTxt.SetActive(true);
    }

    void ResetFirst()
    {
        WarningTxt.SetActive(false);
        WarningTimeTxt.SetActive(false);

        firstTime = 0f;
        isFirst = false;
    }

    private void Awake()
    {
        if (null == Instance)
            Instance = this;
    }

    void ResetGame()
    {
        switch (level)
        {
            case 3:
                time = 60f;
                break;
            case 4:
            case 5:
                time = 30f;
                break;
        }

        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();

        clickCount = 0;
        
        firstTime = 0f;
        isFirst = false;

        timeTxt.rectTransform.anchoredPosition = new Vector3(0, 250 + 50 * level, 0);

        AudioManager.Instance.PlayOriginal();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        score -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time <= 0.0f)
        {
            endGame(false);
        }

        if (!isChangeBGM && (10f >= time))
        {
            anim.SetBool("isWarn", true);

            AudioManager.Instance.PlayBGM(changeBgmClip);
            isChangeBGM = true;
        }

        if (isFirst)
        {
            firstTime += Time.deltaTime;

            WarningTimeTxt.GetComponent<Text>().text = firstTime.ToString("N2");

            if (4f < firstTime)
            {
                Invoke("ResetFirst", 1f);

                if (null != firstCard)
                {
                    firstCard.CloseCard();

                    firstCard = null;
                }
            }
        }
    }

    public void Matched()
    {
        if (isFirst)
            ResetFirst();

        matchTryCount++;

        if(matchTryCount > 4 * level) 
            score -= 1;
        
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchClip);

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
                AudioManager.Instance.StopBGM();

                audioSource.PlayOneShot(finishClip);

                clearEndTxt.SetActive(true);
                Time.timeScale = 0.0f;
                
                endGame(true);

                ++level;
            }
        }
        else
        {
            audioSource.PlayOneShot(failClip);

            firstCard.SetCount(clickCount);
            secondCard.SetCount(clickCount);

            NotMatchTxt.SetActive(true);
            Invoke("HideName", 0.5f);

            firstCard.CloseCard();
            secondCard.CloseCard();

            time -= 1.0f;
            if (time <= 0.0f)
            {
                time = 0.0f;
            }
        }

        firstCard = null;
        secondCard = null;

        ++clickCount;
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