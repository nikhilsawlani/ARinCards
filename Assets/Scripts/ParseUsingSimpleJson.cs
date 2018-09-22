using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ParseUsingSimpleJson : MonoBehaviour
{
    string jsonData;

    void Start()
    {
        StartCoroutine(Startparsing());
    }
    // Use this for initialization
    IEnumerator Startparsing()
    {
        // implememt WWW to get json data from any url
        string url = "https://unitytest-1489946952578.firebaseio.com/.json";
        WWW www = new WWW(url);
        yield return www;

        // store text in www to json string
        if (string.IsNullOrEmpty(www.error))
        {
            jsonData = www.text;
        }

        // use simpleJSON to get values stored in JSON data for different key value pair
        JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);

        //Debug.Log("Bank 0 " + jsonNode["Bank"][0].ToString());
        //Debug.Log("Bank 1 " + jsonNode["Bank"][1].ToString());
        //Debug.Log("Bank 1 " + jsonNode["Bank"][2].ToString());

        // get individual values from Mini-statement
        Debug.Log("Transation 1 name " + jsonNode["Bank"][0][0][0][1].ToString());
        Debug.Log("Transation 1 amt " + jsonNode["Bank"][0][0][0][0].ToString());
        Debug.Log("Transation 2 name " + jsonNode["Bank"][0][0][1][1].ToString());
        Debug.Log("Transation 2 amt " + jsonNode["Bank"][0][0][1][0].ToString());
        Debug.Log("Transation 3 name " + jsonNode["Bank"][0][0][2][1].ToString());
        Debug.Log("Transation 3 amt " + jsonNode["Bank"][0][0][2][0].ToString());
        Debug.Log("Transation 4 name " + jsonNode["Bank"][0][0][3][1].ToString());
        Debug.Log("Transation 4 amt " + jsonNode["Bank"][0][0][3][0].ToString());



        // get individual values from Analytics
        Debug.Log("atm " + jsonNode["Bank"][1][0].ToString());
        Debug.Log("food" + jsonNode["Bank"][1][1].ToString());
        Debug.Log("other" + jsonNode["Bank"][1][2].ToString());
        Debug.Log("travel " + jsonNode["Bank"][1][3].ToString());
        Debug.Log("utilities " + jsonNode["Bank"][1][4].ToString());


        // get individual values from Offers
        Debug.Log("name " + jsonNode["Bank"][2][0].ToString());
        Debug.Log("video " + jsonNode["Bank"][2][1]);



    }
}