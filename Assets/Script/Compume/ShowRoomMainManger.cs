using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoomMainManger : MonoBehaviour {

    public Transform container;
    public Transform mist;
    public GameObject ItemInAisleDetailedPrefab;
    private Transform player;
    private Camera camera;
    private GameObject details;
    // Use this for initialization
    void Start () {
        GvrViewer.Instance.VRModeEnabled = false;
        EventManager.StartListening(EventManager.SHOW_ITEMS_DETAILS, showItemDetails);
        EventManager.StartListening(EventManager.EXIT_DETAILS, ExitItemDetails);
        camera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //fill stands
        int count = PrefabManger.instance.standsPosList.Count;
        GameObject[] prefabsList = PrefabManger.instance.getRandomSetOfPrefabs(count);
        for (int i = 0; i < count; i++)
        {
            GameObject showItem = Instantiate(prefabsList[i], PrefabManger.instance.standsPosList[i], Quaternion.identity, container);
            Vector3 pos = PrefabManger.instance.standsPosList[i];
            Vector3 scale = PrefabManger.instance.standsScaleList[i];
            pos.y = (scale.y *.5f) + (showItem.transform.localScale.y * .85f);
            showItem.transform.localPosition = pos;
            //showItem.transform.LookAt(player);
            var lookPos =  player.position - showItem.transform.position;
            lookPos.y = 0;
            var rot = Quaternion.LookRotation(lookPos);
            showItem.transform.rotation = rot;
            showItem.AddComponent<ShowItem>();
        }
	}

    private void ExitItemDetails()
    {
        LeanTween.scale(mist.gameObject, Vector3.zero, 5).setOnComplete(delegate() { mist.gameObject.SetActive(false); });
        
    }

    private void showItemDetails(GameObject itemIcon)
    {
        if(details)
            DestroyImmediate(details);
        mist.gameObject.SetActive(true);
        mist.localScale = Vector3.zero;
        LeanTween.scale(mist.gameObject, Vector3.one, 5);
        
        //TODO:animate or disable items
        details = Instantiate(ItemInAisleDetailedPrefab, itemIcon.transform.position, Quaternion.identity, transform);
        details.transform.localScale = Vector3.zero;
        Animate();
    }

    private void Animate()
    {
        Vector3 pos =  camera.transform.position + camera.transform.forward * 1.5f;
        pos.y += 1.5f;
        LeanTween.move(details, pos, .5f);
        LeanTween.scale(details, Vector3.one, .5f);
        var lookPos = details.transform.position - player.position;
        var rot = Quaternion.LookRotation(lookPos);
        details.transform.rotation = rot;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
