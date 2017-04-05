using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CategoriesManger : MonoBehaviour {

    public GameObject prefab;
    public Transform player;
    public ItemsManger itemsManger;//TODO use general manger to handel this
    public Canvas scrollingCanvas;
    public DownMenuController downMenuCanvas;
    private int numberOfObjects = 8;
    private float radius = 8f;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private String[] spriteNames;
    private String[] titles;
    private List<GameObject> _itemsList;//has getter
    private float _distanceFromPlayer;//has getter
    private float _childrenDistanceFromCenter;//has getter
    private float _inteactivityDistance;//has getter
    
    public float inteactivityDistance
    {
        get
        {
            return _inteactivityDistance;
        }
    }
    
    // Use this for initialization
    void Start() {
        //GvrViewer.Instance.VRModeEnabled = false;
        //TODO:spriteNames will be received from server and use serializable calss
        spriteNames = new string[]{ "Athletic-Shoes", "Baby-Food", "Perfumes-Fragrances", "Luggage-Accessories", "Televisions", "Gaming", "Tech-Accessories", "Laptops"};
        titles = new string[] { "Athletic Shoes", "Baby Food", "Perfumes", "Luggage", "Televisions", "Gaming", "Tech-Accessories", "Laptops" };
        numberOfObjects = spriteNames.Length;

        initialPos = transform.position;
        initialRot = transform.rotation;
            
        drawCategoriesAsCircle();
        //TODO:update number of objects according to number of real categories from server
        //TODO:update radius according to number of categories
    }

    public void drawCategoriesAsCircle()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;

        downMenuCanvas.hideHomeButton();
        scrollingCanvas.enabled = true;
        _itemsList = new List<GameObject>();
        GameObject item = null;
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            item = Instantiate(prefab, pos, Quaternion.identity,transform);
            item.transform.Rotate(new Vector3(0, 180, 0));
            /*            print(item);
                        print(item.GetComponentInChildren<SpriteRenderer>());
                        print(item.GetComponentInChildren<SpriteRenderer>().sprite);
                        print(Resources.Load<Sprite>("Texture/" + spriteNames[i]));
            */
            item.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/" + spriteNames[i]);
            item.GetComponentInChildren<TextMesh>().text = titles[i];
            item.GetComponent<CategoryItem>().id = i;
            _itemsList.Add(item);
        }
        transform.position = new Vector3(0, 9, numberOfObjects * 1.5f);
        transform.Rotate(new Vector3(-18, 0, 0));

        _distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        _childrenDistanceFromCenter = Vector3.Distance(transform.position, item.transform.position);
        print("_distanceFromPlayer: " + _distanceFromPlayer+ ", _childrenDistanceFromCenter: " + _childrenDistanceFromCenter);
        _inteactivityDistance = _distanceFromPlayer - _childrenDistanceFromCenter + (_childrenDistanceFromCenter*0.2f);
    }

    internal void StartExitAnimation(GameObject gameObject, int id)
    {
        downMenuCanvas.showHomeButton();
        foreach (GameObject item in _itemsList)
        {
            if (item == gameObject){
                item.GetComponent<CategoryItem>().ChosenAnimation();
                StartCoroutine(ShowItemsOfCategory(id));
            }
            else{
                item.GetComponent<CategoryItem>().ExitAnimation();
            }
        }

    }

    private IEnumerator ShowItemsOfCategory(int id)
    {
        scrollingCanvas.enabled = false;
        yield return new WaitForSeconds(1);
        //start loading items of this category
        //itemsManger.ShowItems(id);
        //TODO: create manger class , send event to it to handle this stuff above
        EventManager.TriggerEvent(EventManager.SHOW_ITEMS_AISLE,1);//TODO add category id instead on this "1"
    }

    internal void clear()
    {
        _itemsList.Clear();
        System.GC.Collect();
    }

    // Update is called once per frame
    void LateUpdate () {
        try {
            foreach (GameObject i in _itemsList) {
                i.transform.rotation = Quaternion.LookRotation(transform.position - player.position);//LookAt(player);
            }
        }catch(MissingReferenceException e)
        {

        }
    }
    //TODO:exit animation function
    //TODO: clear function
}
