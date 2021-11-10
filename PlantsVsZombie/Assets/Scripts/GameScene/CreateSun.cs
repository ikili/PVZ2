using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��������Ľű�
 */
public class CreateSun : MonoBehaviour
{
    //�����Ԥ����
    public GameObject sunPrefab;
    //����������ٶ�
    private float createSpeed = 10f;
    private float firstCreateSpeed = 3f;
    private bool isFirstCreate = true;
    private bool isHadSpawn = false;

    private void Update()
    {
        AnimatorStateInfo animatorInfo = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if(isHadSpawn == false)
        {
            if (isFirstCreate)
            {
                if (animatorInfo.normalizedTime >= firstCreateSpeed)
                {
                    Spawn();
                    isHadSpawn = true;
                    isFirstCreate = false;
                }
            }
            else
            {
                if (animatorInfo.normalizedTime >= createSpeed)
                {
                    Spawn();
                    isHadSpawn = true;
                }
            }
        }

        if (animatorInfo.IsName("sunFlowerCreate"))
        {
            isHadSpawn = false;
        }
    }
    //��������ķ���
    void Spawn()
    {
        gameObject.GetComponent<Animator>().SetTrigger("isCreateSun");
    }

    void SpawnSun()
    {
        //ʵ��������
        GameObject sun = Instantiate(sunPrefab, transform.position, Quaternion.identity);
        //��������ĳ�ʼ��λ��
        sun.transform.position = new Vector3(transform.position.x - 4, transform.position.y + 19, transform.position.z);
        //���������͸����
        sun.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 233);
    }
}
