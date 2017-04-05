using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootDataManger : MonoBehaviour {

    
    public FootOdaDataItem Desc;
    public FootOdaDataItem Rate;
    public FootOdaDataItem Price;
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
        FootOdaDataItem[] subItems = { Desc, Rate, Price };
        for (int i = 0; i < subItems.Length; i++)
        {
            FootOdaDataItem subItemAnime = subItems[i];
            subItemAnime.IntroAnimation((float) rand.NextDouble() * .5f);
        }
    }

    internal void Outro()
    {
        StartCoroutine(Deactivate());
        FootOdaDataItem[] subItems = { Desc, Rate, Price };
        for (int i = 0; i < subItems.Length; i++)
        {
            FootOdaDataItem subItemAnime = subItems[i];
            subItemAnime.ExitAnimation();
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(.6f);
        gameObject.SetActive(false);
    }
}
