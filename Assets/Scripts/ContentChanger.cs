using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChanger : MonoBehaviour {

    public GameObject accountinfo;
    public GameObject offers;
    public GameObject Analytics;
    public GameObject fd;
    public GameObject stocks;
	// Use this for initialization
	void Start () {
        accountinfo.SetActive(true);
        offers.SetActive(false);
        Analytics.SetActive(false);
        fd.SetActive(false);
        stocks.SetActive(false);
	}
	
    public void ActiveAccountsInfo(){
        accountinfo.SetActive(true);
        offers.SetActive(false);
        Analytics.SetActive(false);
        fd.SetActive(false);
        stocks.SetActive(false);

    }

    public void ActiveOffers()
    {
        accountinfo.SetActive(false);
        offers.SetActive(true);
        Analytics.SetActive(false);
        fd.SetActive(false);
        stocks.SetActive(false);

    }

    public void ActivePerfomance()
    {
        accountinfo.SetActive(false);
        offers.SetActive(false);
        Analytics.SetActive(true);
        fd.SetActive(false);
        stocks.SetActive(false);

    }

    public void fdinfo()
    {
        accountinfo.SetActive(false);
        offers.SetActive(false);
        Analytics.SetActive(false);
        fd.SetActive(true);
        stocks.SetActive(false);

    }

    public void Stocksinfo()
    {
        accountinfo.SetActive(false);
        offers.SetActive(false);
        Analytics.SetActive(false);
        fd.SetActive(false);
        stocks.SetActive(true);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
