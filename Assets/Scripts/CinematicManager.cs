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
    public Text introText;

    void Start()
    {
        introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 0);
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

    private void TextSwitch(int part)
    {
        switch (part)
        {
            case 1:
                introText.text = "Our beautiful planet, our home, is dying.";
                break;
            case 2:
                introText.text = "After many years of overpopulation and depleting earth of its natural resources, the nations of the world have decided to come together to form a solution to this problem.";
                break;
            case 3:
                introText.text = "The answer: the creation of a space botanical organization, dubbed Earth’s Natural Development Team(END).";
                break;
            case 4:
                introText.text = "END searched far and wide, until they found their answer in South Dakota.";
                break;
            case 5:
                introText.text = "They enlist the help of you, the Gardener, to grow plants in efforts to save the world.";
                break;
            case 6:
                introText.text = "No pressure, right? Of course not; if the earth perishes then we just go to Mars and hope history doesn't repeat itself.";
                break;
            case 7:
                introText.text = "If all else fails, abandon ship, but do you really want to?";
                break;
        }
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
        yield return new WaitForSeconds(1.0f);
        introText.color = new Color(1, 1, 1, 1);
        TextSwitch(1);
        yield return new WaitForSeconds(5.0f);
        TextSwitch(2);
        yield return new WaitForSeconds(10.0f);
        TextSwitch(3);
        yield return new WaitForSeconds(7.0f);
        TextSwitch(4);
        yield return new WaitForSeconds(6.0f);
        TextSwitch(5);
        yield return new WaitForSeconds(6.0f);
        TextSwitch(6);
        yield return new WaitForSeconds(7.0f);
        TextSwitch(7);
        yield return StartCoroutine(CinematicEndBuffer(2.0f));
    }

    private IEnumerator CinematicEndBuffer(float time)
    {
        yield return new WaitForSeconds(time);
        continueButton.SetActive(true);
    }
}
