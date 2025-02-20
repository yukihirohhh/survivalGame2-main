using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static PlayerState Instance { get; set; }

    // ----- Player Health (�̗�) ----- //
    public float currentHealth;   // ���݂̗̑�
    public float maxHealth;       // �ő�̗�

    // ----- Player Calories (�J�����[) ------ //
    public float currentCalories; // ���݂̃J�����[
    public float maxCalories;     // �ő�J�����[

    float distanceTraveled = 0;   // �ړ������̒ǐ�
    Vector3 lastPosition;         // �Ō�̈ʒu

    public GameObject playerBody; // �v���C���[�̃I�u�W�F�N�g

    // ----- Player Hydration (����) ----- //
    public float currentHydrationPercent; // ���݂̐�����(%)
    public float maxHydrationPercent;     // �ő吅����(%)

    public bool isHydrationActive; // �����⋋���L�����ǂ���

    private void Awake()
    {
        // �V���O���g���p�^�[���̓K�p
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
        // �����X�e�[�^�X�ݒ�
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        // ���������̃R���[�`���J�n
        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
        while (true)
        {
            currentHydrationPercent -= 1; // 10�b���Ƃɐ�����1����
            yield return new WaitForSeconds(10);
        }
    }

    void Update()
    {
        // �v���C���[�̈ړ��������v�Z
        distanceTraveled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        // 10m�ړ����邲�ƂɃJ�����[��1����
        if (distanceTraveled >= 10)
        {
            distanceTraveled = 0;
            currentCalories -= 1;
        }

        // N�L�[�������Ƒ̗͂�10����
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
    }
}
