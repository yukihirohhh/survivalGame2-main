using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public GameObject ItemInfoUI;

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    //public bool isFull;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;

        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform)
        {
            if(child.CompareTag("Slot"))
            {

                slotList.Add(child.gameObject);

            }

        }

    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !isOpen)
        {

            Debug.Log("tab is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }


    public void AddToInventory(string itemName)
    {
        // Resourcesフォルダからプレハブを読み込む
        GameObject itemPrefab = Resources.Load<GameObject>(itemName);

        // プレハブが存在しない場合のエラーハンドリング
        if (itemPrefab == null)
        {
            Debug.LogError($"アイテムプレハブ '{itemName}' が Resources フォルダ内に見つかりません。");
            return;
        }

        // 空のスロットを探す
        whatSlotToEquip = FindNextEmptySlot();

        // 空のスロットが見つからない場合のエラーハンドリング
        if (whatSlotToEquip == null)
        {
            Debug.LogError("インベントリに空きスロットがありません。");
            return;
        }

        // プレハブをインスタンス化してスロットに追加
        itemToAdd = Instantiate(itemPrefab, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        // アイテムリストに追加
        itemList.Add(itemName);



        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }



    private GameObject FindNextEmptySlot()
    {

        foreach(GameObject slot in slotList)
        {

            if (slot.transform.childCount == 0)
            {

                return slot;

            }

        }

        return new GameObject();

    }



    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in slotList)
        {

            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }

        }

            if(counter == 32)
            {
                return true;
            }
            else
            {
                return false;
            }

    }


    public void RemoveItem(string nameToRemove, int amountToRemove)
    {

        int counter = amountToRemove;

        for(var i = slotList.Count - 1; i >= 0; i--)
        {

            if (slotList[i].transform.childCount > 0)
            {

                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {

                    Destroy(slotList[i].transform.GetChild(0).gameObject);

                    counter -= 1;

                }
            }
        }
    }

    public void ReCalculateList()
    {
        itemList.Clear();

        foreach(GameObject slot in slotList)
        {

            if(slot.transform.childCount > 0)
            {

                string name = slot.transform.GetChild(0).name; //Stone(Clone)
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");

                itemList.Add(result);

            }
        }
    }
}