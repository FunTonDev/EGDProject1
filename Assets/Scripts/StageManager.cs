using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    //A grouping of all plots on the stage
    public GameObject[] plots;
    //The current score 
    public int score;
    //Score needed to get the rocket to appear (continue)
    public int maxScoreRocket;
    //Score needed to fix earth (good end)
    public int maxScoreEnd;
    //Text to display the current score
    public Text scoreText;


    public GameObject rocketShip;

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
    }

    public void updateScore()
    {
        scoreText.color = new Color(0.0f, 1.0f, 0.0f);
        scoreText.text = "Score: " + score;
        scoreText.color = new Color(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= maxScoreRocket && rocketShip.transform.position.y < -1)
        {
            rocketShip.transform.position = new Vector3(rocketShip.transform.position.x, rocketShip.transform.position.y + 0.01f, rocketShip.transform.position.z);
        }
    }
}
