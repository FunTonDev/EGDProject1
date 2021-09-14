using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public AudioSource navSource;
    public AudioClip choiceClip;
    public AudioClip confirmClip;
    public Graphic bg;
    public Graphic ship;

    public void Start()
    {
        StartCoroutine(ParallaxPanLoop(bg, 12.0f));
        StartCoroutine(ParallaxPanLoop(ship, 4.0f));
    }

    public void MouseAudioTrigger(AudioClip tClip)
    {
        navSource.clip = tClip;
        navSource.PlayOneShot(tClip);
    }

    public void ButtonChoice(int index)
    {
        if (index == 0)
        {
            GameManager.SetGameState(GameState.CINEMATIC);
        }
        else
        {
            Application.Quit();
        }
    }

    IEnumerator ParallaxPanLoop(Graphic tGraphic, float displace = 1.0f)
    {
        float xNew, yNew;
        float xOrigin = tGraphic.rectTransform.localPosition.x;
        float yOrigin = tGraphic.rectTransform.localPosition.y;
        while (true)
        {
            float xTarget = Random.Range(xOrigin - displace, xOrigin + displace);
            float yTarget = Random.Range(yOrigin - displace, yOrigin + displace);
            float xChange = Mathf.Sign(xTarget - tGraphic.rectTransform.localPosition.x) * 0.25f;
            float yChange = Mathf.Sign(yTarget - tGraphic.rectTransform.localPosition.y) * 0.25f;

            while (Mathf.Abs(xTarget - tGraphic.rectTransform.localPosition.x) > 0.5f && Mathf.Abs(yTarget - tGraphic.rectTransform.localPosition.y) > 0.5f)
            {
                xNew = Mathf.Abs(xTarget - tGraphic.rectTransform.localPosition.x) > 0.5f ? tGraphic.rectTransform.localPosition.x + xChange : tGraphic.rectTransform.localPosition.x;
                yNew = Mathf.Abs(yTarget - tGraphic.rectTransform.localPosition.y) > 0.5f ? tGraphic.rectTransform.localPosition.y + yChange : tGraphic.rectTransform.localPosition.y;
                tGraphic.rectTransform.localPosition = new Vector3(xNew, yNew, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
