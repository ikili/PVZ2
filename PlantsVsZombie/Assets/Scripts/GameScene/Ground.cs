using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ����Ľű�
 */
public class Ground : MonoBehaviour
{
    public GameObject plant;//�洢�����ϵ�ֲ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "plant")
        {
            plant = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "plant")
        {
            plant = null;
        }
    }
}
