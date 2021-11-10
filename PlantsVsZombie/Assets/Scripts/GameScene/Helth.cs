using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ֲ��ͽ�ʬ������ֵ�ű�
 */
public class Helth : MonoBehaviour
{   
    //Ѫ��
    public  float blood = 5;
    public float bloodNumber = 5;

    //�յ����� ���Ѫ��С��0������
    public void AcceptDamage(float damage)
    {
        bloodNumber -= damage;
        if (bloodNumber < 0)
        {
            if (gameObject.tag == "zombie")
            {
                gameObject.GetComponent<Animator>().SetTrigger("isDie");
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<ZombieMove>().isDie = true;
                //StartCoroutine(DestoryObject(1.45f));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestoryObject(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
