using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;



public class OfflineJsonLoader : MonoBehaviour
{
    [SerializeField]
    TextAsset builtInJson;

    public delegate void JSONRefreshed(JSONNode json);
    public JSONRefreshed jsonRefreshed;

    public JSONNode currentJSON;
    // Use this for initialization
    void Start()
    {

        //currentJSON = JSON.Parse("{\"AsteroidStats\":" + builtInJson.text + "}");

        StartCoroutine(RefreshJSON());

        //print(asteroidID);

        //print(currentJSON[0][asteroidID]["orbit_class"]);

        //Debug.Log(jsonRefreshed);
        //jsonRefreshed.Invoke(currentJSON);
    }


    IEnumerator RefreshJSON()
    {
        currentJSON = JSON.Parse("{\"AsteroidStats\":" + builtInJson.text + "}"); ;
        yield return currentJSON;

        jsonRefreshed.Invoke(currentJSON);

        StopCoroutine(RefreshJSON());
    }

}
