using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // シングルトンインスタンス
    public static PlayerState Instance { get; set; }

    // ----- Player Health (体力) ----- //
    public float currentHealth;   // 現在の体力
    public float maxHealth;       // 最大体力

    // ----- Player Calories (カロリー) ------ //
    public float currentCalories; // 現在のカロリー
    public float maxCalories;     // 最大カロリー

    float distanceTraveled = 0;   // 移動距離の追跡
    Vector3 lastPosition;         // 最後の位置

    public GameObject playerBody; // プレイヤーのオブジェクト

    // ----- Player Hydration (水分) ----- //
    public float currentHydrationPercent; // 現在の水分量(%)
    public float maxHydrationPercent;     // 最大水分量(%)

    public bool isHydrationActive; // 水分補給が有効かどうか

    private void Awake()
    {
        // シングルトンパターンの適用
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // 初期ステータス設定
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        // 水分減少のコルーチン開始
        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
        while (true)
        {
            currentHydrationPercent -= 1; // 10秒ごとに水分を1減少
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        // プレイヤーの移動距離を計算
        distanceTraveled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        // 10m移動するごとにカロリーを1減少
        if (distanceTraveled >= 10)
        {
            distanceTraveled = 0;
            currentCalories -= 1;
        }

        // Nキーを押すと体力を10減少
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
    }
}
