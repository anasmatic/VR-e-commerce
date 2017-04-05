using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManger : MonoBehaviour {
    public GameObject prefab;
    public GameObject detailsPrefab;
    private GameObject details;
    public Transform player;
    
    private readonly int gridX = 3;
    private readonly int gridY = 2;
    private readonly int spacing = 5;
    private int numberOfObjects = 8;
    private float radius = 8f;
    private List<GameObject> _itemsList;

    private Vector3 initialPos;
    private Quaternion initialRot;
    // Use this for initialization
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;

    }

    private void drawCategoriesAsCircle(int id)
    {
        transform.position = initialPos;
        transform.rotation = initialRot;

        _itemsList = new List<GameObject>();
        numberOfObjects = Data.lengths[id];
        radius = numberOfObjects;
        GameObject item = null;
        for (int i = 0; i < Data.lengths[id]; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            item = Instantiate(prefab, pos, Quaternion.identity, transform);
            //item.transform.Rotate(new Vector3(0, 180, 0));
            /*            print(item);
                        print(item.GetComponentInChildren<SpriteRenderer>());
                        print(item.GetComponentInChildren<SpriteRenderer>().sprite);
                        print(Resources.Load<Sprite>("Texture/" + spriteNames[i]));
            */
            item.GetComponent<ItemThumb>().itemImg.sprite = Resources.Load<Sprite>("Texture/" + Data.directories[id] + Data.spriteNames[id, i]);
            item.GetComponent<ItemThumb>().itemTitle.text = Data.itemsTitles[id, i];
            item.GetComponent<ItemThumb>().itemPrice.text = "$" + Data.itemsPrices[id, i];
            _itemsList.Add(item);
        }
        transform.position = new Vector3(0, 5, numberOfObjects * 1.5f);
        transform.Rotate(new Vector3(-18, 0, 0));
        /*
        _distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        _childrenDistanceFromCenter = Vector3.Distance(transform.position, item.transform.position);

        _inteactivityDistance = _distanceFromPlayer - _childrenDistanceFromCenter + (_childrenDistanceFromCenter * 0.2f);
        */
    }

    internal void ItemSelected(int id)
    {
        DisableAllItems();
        //ShowDetails(id);
    }

    private void ShowDetails(int id)
    {
        details = Instantiate(detailsPrefab, new Vector3(0,2.5f,5), Quaternion.identity, transform);
        details.transform.Rotate(new Vector3(-18, 0, 0));
    }

    private void DisableAllItems()
    {
        foreach (GameObject item in _itemsList)
        {
            item.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void EnableAllItems()
    {
        foreach (GameObject item in _itemsList)
        {
            item.GetComponent<BoxCollider>().enabled = true;
        }
    }
    public void DeleteAllItems()
    {
        DisableAllItems();
        foreach (GameObject item in _itemsList)
        {
            Destroy(item);//.GetComponent<BoxCollider>().enabled = false;
        }
        _itemsList.Clear();
        if(details != null)
        {
            Destroy(details);
        }
    }
    private void drawCategoriesAsGrid(int id)
    {
        print("neek");
        GameObject item;
        int counter = 0;
        
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                
                Vector3 pos = new Vector3(x*4.65f+spacing, y*5.00f+spacing, 0);

                item = Instantiate(prefab, pos, Quaternion.identity,transform);
                print("ya 5awal : " + item.GetComponent<SpriteRenderer>().bounds.size); 
                item.GetComponent<ItemThumb>().itemImg.sprite = Resources.Load<Sprite>("Texture/"+ Data.directories[id] + Data.spriteNames[id, counter]);
                item.GetComponent<ItemThumb>().itemTitle.text = Data.itemsTitles[id, counter];
                item.GetComponent<ItemThumb>().itemPrice.text = "$"+Data.itemsPrices[id, counter];
                counter++;
                if (counter == Data.lengths[id]) break;
                //TODO: save more data about the object
            }
        }
        print("ya mtnak : " + gameObject.GetComponent<SpriteRenderer>().bounds.size);
        transform.position = new Vector3(0, 2, Data.lengths[id] * 1.5f);
        //transform.Rotate(new Vector3(-18, 0, 0));
    }

    internal void ShowItems(int id)
    {
        drawCategoriesAsCircle(id);
        //drawCategoriesAsGrid(id);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
