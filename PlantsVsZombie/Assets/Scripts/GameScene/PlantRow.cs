using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *������ֲ��������ֲ��һ���Ƿ��н�ʬ
 *��Ϊֲ��ͨ�� ���Ե���һ���ű�
 */
public class PlantRow : MonoBehaviour
{
    //�ж�ֲ�����ڵ���һ���Ƿ���ڽ�ʬ
    public bool isHaveZombie = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "row")//�еĿ�����
        {
            //��ȡ�������Ƿ��н�ʬ�ı�־λ
            isHaveZombie = collision.GetComponent<Row>().isHaveZombie;
        }
    }
}
