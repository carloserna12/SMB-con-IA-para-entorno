using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class ApiRequest : MonoBehaviour
{
    //private GameStateManager t_GameStateManager;
    public GameStateManager listApi;
    public static List<string> Api = new List<string>();
    
    public static IEnumerator startApi()
    {
        //t_GameStateManager = FindObjectOfType<GameStateManager>();

        string apiURL = "https://pokeapi.co/api/v2/pokemon/151";
        UnityWebRequest apiInfoRequest = UnityWebRequest.Get(apiURL);  
        yield return apiInfoRequest.SendWebRequest();


        if (apiInfoRequest.isNetworkError || apiInfoRequest.isHttpError)
            {
                Debug.LogError(apiInfoRequest.error);
                yield break;
            }


        JSONNode apiInfo = JSON.Parse(apiInfoRequest.downloadHandler.text);

        string testName = apiInfo["name"]; 
        Debug.Log("Hizo la peticion y retorno: " + testName);
        Api.Add("api" + testName);
        //send();
    
    }

    public  void send(){
        
        listApi.testApi.AddRange(Api);
    }

    // Update is called once per frame

}
