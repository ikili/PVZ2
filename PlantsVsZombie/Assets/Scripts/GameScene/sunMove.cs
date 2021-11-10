using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ����Ľű�
 * ����������ƶ�
 */
public class sunMove : MonoBehaviour
{
    //̫��������������ƶ�����߸߶�
    int height = 60;
    //����ĳ�ʼ�߶�
    float origin_y;
    //�����ƶ��ķ������
    bool dir = false;

    //�ж������Ƿ񱻵��
    public bool isClicked = false;
    //�����ƶ����ٶ�
    public float moveSpeed = 1f;
    //�����С���ٶ�
    public float shrinkSpeed = 2f;

    //�Ƿ���̫�������ɵ�����
    public bool isSunflowerCreate = true;
    //�����½����ٶ�
    public float fallSpeed = 100f;
    

    void Start()
    {
        if (isSunflowerCreate)
        {
            //�����ʼ��y����
            origin_y = gameObject.transform.position.y - 30;
            //�ظ�ִ���ƶ�����
            InvokeRepeating("move", 0, 0.02f);
        }
    }

    private void Update()
    {
        if (isClicked)
        {
            CancelInvoke();
            //�ɿ쵽���ƶ���ָ��λ��
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,new Vector3(-460, 280, 0), moveSpeed * Time.deltaTime);
            //�ɴ�С�仯
            //gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), shrinkSpeed * Time.deltaTime);
        }

        if (isSunflowerCreate == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, -200, gameObject.transform.position.z), fallSpeed * Time.deltaTime);
        }
    }

    void move()
    {
        //��ȡ����ĵ�ǰ����
        Vector3 position = gameObject.transform.position;
        if (position.y - origin_y > height)
        {
            dir = true;//�ƶ�λ�ô���ָ���߶Ⱥ���ȡ��
        }
        if (position.y == origin_y&&dir)
        {
            CancelInvoke();//�ص�ԭ����λ�ú�ȡ���ظ�ִ�еĺ���
        }
        if (dir)
        {
            //�����ƶ�
            gameObject.transform.position = new Vector3(position.x, position.y - 3, position.z);
        }
        else
        {
            //�����ƶ�
            gameObject.transform.position = new Vector3(position.x-1.5f, position.y + 3, position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "SunCollection")
        {
            GameManage.sunNum += 25;
            Destroy(gameObject);
        }
    }
}
