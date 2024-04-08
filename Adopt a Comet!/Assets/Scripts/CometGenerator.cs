using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static Unity.VisualScripting.Member;


public class CometGenerator : MonoBehaviour
{
    GameObject currentAsteroid = null;

    JSONLoader jsonLoader;

    OfflineJsonLoader offlineJsonLoader;


    [SerializeField]
    public GameObject asteroidModel;

    [SerializeField]
    public GameObject cometModel;
    
    [SerializeField]
    public GameObject eyeModel;

    


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hai!");

        jsonLoader = GetComponent<JSONLoader>();

        jsonLoader.jsonRefreshed += ReadJSON;

       
        
        
        
        offlineJsonLoader = GetComponent<OfflineJsonLoader>();

        offlineJsonLoader.jsonRefreshed += ReadOfflineJSON;

        //Debug.Log(offlineJsonLoader.currentJSON);

        




        
    }

    private void OnDestroy()
    {
        jsonLoader.jsonRefreshed -= ReadJSON;

        offlineJsonLoader.jsonRefreshed -= ReadJSON;
    }

    // Update is called once per frame
    public void ReadJSON(JSONNode json)
    {
        

        Debug.Log(json[0]);

        //print(json["abilities"]);
        //print(json["abilities"][0]["ability"]["name"]);
        //tX_Name.text = json["name"];
        //string imageURL = json["sprites"]["other"]["home"]["front_default"];
        //print(imageURL);
        //StartCoroutine(DownloadImage(json[0]));
    }
    
    public void ReadOfflineJSON(JSONNode json)
    {
        

        //Debug.Log(json[0]);

        //int asteroidID = (int)UnityEngine.Random.Range(0, 200);

        //print(asteroidID);

        //(json[0][asteroidID]["orbit_class"]);

        GenerateAsteroid();

        //tX_Name.text = json["name"];
        //string imageURL = json["sprites"]["other"]["home"]["front_default"];
        //print(imageURL);
        //StartCoroutine(DownloadImage(imageURL));
    }

    public void ReadOfflineMTGJSON(JSONNode json)
    {


        //Debug.Log(json[0]);
        //tX_Name.text = json["name"];
        //string imageURL = json["sprites"]["other"]["home"]["front_default"];
        //print(imageURL);
        //StartCoroutine(DownloadImage(imageURL));
    }



    public void GenerateAsteroid() {
        Debug.Log(offlineJsonLoader);

        JSONNode AsteroidData = offlineJsonLoader.currentJSON[0][(int)UnityEngine.Random.Range(0, 200)];

        
        
        if (currentAsteroid != null)
        {
            Destroy(currentAsteroid);
        }


        GameObject newAsteroid;
        
        if (AsteroidData["orbit_class"].ToString().ToLower().Contains("comet"))
        {
            Debug.Log("It's a comet, baby!");

            newAsteroid = Instantiate(cometModel);
        }
        else
        {
            Debug.Log("We're rockin' baby!");

            newAsteroid = Instantiate(asteroidModel);
        }
  
        

        Asteroid astroScript = newAsteroid.GetComponent<Asteroid>();

        Debug.Log(jsonLoader.currentJSON.Count);



        int asteroidNameIndex = (int)Random.Range(10, 1800);



        foreach (var name in jsonLoader.currentJSON)
        {
            asteroidNameIndex--;

            string possibleName = name.Key;

            if (asteroidNameIndex <= 0 && possibleName.Split('_').Length - 1 < 2)
            {
                astroScript.asteroidName = possibleName.Replace("_","").ToUpper();

                break;
            }

            Debug.Log(name.Key);
        }

        astroScript.orbit_class = AsteroidData["orbit_class"].ToString();

        astroScript.moodEmojiURl = jsonLoader.currentJSON[(int)Random.Range(0,1800)];

        CometDisplay.Utilities.CometDisplay.instance.DisplayCometStats(astroScript);

        //Finally, we need to make some eyes and randomize the colors!

        GameObject eye1 = Instantiate(eyeModel);

        eye1.transform.position = newAsteroid.transform.position + new Vector3(-.6f + Random.Range(-.1f,.1f) ,1 + Random.Range(-.1f, .1f), -1);

        eye1.transform.parent = newAsteroid.transform;

        GameObject eye2 = Instantiate(eyeModel);

        eye2.transform.position = newAsteroid.transform.position + new Vector3(.6f + Random.Range(-.1f, .1f), 1 + Random.Range(-.1f, .1f), -1);

        eye2.transform.parent = newAsteroid.transform;



        currentAsteroid = newAsteroid;

    }


}
