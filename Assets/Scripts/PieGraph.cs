using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PieGraph : MonoBehaviour {

    public Image [] wedge ;

    public float[] values ;
	// Use this for initialization
	void Start () {

	}

    void Update()
    {
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
            wedge[i].rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= wedge[i].fillAmount * 360f;
        }

    }

}
