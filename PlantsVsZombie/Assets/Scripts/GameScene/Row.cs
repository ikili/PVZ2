using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ײ���ϵĽű� �������ڱ����Ƿ���ڽ�ʬ
 */
public class Row : MonoBehaviour
{
    /*
     * �ܹ����� ÿһ�����涼һ����ײ�� ����Ƿ��н�ʬ������
     */

    //����һ���Ƿ��н�ʬ�ı�־
    public bool isHaveZombie = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            //��⵽�н�ʬ
            isHaveZombie = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "zombie")
        {
            //��ʬû����
            isHaveZombie = false;
        }
    }
}
