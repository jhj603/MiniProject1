using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public SpriteRenderer frontImage;

    public GameObject goFront;
    public GameObject goBack; 

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    int matchedCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (0 <= matchedCount)
        {
            //프로퍼티 적용해야 함
            switch (GameManager.Instance.clickCount - matchedCount)
            {
                case 1:
                    goBack.GetComponent<SpriteRenderer>().color = new Color(82f / 255f, 82f / 255f, 82f / 255f, 255f / 255f);
                    break;
                case 2:
                    goBack.GetComponent<SpriteRenderer>().color = new Color(107f / 255f, 107f / 255f, 107f / 255f, 255f / 255f);
                    break;
                case 3:
                    goBack.GetComponent<SpriteRenderer>().color = new Color(157f / 255f, 157f / 255f, 157f / 255f, 255f / 255f);
                    break;
                case 4:
                    goBack.GetComponent<SpriteRenderer>().color = new Color(206f / 255f, 206f / 255f, 206f / 255f, 255f / 255f);
                    break;
                case 5:
                    goBack.GetComponent<SpriteRenderer>().color = new Color(224f / 255f, 224f / 255f, 224f / 255f, 255f / 255f);
                    break;
                default:
                    goBack.GetComponent<SpriteRenderer>().color = Color.white;
                    break;
            }
        }
    }

    public void Setting(int number)
    {
        idx = number;

        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");  
    }

    public void OpenCard()  
    {
        audioSource.PlayOneShot(clip);

        anim.SetBool("IsOpen", true);    

        if (null == GameManager.Instance.firstCard)
        {
            GameManager.Instance.firstCard = this;

            GameManager.Instance.SetFirst();
        }
        else
        {
            GameManager.Instance.secondCard = this;

            GameManager.Instance.Matched();
        }
    }

    public void DestroyCard()   //카드 삭제 함수
    {
        Invoke("DestroyCardInvoke", 1f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()     //카드 다시 뒤집는 함수
    {
        Invoke("CloseCardInvoke", 1f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("IsOpen", false);
    }

    //count 프로퍼티 적용해야 함
    public void SetCount(int _count)
    {
        matchedCount = _count;
    }
}
