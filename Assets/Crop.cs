using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    //The amount of time left to water the plot
    public float waterTimer;
    //Max amount of time
    public float waterTimerMax;
    //Whether the plot has a crop or not
    public bool hasCrop;
    //The image tied to the plot of land
    public SpriteRenderer plot;
    //A bar to show how much time is left to water the crop
    public Image waterBar;

    // Start is called before the first frame update
    void Start()
    {
        waterTimerMax = 5;
        waterTimer = 5;
    }

    //Give the plot of land a crop
    public void getCrop()
    {
        hasCrop = true;
        plot.color = new Color(0.0f, 1.0f, 0.0f);
        waterTimer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        waterTimer -= Time.deltaTime;
        waterBar.fillAmount = waterTimer / waterTimerMax;
        if (waterTimer <= 0 && hasCrop)
        {
            hasCrop = false;
            plot.color = new Color(1.0f, 0.5f, 0.0f);
        }
    }
}
