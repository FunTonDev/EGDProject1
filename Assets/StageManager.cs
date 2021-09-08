using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //A grouping of all plots on the stage
    public GameObject[] plots;
    //The current score 
    public int score;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
