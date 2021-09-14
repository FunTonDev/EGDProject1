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
    //Score that the ending countdown starts at (bad end)
    public int lowScoreEnd;

    //Plant health needed to stabilize score
    public float stablePlantHealth;
    //Multiplier to delta score
    public float deltaScoreMultiplier;
    //Variable tracking current plant health
    public float currentPlantHealth;

    //Max amount of time countdown starts at
    public float maxCountdown = 10;
    //Current time in the countdown
    private float currentCountdown = 10;

    //Text to display the current score
    public Text scoreText;
    //Rocket Ship object
    public GameObject rocketShip;
    //UI image to fade out of the scene
    public Image fader;

    public GameObject leftBorder;
    public GameObject rightBorder;


    public AudioSource musicSource;
    public List<AudioClip> musicClips;


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 000";
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
        musicSource.clip = musicClips[0];
        musicSource.Play();
        currentPlantHealth = 0;
    }

    public void updateScore()
    {
        scoreText.text = "Score: " + score;
        scoreText.color = new Color(1.0f, 1.0f, 1.0f);
    }

    public void stageEnd(bool status)
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * deltaScoreMultiplier * (currentPlantHealth - stablePlantHealth);
        updateScore();

        if (score <= lessScoreRocket && rocketShip.transform.position.y < -1)
        {
            rocketShip.transform.position = new Vector3(rocketShip.transform.position.x, rocketShip.transform.position.y + 0.01f, rocketShip.transform.position.z);
        }
        else if (score >= maxScoreRocket && rocketShip.transform.position.y > -9)
        {
            rocketShip.transform.position = new Vector3(rocketShip.transform.position.x, rocketShip.transform.position.y - 0.01f, rocketShip.transform.position.z);
        }
        if (currentCountdown <= 0)
        {
            //End the stage (bad end)

        }
    }
}
