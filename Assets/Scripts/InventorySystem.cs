using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

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

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }


    public void AddToInventory(string itemName)
    {
        // Resources�t�H���_����v���n�u��ǂݍ���
        GameObject itemPrefab = Resources.Load<GameObject>(itemName);

        // �v���n�u�����݂��Ȃ��ꍇ�̃G���[�n���h�����O
        if (itemPrefab == null)
        {
            Debug.LogError($"�A�C�e���v���n�u '{itemName}' �� Resources �t�H���_���Ɍ�����܂���B");
            return;
        }

        // ��̃X���b�g��T��
        whatSlotToEquip = FindNextEmptySlot();

        // ��̃X���b�g��������Ȃ��ꍇ�̃G���[�n���h�����O
        if (whatSlotToEquip == null)
        {
            Debug.LogError("�C���x���g���ɋ󂫃X���b�g������܂���B");
            return;
        }

        // �v���n�u���C���X�^���X�����ăX���b�g�ɒǉ�
        itemToAdd = Instantiate(itemPrefab, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        // �A�C�e�����X�g�ɒǉ�
        itemList.Add(itemName);
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
}