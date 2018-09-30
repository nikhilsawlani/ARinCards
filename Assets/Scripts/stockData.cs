using UnityEngine;
using System.Collections;
using SimpleJSON;

public class stockData : MonoBehaviour
{
    string jsonData;

    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    public GameObject bar4;
    public GameObject bar5;
 


    void Start()
    {
        StartCoroutine(Startdata());
    }

    // Use this for initialization
    IEnumerator Startdata()
    {
        // implememt WWW to get json data from any url
        string url = "https://www.alphavantage.co/query?function=SECTOR&apikey=dIC4Y1YBJGMBWW691";
        WWW www = new WWW(url);
        yield return www;

        // store text in www to json string
        if (string.IsNullOrEmpty(www.error))
        {
            jsonData = www.text;
        }

        // use simpleJSON to get values stored in JSON data for different key value pair
        JSONNode jsonNode = SimpleJSON.JSON.Parse(jsonData);
        string bar11 = jsonNode["Rank A: Real-Time Performance"]["Energy"];
        string bar12 = jsonNode["Rank A: Real-Time Performance"]["Utilities"];
        string bar13 = jsonNode["Rank A: Real-Time Performance"]["Financials"];
        string bar14 = jsonNode["Rank A: Real-Time Performance"]["Real Estate"];
        string bar15 = jsonNode["Rank A: Real-Time Performance"]["Industrials"];

        bar11 = bar11.Remove(bar11.Length - 1) ;
        bar12 = bar12.Remove(bar12.Length - 1);

        bar1.transform.localScale = new Vector3(1, float.Parse(bar11),1) ;
        bar2.transform.localScale = new Vector3(1, float.Parse(bar12), 1);
        bar3.transform.localScale = new Vector3(1, float.Parse(bar13.Remove(bar13.Length - 1)), 1);
        bar4.transform.localScale = new Vector3(1, float.Parse(bar14.Remove(bar14.Length - 1)), 1);
        bar5.transform.localScale = new Vector3(1, float.Parse(bar15.Remove(bar15.Length - 1)), 1);

        // get individual values from Analytics
        Debug.Log("Telecommunication Services " + jsonNode["Rank A: Real-Time Performance"]["Utilities"].ToString());
        Debug.Log("Energy " + jsonNode["Rank A: Real-Time Performance"]["Health Care"].ToString());
        Debug.Log("Utilities " + jsonNode["Rank A: Real-Time Performance"]["Financials"].ToString());
        Debug.Log("Industrials " + jsonNode["Rank A: Real-Time Performance"]["Real Estate"].ToString());
        Debug.Log("Consumer Staples " + jsonNode["Rank A: Real-Time Performance"]["Industrials"].ToString());
        Debug.Log("Health Care " + jsonNode["Rank A: Real-Time Performance"][5].ToString());
        Debug.Log("Real Estate " + jsonNode["Rank A: Real-Time Performance"][6].ToString());
        Debug.Log("Materials " + jsonNode["Rank A: Real-Time Performance"][7].ToString());
        Debug.Log("Consumer Discretionary " + jsonNode["Rank A: Real-Time Performance"][8].ToString());
        Debug.Log("Information Technology " + jsonNode["Rank A: Real-Time Performance"][9].ToString());
        Debug.Log("Financials " + jsonNode["Rank A: Real-Time Performance"][10].ToString());



    }
}