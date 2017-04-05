using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//TODO: make mist and lines not clickable

public class MainItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform container;
    public GameObject mistPrefab;
    bool isGazedAt = false;
    float animationDuration = 0.5f;
    private Vector3[] subitemsPositions;
    private Vector3 initPos;
    ExpandableCollapsabe expandableCollapsabe;
    GameObject mist;
    Transform player;
    private GameObject title;

    // Use this for initialization
    void Start () {
        EventManager.StartListening(EventManager.GAZE_COMPLETED, onItemSelected);
        expandableCollapsabe = gameObject.AddComponent<ExpandableCollapsabe>();
        expandableCollapsabe.isCollapsed = true;
        subitemsPositions = new Vector3[] {
            new Vector3(1.4f,1,1.2f),//2
            new Vector3(1.3f,.8f,-1.3f),//8
            new Vector3(1.4f,-1,1),//4
            new Vector3(1.4f,-1.1f,-.9f),//6
            new Vector3(1.6f,1.5f,0),//1
            new Vector3(1.6f,-1.5f,.2f),//5
            new Vector3(1.5f,0,1.5f),//3
            new Vector3(1.5f,-.3f,-1.6f)//7
            
        };
        initPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void onItemSelected()
    {
        if (!isGazedAt) return;

        EventManager.TriggerEvent(EventManager.NEW_MAIN_ITEM_SELECTED, gameObject);

        if (expandableCollapsabe.GetState() == expandableCollapsabe.STATE_COLLAPSED)
        {
            //dim, start drawing sub items in 3d space, draw lines from origin to flying items
            Vector3 newPosition = (initPos + player.position) * .5f;
            Vector3 midPoint = (initPos + newPosition) * .5f;
            midPoint.y += 2;
            if (!title)
                title = gameObject.GetComponentInChildren<TextMesh>().gameObject;
            LeanTween.alpha(title, 0, animationDuration);
            LeanTween.move(gameObject, new LTBezierPath(new Vector3[] { initPos, midPoint, midPoint, newPosition }), animationDuration).setOnComplete(ShowMistPrepareSubItems);
        }
        else if (expandableCollapsabe.GetState() == expandableCollapsabe.STATE_EXPANDED)
        {
            CollapseAnimation();
        } 
    }

    public void CollapseAnimation()
    {
        //collapse sub items
        HideMistAndSubItems();
        //return to original pos
        LeanTween.move(gameObject, initPos, animationDuration).setDelay(.3f);
        LeanTween.alpha(title, 1, animationDuration*2 );
        expandableCollapsabe.toggleState();
    }

    private void ShowMistPrepareSubItems()
    {
        //create Mist
        mist = Instantiate(mistPrefab, container);
        Vector3 finalMistScal = mist.transform.localScale;

        mist.transform.localScale = Vector3.zero;
        mist.transform.localPosition = Vector3.zero;
        mist.transform.Rotate(transform.rotation.eulerAngles);

        LeanTween.scale(mist, finalMistScal, animationDuration*10);

        StartCoroutine(IntSubItems());
    }

    private IEnumerator IntSubItems()
    {
        System.Random rand = new System.Random();
        GameObject[] subItems = PrefabManger.instance.getRandomSetOfPrefabs(8);
        GameObject prefab;
        Gizmos.color = Color.blue;
        for (int i = 0; i < subItems.Length; i++)
        {
            prefab = subItems[i];
            GameObject subItem = Instantiate(prefab, Vector3.zero, Quaternion.identity, container);
            subItem.transform.Rotate(transform.rotation.eulerAngles);
            SubItem subItemAnime = subItem.AddComponent<SubItem>();
            subItemAnime.IntroAnimation(i);
            yield return new WaitForSeconds((float)rand.NextDouble()*.5f);
        }

        //container.LookAt(player);
    }
    
    private void HideMistAndSubItems()
    {
        LeanTween.scale(mist, Vector3.zero, animationDuration * 2);
        foreach(Transform subItem in container)
        {
            if(subItem.gameObject != mist)
            {
                SubItem subItemAnime = subItem.gameObject.AddComponent<SubItem>();
                subItemAnime.ExitAnimation();
            }
        }
    }

    /*
   private void ommak(GameObject item)
   {
       LineRenderer lineR = subItem.GetComponent<LineRenderer>();
       print("from:" + subItem.transform.localPosition + ", to:" + container.localPosition);
       print("    :" + subItem.transform.position + ", to:" + container.position);
       print("    :" + subitemsPositions[i] + ", to: 0");
       lineR.SetPosition(0, Vector3.zero);
       lineR.SetPosition(1, subItem.transform.InverseTransformPoint(container.position));
   }
*/
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
            isGazedAt = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == gameObject)
            isGazedAt = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
