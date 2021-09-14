using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class bulletinScript : MonoBehaviour
{
    public GameObject text;
    public float startSec;
    public float endIncSec;
    public float textSpeed;
    public float endX;
    List<string> textList = new List<string>();
    int coCount = 0;

    public float maxSpeedSmoth;

    Vector3 permVect;// = text.transform.position;

    public float waitTime;
    //public GameObject negText;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    Vector3 targetVect;// = new Vector3(endX, text.transform.position.y, text.transform.position.z);

    //Vector3 permVect = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    IEnumerator cycleText() 
    {
        while (true)
        {
            Debug.Log("START COROUTINE NUMBER: " + coCount);

            float returnT = Random.Range(startSec, endIncSec);//0.7f;
            int index = Random.Range(0, textList.Count);

            Debug.Log("This is SECONDS OWOWO: " + returnT);
            Debug.Log("THIS IS INDEX: " + index);

            text.GetComponent<Text>().text = textList[index];





            float permX = text.transform.position.x;

            

            float tmpX = text.transform.position.x;



            //Vector3 targetVect = new Vector3(endX, text.transform.position.y, text.transform.position.z);

            Debug.Log("HASNT CRASHED YET");

            //Vector3 moveVect = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            Vector3 moveVect;

            while (text.transform.position.x > targetVect.x)
            {

                //
                tmpX -= (1 * textSpeed);
                //text.transform.position.x = permX;
                text.transform.Translate((-1 * textSpeed), 0f, 0f);
                //text.transform.position = new Vector3(tmpX, text.transform.position.y, text.transform.position.z);
                //text.transform.position = Vector3.SmoothDamp(text.transform.position, targetVect, ref velocity, smoothTime);//, maxSpeedSmoth);
                //yield return null;
                //transform.position = moveVect;//Vector3.SmoothDamp(transform.position, targetVect, ref velocity, smoothTime);


                Debug.Log("this is Target Vec: " + targetVect);

                Debug.Log("this is Text Vec: " + permVect);

                yield return new WaitForSeconds(waitTime);


            }

            Debug.Log("WHILE LOOP SUCCESSFUL");

            //transform.Translate(permX, 0f, 0f);
            text.transform.position = permVect;





            //moveText();

            coCount += 1;

            //
            Debug.Log("ABOUT TO WAIT!!!");
            yield return new WaitForSeconds(returnT);
        }

    }

    void moveText() 
    {


        //
        float permX = text.transform.position.x;
        float tmpX = text.transform.position.x;

        Vector3 permVect = new Vector3(text.transform.position.x, text.transform.position.y, text.transform.position.z);

        Vector3 targetVect = new Vector3(endX, text.transform.position.y, text.transform.position.z);

        Debug.Log("HASNT CRASHED YET");

        while (text.transform.position.x > targetVect.x) 
        {

            //
            //tmpX -= (1 * textSpeed);
            //text.transform.position.x = permX;
            //transform.Translate((-1 * textSpeed), 0f, 0f);
            // transform.position = new Vector3(tmpX, transform.position.y, transform.position.z);
            //yield return null;
            transform.position = Vector3.SmoothDamp(transform.position, targetVect, ref velocity, smoothTime);



        }

        Debug.Log("WHILE LOOP SUCCESSFUL");

        //transform.Translate(permX, 0f, 0f);
        transform.position = permVect;


        

    }

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<Text>().text = "";
        //negText.GetComponent<Text>().text = "";

        permVect = text.transform.position;

        targetVect = new Vector3(endX, text.transform.position.y, text.transform.position.z);

        Debug.Log("this is Target Vec: "+ targetVect);

        Debug.Log("this is Text Vec: " + permVect);

        string path = "Assets/bulletPrompts.txt";
        StreamReader reader = new StreamReader(path);

        string stuff = reader.ReadLine();

        while (stuff != null) 
        {
            //if(stuff != null) 
            //{

            //Debug.Log(stuff);
            textList.Add(stuff);

            //}
            
            stuff = reader.ReadLine();
        }

       
        reader.Close();



        StartCoroutine(cycleText());

        //textList.Add();
    }

    // Update is called once per frame

   /* void Update()
    {

        float permX = text.transform.position.x;
        float tmpX = text.transform.position.x;



        Vector3 targetVect = new Vector3(endX, text.transform.position.y, text.transform.position.z);

        Debug.Log("HASNT CRASHED YET");

        Vector3 moveVect = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);

        // while (transform.position.x > targetVect.x)
        // {

        //
        //tmpX -= (1 * textSpeed);
        //text.transform.position.x = permX;
        //transform.Translate((-1 * textSpeed), 0f, 0f);
        // transform.position = new Vector3(tmpX, transform.position.y, transform.position.z);
        //yield return null;
        text.transform.position = Vector3.SmoothDamp(text.transform.position, targetVect, ref velocity, smoothTime);



        // }

        Debug.Log("WHILE LOOP SUCCESSFUL");

        //transform.Translate(permX, 0f, 0f);
        //transform.position = permVect;


    }*/

}
