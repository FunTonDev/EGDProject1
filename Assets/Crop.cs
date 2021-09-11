using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    //Amount of time for the plant to spawn back into the game
    public float spawnTimer;
    //Max amount of time to spawn
    public float spawnTimerMax;
    //The amount of time left to water the plot
    public float waterTimer;
    //Max amount of time
    public float waterTimerMax;
    //Whether the plot has a crop or not
    public bool hasCrop;
    //The stage the crop is at (0 == not planted, 1 == seedling, 2 == sprout, 3 == mature, 4 == withered)
    public int cropLevel;
    //The image tied to the plot of land
    private SpriteRenderer plot;

    //Sprites to match the crops level
    public Sprite stage0;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    public Sprite stage4;

    //Prefab for water bar
    public GameObject waterBarPrefab;

    //Canvas to use for water bar
    public Canvas canvas;

    //Water bar image
    private Image waterBar;

    // Start is called before the first frame update
    void Start()
    {
        plot = GetComponent<SpriteRenderer>();

        GameObject waterBarObj = Instantiate(waterBarPrefab);
        waterBarObj.transform.SetParent(canvas.transform);
        waterBarObj.transform.position = GetComponent<Transform>().position + Vector3.up * 0.5f;
        waterBar = waterBarObj.GetComponent<Image>();

        removeCrop();
    }

    //Give the plot of land a crop (and change appearance)
    public void nextGrowthStage()
    {
        hasCrop = true;
        if (cropLevel < 3) cropLevel += 1;
        else cropLevel -= 1;
        if (cropLevel == 1)
        {
            plot.sprite = stage1;
        }
        else if (cropLevel == 2)
        {
            plot.sprite = stage2;
        }
        else if (cropLevel == 3)
        {
            plot.sprite = stage3;
        }
        waterTimer = 10;
        waterBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    
    //Make water bar disappear above crop
    public void scoreCrop()
    {
        waterTimerMax = 10;
        hasCrop = false;
        spawnTimer = spawnTimerMax;
        waterBar.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
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
            plot.sprite = stage4;
        }
        waterTimer = 10;
        waterBar.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCrop)
        {
            spawnTimer -= Time.deltaTime;
        }
        if (spawnTimer <= 0 && !hasCrop)
        {
            nextGrowthStage();
        }
        if (waterTimer > 0 && hasCrop)
            waterTimer -= Time.deltaTime;
        waterBar.fillAmount = waterTimer / waterTimerMax;
        if (waterTimer <= 0 && hasCrop)
        {
            hasCrop = false;
            plot.color = new Color(1.0f, 0.5f, 0.0f);
        }
    }
}
