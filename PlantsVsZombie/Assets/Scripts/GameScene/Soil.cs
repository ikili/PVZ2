using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����Ľű�
 * �����궯������������ ���ùؼ�֡�ķ�ʽҲ����
 */
public class Soil : MonoBehaviour
{
    void Update()
    {
        //����������֮����������
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
