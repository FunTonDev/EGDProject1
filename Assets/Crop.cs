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
    //The stage the crop is at (0 == not planted, 1 == seedling, 2 == sprout, 3 == mature, 4 == withered)
    public int cropLevel;
    //The image tied to the plot of land
    public SpriteRenderer plot;
    //A bar to show how much time is left to water the crop
    public Image waterBar;

    // Start is called before the first frame update
    void Start()
    {
        waterTimerMax = 10;
        waterTimer = 10;
        removeCrop();
    }

    //Give the plot of land a crop (and change appearance)
    public void getCrop()
    {
        hasCrop = true;
        if (cropLevel < 3) cropLevel += 1;
        if (cropLevel == 1)
        {
            
        }
        else if (cropLevel == 2)
        {

        }
        else if (cropLevel == 3)
        {

        }
        plot.color = new Color(0.0f, 1.0f, 0.0f);
        waterTimer = 10;
        waterBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    //Remove crop from plot of land
    public void removeCrop()
    {
        if (cropLevel < 3)
        {
            hasCrop = false;
        }
        else
        {
            cropLevel = 4;
        }
        plot.color = new Color(1.0f, 0.5f, 0.0f);
        waterTimer = 10;
        waterBar.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (waterTimer > 0)
            waterTimer -= Time.deltaTime;
        waterBar.fillAmount = waterTimer / waterTimerMax;
        if (waterTimer <= 0 && hasCrop)
        {
            hasCrop = false;
            plot.color = new Color(1.0f, 0.5f, 0.0f);
        }
    }
}
