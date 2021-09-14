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
    public GameObject continueButton;
    public GameObject gameGuide;

    void Start()
    {
        gameGuide.SetActive(false);
        continueButton.SetActive(false);
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
        StartCoroutine(CinematicEndBuffer(6.0f));
    }

    private void End2Cinematic()
    {
        LoopPlayClip(mainSource, cinematicClips[2]);
        LoopPlayClip(auxSource, cinematicClips[4]);

        StartCoroutine(AnimationLoop(end2Sprites));
        StartCoroutine(CinematicEndBuffer(6.0f));
    }

    private void LoopPlayClip(AudioSource tSource, AudioClip tClip, float tVolume = 1.0f)
    {
        tSource.loop = true;
        tSource.volume = tVolume;
        tSource.clip = tClip;
        tSource.Play();
    }

    public void NextSceneContinue()
    {
        if (GameManager.cinematicChosen == Cinematic.INTRO)
        {
            if (!gameGuide.activeInHierarchy)
            {
                gameGuide.SetActive(true);
            }
            else
            {
                GameManager.SetGameState(GameState.GAME);
            }
        }
        else
        {
            GameManager.SetGameState(GameState.MENU);
        }
    }

    public void MouseAudioTrigger(AudioClip tClip)
    {
        auxSource.PlayOneShot(tClip);
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

        yield return StartCoroutine(CinematicEndBuffer(2.0f));
    }

    private IEnumerator CinematicEndBuffer(float time)
    {
        yield return new WaitForSeconds(time);
        continueButton.SetActive(true);
    }
}
