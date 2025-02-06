using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    // インタラクト可能なオブジェクトの情報を表示するUI
    public GameObject interaction_Info_UI;

    // UIに表示するテキスト
    Text interaction_text;

    // Raycastが届く最大距離
    public float interactionDistance = 5f;

    private void Start()
    {
        // UIのTextコンポーネントを取得
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Update()
    {
        // マウスの位置からRayを発射
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycastを発射し、指定した距離内でヒットしたオブジェクトを取得
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // ヒットしたオブジェクトのTransformを取得
            var selectionTransform = hit.transform;

            // ヒットしたオブジェクトがInteractableObjectコンポーネントを持っているか確認
            if (selectionTransform.GetComponent<InteractableObject>())
            {
                // オブジェクトの名前をUIに表示
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);

            }
            else
            {
                // InteractableObjectコンポーネントがない場合、UIを非表示にする
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            // Raycastが何もヒットしなかった場合、UIを非表示にする
            interaction_Info_UI.SetActive(false);
        }
    }
}