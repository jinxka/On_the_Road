﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class questManager : MonoBehaviour {

    #region UnityCompliant Singleton
    public static questManager Instance
    {
        get;
        private set;
    }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

#endregion

    [SerializeField]
    private ItemDataBaseList questDatabase;
    [SerializeField]
    private GameObject prefabQuest;
    [SerializeField]
    private GameObject SlotContainer;
    [SerializeField]
    public List<Item> ItemsInInventory = new List<Item>();


    // Use this for initialization
    void Start () {
        if (questDatabase == null)
            questDatabase = (ItemDataBaseList)Resources.Load("QuestDatabase");
        if(prefabQuest == null)
            prefabQuest = Resources.Load("Prefabs/Quest") as GameObject;
        SlotContainer = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

        addQuestToLog(3);
        addQuestToLog(1);
        addQuestToLog(2);
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(NewInputManager.Instance.QuestLog))
            GUIManager.Instance.TogglePanel(GetComponent<CanvasGroup>());
    }

    public GameObject addQuestToLog(int id)
    {
       GameObject quest = (GameObject)Instantiate(prefabQuest);
       QuestOnObject questOnObject = quest.GetComponent<QuestOnObject>();
       questOnObject.item = questDatabase.getItemByID(id);
       quest.transform.SetParent(SlotContainer.transform);
       quest.GetComponent<RectTransform>().localPosition = Vector3.zero;
        quest.transform.localScale = new Vector3(1, 1, 1);
       quest.transform.GetChild(1).GetComponent<Image>().sprite = questOnObject.item.itemIcon;
       questOnObject.item.indexItemInList = 0;
       updateItemList();
       return quest;  
    }

    public void updateItemList()
    {
        ItemsInInventory.Clear();
        for (int i = 0; i < SlotContainer.transform.childCount; i++)
        {
            ItemsInInventory.Add(SlotContainer.transform.GetChild(i).GetComponent<QuestOnObject>().item);
        }
    }
}
