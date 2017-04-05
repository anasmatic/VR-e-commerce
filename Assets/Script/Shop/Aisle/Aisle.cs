using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Aisle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform player;
    public GameObject myself;
    public Transform itemsContainer;
    public Transform controlsContainer;
    public GameObject ArrowFrwd;
    public GameObject ArrowBack;
    public GameObject ArrowFrwd2;
    public GameObject ArrowBack2;

    public GameObject ItemInAislePrefab;
    public GameObject ItemInAisleDetailedPrefab; 

    private GameObject gazedObject;
    private GameObject tempCopy;
    private GameObject details;
    private bool isGazedAtSomthing = false;

    private Vector3 initialPos;
    private Vector3 initialitemsContPos;
    private Vector3 initialcontrolsContPos;
    private const int GAP = 8;
    private bool isHidingWithAniamtion;
    private Vector3 exitVector = new Vector3(0, -20, 0);
    private Vector3 aisleLasteState;
    private Renderer mRenderer;
    private List<GameObject> _itemsList;
    private bool shouldHideAll = false;
    private bool isDetailsAnimation = false;
    private bool isShowingWithAniamtion = false;

    // Use this for initialization
    void Start () {
        mRenderer = GetComponent<Renderer>();
        initialPos = transform.position;
        initialitemsContPos = itemsContainer.position;
        initialcontrolsContPos = controlsContainer.position;
        //GameObject icon = AssetDatabase.LoadAssetAtPath(Data.AisleDate[i]["prefab"], typeof(GameObject)) as GameObject;
        //Instantiate(TwoSlice)
        //formAisle();
        //listen to gaze end, and check if isGazedAtSomthing then react

    }

    internal void formAisle(int catID)
    {
        aisleLasteState = initialitemsContPos;
        enabled = true;
        gameObject.SetActive(true);
        itemsContainer.gameObject.SetActive(true);
        controlsContainer.gameObject.SetActive(true);
        isHidingWithAniamtion = false;
        shouldHideAll = false;
        transform.position = initialPos;
        itemsContainer.position = initialitemsContPos;
        controlsContainer.position = initialcontrolsContPos;
        _itemsList = new List<GameObject>();
        //add start dummy transform
        GameObject item;
        for (int i = 2; i < 22/*Data.AisleDate.Length*/; i++)
        {
            //TODO create an item with data from Data class
            //GameObject item = AssetDatabase.LoadAssetAtPath("Assets/Prefab/ItemInAisle.prefab", typeof(GameObject)) as GameObject;
            Vector3 pos = new Vector3(10, 0, i * GAP);
            item = Instantiate(ItemInAislePrefab, pos, Quaternion.identity, itemsContainer);
            pos = new Vector3(-10, 0, i * GAP);
            _itemsList.Add(item);
            i++;
            //item = AssetDatabase.LoadAssetAtPath("Assets/Prefab/ItemInAisle.prefab", typeof(GameObject)) as GameObject;
            item = Instantiate(ItemInAislePrefab, pos, Quaternion.identity, itemsContainer);
            _itemsList.Add(item);
        }
        //add end dummy transform
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        gazedObject = eventData.pointerEnter;
        isGazedAtSomthing = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gazedObject = null;
        isGazedAtSomthing = false;
    }

    internal void ShowDetails(GameObject item)
    {
        tempCopy = Instantiate(item,item.transform.position,item.transform.rotation,transform);
        tempCopy.GetComponent<ItemInAisle>().enabled = true;
        //isDetailsAnimation = true;
        LeanTween.move(tempCopy,
                        new LTBezierPath(new Vector3[] { tempCopy.transform.position, new Vector3(tempCopy.transform.position.x/2,0, 5), new Vector3(tempCopy.transform.position.x / 2, 0, 5), new Vector3(0, 0f, 10) }),
                        .5f).setDelay(1).setOnComplete(AnimateItemComponents);
    }

    private void AnimateItemComponents()
    {
        ItemInAisle thecopy = tempCopy.GetComponent<ItemInAisle>();
        Destroy(thecopy._3dModle); // destroying parent as well
        LeanTween.moveLocalZ(thecopy._bg, 40, 0.3f).setDelay(.2f);
        LeanTween.alpha(thecopy._bg, 0, .5f).setDestroyOnComplete(true);
        LeanTween.moveLocal(thecopy._price, new Vector3(6, 1.7f, 0), .3f);
        LeanTween.alpha(thecopy._price, 0, .6f).setDestroyOnComplete(true);
        LeanTween.moveLocal(thecopy._desc, new Vector3(-6, 1.7f, 0), .3f);
        LeanTween.alpha(thecopy._desc, 0, .6f).setDestroyOnComplete(true);
        LeanTween.moveLocal(thecopy._button, new Vector3(-2.3f, -2.6f, 0), 0.3f).setDestroyOnComplete(true);

        details = Instantiate(ItemInAisleDetailedPrefab, new Vector3(0, 0, 10), Quaternion.identity, transform);
    }

    internal void showWithAnimation()
    {
        foreach ( GameObject item in _itemsList)
        {
            item.GetComponent<ItemInAisle>().enabled = true;
            item.SetActive(true);
        }
        enabled = true;
        gameObject.SetActive(true);
        itemsContainer.gameObject.SetActive(true);
        controlsContainer.gameObject.SetActive(true);
        isHidingWithAniamtion = false;
        isShowingWithAniamtion = true;
    }
    public void HideWithAnimation(bool hideAll)
    {
        aisleLasteState = itemsContainer.position;
        shouldHideAll = hideAll;
        isShowingWithAniamtion = false;
        //isHidingWithAniamtion = true;
        LeanTween.moveLocalY(controlsContainer.gameObject, -20, .5f).setOnComplete(delegate ()
        {
            controlsContainer.gameObject.SetActive(false);
            LeanTween.moveLocalY(itemsContainer.gameObject, -20, .5f).setOnComplete(delegate ()
            {
                enabled = false;
                itemsContainer.gameObject.SetActive(false);
            });
        });
        
    }
    private void DisableAllItems()
    {
        foreach (GameObject item in _itemsList)
        {
            //item.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void EnableAllItems()
    {
        foreach (GameObject item in _itemsList)
        {
            //item.GetComponent<BoxCollider>().enabled = true;
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
        if (details != null)
        {
            Destroy(details);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowingWithAniamtion)
        {

            itemsContainer.position = Vector3.MoveTowards(itemsContainer.position, aisleLasteState, 100 * Time.deltaTime);
            controlsContainer.position = Vector3.MoveTowards(controlsContainer.position, initialcontrolsContPos, 100 * Time.deltaTime);
            if (Vector3.Distance(itemsContainer.position, aisleLasteState) <= 0.1)
            {
                player.GetComponentInChildren<GvrReticlePointer>().OnPointerExit(gameObject);
                
                isShowingWithAniamtion= false;
                enabled = true;
                gameObject.SetActive(true);
                itemsContainer.gameObject.SetActive(true);
                controlsContainer.gameObject.SetActive(true);
            }
        }
        else
        if (isHidingWithAniamtion)
        {
            itemsContainer.position = Vector3.MoveTowards(itemsContainer.position, exitVector, 100 * Time.deltaTime);
            controlsContainer.position = Vector3.MoveTowards(controlsContainer.position, exitVector, 100 * Time.deltaTime);
            //mRenderer.material.color = new Color(1, 1, 1, (mRenderer.material.color.a - 0.01f));
            if (Vector3.Distance(itemsContainer.position, exitVector) < 5)
            {
                isHidingWithAniamtion = false;
                shouldHideAll = false;
                enabled = false;
                player.GetComponentInChildren<GvrReticlePointer>().OnPointerExit(gameObject);
                if (shouldHideAll)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    itemsContainer.gameObject.SetActive(false);
                    controlsContainer.gameObject.SetActive(false);
                }

            }
        }
        else
        if (isGazedAtSomthing)
        {
            if (gazedObject == ArrowFrwd || gazedObject == ArrowBack2)
            {
                itemsContainer.position += new Vector3(0, 0, -1);
            }
            else if (gazedObject == ArrowBack || gazedObject == ArrowFrwd2)
            {
                itemsContainer.position += new Vector3(0, 0, 1);
            }
        }
    }
    
}
