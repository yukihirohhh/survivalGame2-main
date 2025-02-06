using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // �A�C�e���̖��O��ݒ肷�邽�߂̕ϐ�
    public string ItemName;

    // �A�C�e���̖��O���擾���郁�\�b�h
    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        // �}�E�X�̍��N���b�N�������ꂽ���ǂ������m�F
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̈ʒu����Ray�𔭎�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast�𔭎˂��A�w�肵���������Ńq�b�g�����I�u�W�F�N�g���擾
            if (Physics.Raycast(ray, out hit))
            {
                // �q�b�g�����I�u�W�F�N�g���������g���ǂ������m�F
                if (hit.transform == transform)
                {
                    // �I�u�W�F�N�g�̃^�O�� "item" ���ǂ������m�F
                    if (gameObject.CompareTag("item"))
                    {

                        if(!InventorySystem.Instance.CheckIfFull())
                        {

                            // �C���x���g���ɃA�C�e����ǉ�
                            InventorySystem.Instance.AddToInventory(ItemName);

                            // �I�u�W�F�N�g��j��
                            Destroy(gameObject);

                        }
                        else
                        {

                            Debug.Log("�C���x���g�����ɃX�y�[�X������܂���B");

                        }

                    }
                }
            }
        }
    }
}