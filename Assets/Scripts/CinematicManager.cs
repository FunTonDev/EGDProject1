using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    public Image cinematicImage;
    public AudioSource mainSource;
    public AudioSource auxSource;
    public List<AudioClip> cinematicClips;
    public List<Sprite> introSprites;
    public List<Sprite> end1Sprites;
    public List<Sprite> end2Sprites;

    public bool cinematicComplete;

    void Start()
    {
        cinematicComplete = false;
        switch(GameManager.cinematicChosen)
        {
            case Cinematic.INTRO:
                IntroCinematic();
                break;
            case Cinematic.END1:
                End1Cinematic();
                break;
            case Cinematic.END2:
                End2Cinematic();
                break;
        }
    }

    private void Update()
    {
        if (cinematicComplete && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            if (GameManager.cinematicChosen == Cinematic.INTRO)
            {
                GameManager.SetGameState(GameState.GAME);
            }
            else
            {
                GameManager.SetGameState(GameState.MENU);
            }
        }
    }

    private void IntroCinematic()
    {
        LoopPlayClip(mainSource, cinematicClips[0]);

        StartCoroutine(AnimationLoop(introSprites));
        StartCoroutine(IntroTextSequence());
    }

    private void End1Cinematic()
    {
        LoopPlayClip(mainSource, cinematicClips[1], 0.2f);
        LoopPlayClip(auxSource, cinematicClips[3]);

        StartCoroutine(AnimationLoop(end1Sprites));
        StartCoroutine(CinematicTimeBuffer(6.0f));
    }

    private void End2Cinematic()
    {
        LoopPlayClip(mainSource, cinematicClips[2]);
        LoopPlayClip(auxSource, cinematicClips[4]);

        StartCoroutine(AnimationLoop(end2Sprites));
        StartCoroutine(CinematicTimeBuffer(6.0f));
    }

    private void LoopPlayClip(AudioSource tSource, AudioClip tClip, float tVolume = 1.0f)
    {
        tSource.loop = true;
        tSource.volume = tVolume;
        tSource.clip = tClip;
        tSource.Play();
    }

    private IEnumerator AnimationLoop(List<Sprite> animationSprites)
    {
        for(int index = 0;;index++)
        {
            if(index == animationSprites.Count)
            {
                index = 0;
            }
            cinematicImage.sprite = animationSprites[index];
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IntroTextSequence()
    {

        yield return StartCoroutine(CinematicTimeBuffer(2.0f));
    }
    private IEnumerator CinematicTimeBuffer(float time)
    {
        yield return new WaitForSeconds(time);
        cinematicComplete = true;
    }
}
