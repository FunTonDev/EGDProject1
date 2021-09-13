using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public AudioSource navSource;
    public AudioClip choiceClip;
    public AudioClip confirmClip;

    public void MouseAudioTrigger(AudioClip tClip)
    {
        navSource.clip = tClip;
        navSource.PlayOneShot(tClip);
    }
}
