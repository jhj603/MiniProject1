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
    public GameObject endTxt;

    AudioSource audioSource;
    public AudioClip matchClip;
    public AudioClip failClip;
    public AudioClip finishClip;

    public int cardCount = 0;
    float timeStack = 0f;

    //프로퍼티 적용해야 함
    public int clickCount = 0;

    float firstTime = 0f;
    bool isFirst = false;

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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1f;

        clickCount = 0;
        timeStack = 0f;
        firstTime = 0f;
        isFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeStack += Time.deltaTime;

        timeTxt.text = timeStack.ToString("N2");

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

        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(matchClip);

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
                AudioManager.Instance.StopBGM();

                audioSource.PlayOneShot(finishClip);

                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
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
        }

        firstCard = null;
        secondCard = null;

        ++clickCount;
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