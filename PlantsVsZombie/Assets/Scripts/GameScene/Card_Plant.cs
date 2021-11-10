using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *���ֲ�￨Ƭ�����ɵ�ֲ�����Ľű� 
 */
public class Card_Plant : MonoBehaviour
{
    //�黯��ֲ��Ԥ����
    public GameObject blurPlantPrefab;
    //ʵ�����黯ֲ��ķ���ֵ
    private GameObject plant;

    //��ײ��� ������ײ���г���
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            if (plant == null&&collision.GetComponent<Ground>().plant==null)
            {
                //ʵ�����黯��ֲ��
                plant = Instantiate(blurPlantPrefab, collision.gameObject.transform.position, Quaternion.identity);
                //�����黯ֲ�����ʾ sprite
                plant.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            //�Ƴ���ײ��֮����������
            Destroy(plant);
        }
    }
}
