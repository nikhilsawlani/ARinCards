using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class PieGraph : MonoBehaviour {

    public Image [] wedge ;
    string jsonData;
    public float[] values ;
	// Use this for initialization
	void Start () {
        // makegraph();

    }

    void Update()
    {
        StartCoroutine(Startparsing());
    }

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



        values[0] = jsonNode["Bank"][1][0];
        values[1] = jsonNode["Bank"][1][1];
        values[2] = jsonNode["Bank"][1][2];
        values[3] = jsonNode["Bank"][1][3];
        values[4] = jsonNode["Bank"][1][4];

        makegraph();

    }

        public void makegraph(){
        float total = 0f;
        float zRotation = 0f;
        for (int i = 0; i < values.Length;i++){

            total += values[i];
        }

        for (int i = 0; i < values.Length;i++){

            //newwedge.color = wedgec[i];
            wedge[i].fillAmount = values[i] / total;
            //wedge[i].rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= wedge[i].fillAmount * 360f;
        }

    }

}
