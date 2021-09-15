using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    //A grouping of all plots on the stage
    public GameObject[] plots;

    //The current score
    public float score;
    //Score needed to get the rocket to disappear
    public int maxScoreRocket;
    //Score needed to get the rocket to appear 
    public int lessScoreRocket;
    //Score needed to fix earth (good end)
    public int maxScoreEnd;

    //Plant health needed to stabilize score
    public float stablePlantHealth;
    //Multiplier to delta score
    public float deltaScoreMultiplier;
    //Variable tracking current plant health
    public float currentPlantHealth;

    //Max amount of time countdown starts at
    public float maxCountdown = 10;
    //Current time in the countdown
    public float currentCountdown = 10;

    //Slider to track current environmental health
    public GameObject slider;
    //Background for countdown text
    public Image CountBack;
    //Countdown text
    public Text countDownText;
    //Bool to show whether the countdown should begin or not
    public bool ender;
    //Bool to show whether the game is over or not
    public bool theEnd;
    //Rocket Ship object
    public GameObject rocketShip;
    //UI image to fade out of the scene
    public Image fader;

    public GameObject leftBorder;
    public GameObject rightBorder;


    public AudioSource musicSource;
    public List<AudioClip> musicClips;
    public int currentMusic;

    // Start is called before the first frame update
    void Start()
    {
        plots = GameObject.FindGameObjectsWithTag("Crop");
        int x = Random.Range(0, plots.Length);
        plots[x].GetComponent<Crop>().nextGrowthStage();
        int add = 0;
        for (int i = 0; i < plots.Length; i++)
        {
            if (i != x)
            {
                plots[i].GetComponent<Crop>().spawnTimer += add;
                add += 7;
            }
        }
        currentMusic = -1;
        PlayGameMusic(0);
        currentPlantHealth = 0;
        currentCountdown = maxCountdown = 10;
    }

    public void updateScore()
    {
        Vector3 position = slider.transform.localPosition;
        position.x = 384 * (score - 50) / 100;

        slider.transform.localPosition = position;
    }

    //End the game (true == good end, false == bad end)
    public void stageEnd(bool status)
    {
        Cinematic endCinematic = status ? Cinematic.END1 : Cinematic.END2;
        GameManager.SetGameState(GameState.CINEMATIC, endCinematic);
    }


    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * deltaScoreMultiplier * (currentPlantHealth - stablePlantHealth);
        updateScore();
        if (ender && currentCountdown > 0 && score <= lessScoreRocket)
        {
            currentCountdown -= Time.deltaTime;
            countDownText.text = string.Format("Time until Blast Off: {0:#.00}", currentCountdown);
            countDownText.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else if (ender && currentCountdown > 0)
        {
            currentCountdown -= Time.deltaTime;
            countDownText.text = string.Format("Just a little more... {0:#.00}", currentCountdown);
            countDownText.color = new Color(0.0f, 1.0f, 0.0f);
        }

        countDownText.gameObject.SetActive(ender);
        CountBack.gameObject.SetActive(ender);
        leftBorder.GetComponent<BoxCollider2D>().enabled = !ender;

        //If score is low enough, summon rocket and begin countdown
        if (score <= lessScoreRocket && rocketShip.transform.position.y < -1)
        {
            ender = true;
            rocketShip.transform.position = new Vector3(rocketShip.transform.position.x, rocketShip.transform.position.y + 0.01f, rocketShip.transform.position.z);
            PlayGameMusic(1);
        }
        //If score is high enough, remove rocket and stop countdown
        else if (score >= maxScoreRocket && rocketShip.transform.position.y > -9)
        {
            ender = false;
            currentCountdown = maxCountdown;
            rocketShip.transform.position = new Vector3(rocketShip.transform.position.x, rocketShip.transform.position.y - 0.01f, rocketShip.transform.position.z);
            PlayGameMusic(0);
        }

        if (score >= maxScoreEnd)
        {
            ender = true;
        }
        else if (score >= maxScoreRocket && score < maxScoreEnd)
        {
            ender = false;
            currentCountdown = maxCountdown;
        }

        if (score >= maxScoreEnd && currentCountdown <= 0)
        {
            stageEnd(true);
        }
        else if (score <= lessScoreRocket && currentCountdown <= 0)
        {
            stageEnd(false);
        }
    }

    private void PlayGameMusic(int index)
    {
        if (currentMusic != index)
        {
            currentMusic = index;
            musicSource.Stop();
            musicSource.clip = musicClips[index];
            musicSource.Play();
        } 
    }
}
