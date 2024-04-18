using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource audioSource;
    public AudioClip bgmClip;
    public AudioClip originalBGMClip;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgmClip;
        audioSource.Play();
    }

    public void PlayBGM(AudioClip newClip)
    {
        audioSource.Stop();

        audioSource.clip = newClip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlayOriginal()
    {
        if (null != audioSource)
        {
            audioSource.Stop();

            audioSource.clip = originalBGMClip;
            audioSource.Play();
        }
    }
}
