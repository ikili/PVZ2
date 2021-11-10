using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ؼ�֡�����¼� 
 * �ؼ�֡������ ����Ҳ��Ҫ������������
 */
public class Shoot : MonoBehaviour
{
    //�ӵ���Ԥ����
    public GameObject bulletPrefab;
    //��������Ч
    public AudioClip shootClip;
    void SpawnBullet()
    {
        //�ж�ֲ�����ڵ���һ�� �Ƿ���ڽ�ʬ ���ڽ�ʬ�ŷ��𹥻�
        if (gameObject.GetComponent<PlantRow>().isHaveZombie)
        {
            //ʵ�����ӵ�
            Instantiate(bulletPrefab, new Vector3(gameObject.transform.position.x + 23, gameObject.transform.position.y + 24, gameObject.transform.position.z), Quaternion.identity);
            //���Ź�����Ч
            AudioSource.PlayClipAtPoint(shootClip, Vector3.zero);
        }
    }
}
