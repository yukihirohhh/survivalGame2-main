using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // アイテムの名前を設定するための変数
    public string ItemName;

    // アイテムの名前を取得するメソッド
    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        // マウスの左クリックが押されたかどうかを確認
        if (Input.GetMouseButtonDown(0))
        {
            // マウスの位置からRayを発射
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycastを発射し、指定した距離内でヒットしたオブジェクトを取得
            if (Physics.Raycast(ray, out hit))
            {
                // ヒットしたオブジェクトが自分自身かどうかを確認
                if (hit.transform == transform)
                {
                    // オブジェクトのタグが "item" かどうかを確認
                    if (gameObject.CompareTag("item"))
                    {

                        if(!InventorySystem.Instance.CheckIfFull())
                        {

                            // インベントリにアイテムを追加
                            InventorySystem.Instance.AddToInventory(ItemName);

                            // オブジェクトを破棄
                            Destroy(gameObject);

                        }
                        else
                        {

                            Debug.Log("インベントリ内にスペースがありません。");

                        }

                    }
                }
            }
        }
    }
}