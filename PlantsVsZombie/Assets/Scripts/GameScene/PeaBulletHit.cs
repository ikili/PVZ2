using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ӵ����֮��Ŀ��ƽű� ��Ҫ�Ǹ��ӵ�û�ж��� ������Ҫ�ű����ӿ��ƶ���
 */
public class PeaBulletHit : MonoBehaviour
{
    public float bfSpeed;
    void Update()
    {
        gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), bfSpeed * Time.deltaTime);
        if (gameObject.transform.localScale.x == 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
