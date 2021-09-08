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
    //Text to display the current score
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 000";
        plots = GameObject.FindGameObjectsWithTag("Crop");
        if (plots.Length > 1)
        {
            for (int i = 0; i < plots.Length; i++)
            {
                int rando = Random.Range(0, 5);
                if (rando <= 1)
                {
                    plots[i].GetComponent<Crop>().getCrop();
                }
            }
        }
        else
        {
            plots[0].GetComponent<Crop>().getCrop();
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
        
    }
}
