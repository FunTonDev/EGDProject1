using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] plots;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        plots = GameObject.FindGameObjectsWithTag("Crop");
        for (int i = 0; i < plots.Length; i++)
        {
            int rando = Random.RandomRange(0, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
