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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int number)
    {
        idx = number;

        frontImage.sprite = Resources.Load<Sprite>($"img{idx}");  
    }

    public void OpenCard()  
    {
        anim.SetBool("IsOpen", true);    

        goFront.SetActive(true);     
        goBack.SetActive(false);     

        if (null == GameManager.Instance.firstCard)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;

            GameManager.Instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("IsOpen", false);

        goFront.SetActive(false);
        goBack.SetActive(true);
    }
}
