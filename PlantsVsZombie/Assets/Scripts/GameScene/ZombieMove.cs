using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��ʬ�Ľű�
 * ���ƽ�ʬ�ĸ��ֶ���
 */
public class ZombieMove : MonoBehaviour
{
    //�ƶ��ٶ�
    public float moveSpeed;
    //�Ƿ�ʼ����
    private bool isHitPlant = false;
    //���湥����ֲ�����
    private GameObject hitPlant;
    //��ʬ�Ƿ�������־
    public bool isDie=false;
    //����ʵ������ʬͷ�����صĶ���
    private GameObject zombieHead;
    void Update()
    {
        //Ѱ���Ӷ���
        Transform[] father = GetComponentsInChildren<Transform>();
        foreach (Transform child in father)
        {
            if (child.name == "ZombieHead")
            {
                zombieHead = child.gameObject;//�����Ӷ��� Ҳ���ǽ�ʬ��ͷ��
            }
        }
        if (isHitPlant == false&&isDie==false)//�����ʬû�� ����ֲ�������������²��ܹ���ֲ��
        {
            //��ʬ���ƶ�
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(-484, gameObject.transform.position.y, gameObject.transform.position.z), moveSpeed * Time.deltaTime);
        }
        else
        {
            if (hitPlant != null)
            {
                //����ʱ����÷� ÿһ�����1���˺�
                hitPlant.GetComponent<Helth>().AcceptDamage(1 * Time.deltaTime);
            }
        }

        if (gameObject.GetComponent<Helth>().bloodNumber < gameObject.GetComponent<Helth>().blood/3)
        {
            //��ʬѪ��С��2��ʱ�򲥷ŵ�ͷ�Ķ��� ��û��ͷ���ߵĶ���
            if (zombieHead != null)
            {
                zombieHead.GetComponent<Animator>().SetTrigger("isWillDie");
            }
            gameObject.GetComponent<Animator>().SetTrigger("isWillDie");
        }

        if (zombieHead != null)
        {
            //ͷ������������ɺ�ɾ������
            AnimatorStateInfo anifo = zombieHead.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (anifo.normalizedTime >= 1f && anifo.IsName("zombieHead"))
            {
                Destroy(zombieHead);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "plant")
        {
            isHitPlant = true;
            gameObject.GetComponent<Animator>().SetTrigger("isAttack");//���Ź�������
            hitPlant = collision.gameObject;//�������ڱ�������ֲ�� Ȼ��ִ�м�Ѫ����
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //ֲ�ﱻ���� ����ǰ��
        if (collision.tag == "plant")
        {
            isHitPlant = false;
            gameObject.GetComponent<Animator>().SetTrigger("isPlantDie");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
