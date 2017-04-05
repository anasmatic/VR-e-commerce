using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManger : MonoBehaviour {

    
    public OdaDataItem Desc;
    public OdaDataItem Rate;
    public OdaDataItem Price;
    System.Random rand;
    // Use this for initialization
    void Awake () {
        rand = new System.Random();
    }

    void OnEnabled()
    {
        Entro();
    }
    
    internal void Entro()
    {
        OdaDataItem[] subItems = { Desc, Rate, Price };
        for (int i = 0; i < subItems.Length; i++)
        {
            OdaDataItem subItemAnime = subItems[i];
            subItemAnime.IntroAnimation((float) rand.NextDouble() * .5f);
        }
    }

    internal void Outro()
    {
        StartCoroutine(Deactivate());
        OdaDataItem[] subItems = { Desc, Rate, Price };
        for (int i = 0; i < subItems.Length; i++)
        {
            OdaDataItem subItemAnime = subItems[i];
            subItemAnime.ExitAnimation();
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(.6f);
        gameObject.SetActive(false);
    }
}
