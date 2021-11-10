using System;
using UnityEngine;

public class ChangeUsername : MonoBehaviour
{
    //�������λ��
    Vector3 mousePosition;
    //������������
    Vector3 mouseWorldPos;
    //���ʱx��ƫ����
    public int xoff;
    //���ʱy��ƫ����
    public int yoff;
    //����ĵ�ǰx����
    private float cUcurx;
    //���嵱ǰ��y����
    private float cUcury;
    //���������µ�ʱ�� �ϵ�ʱ��
    private DateTime oldTime;
    //��ʱ�� ʵʱˢ��
    private DateTime newTime;
    //�жϱ��β����Ƿ��ǵ�� �����϶� �Ű�ʶ��Ϊ�϶�
    private bool isClick = true;

    private void OnMouseDrag()//����϶����¼� �϶�ʱÿһ֡������� ֻ�ǶԵ�����϶�������
    {
        if (isClick == false)
        {
            mousePosition = Input.mousePosition; //��ȡ��Ļ����
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition); //��Ļ����ת��������   
            GameObject changeUsername = GameObject.FindWithTag("ChangeUserName");
            GameObject changeUsernameUI = GameObject.Find("ChangeUsernameUI");
            //�����λ�ô�������
            if (changeUsername != null)
            {
                cUcurx = changeUsername.transform.localPosition.x;//�������嵱ǰ����
                cUcury = changeUsername.transform.localPosition.y;
                //�������������һ���ƶ�
                changeUsername.transform.localPosition = new Vector3(mouseWorldPos.x - gameObject.transform.localPosition.x + xoff, mouseWorldPos.y - gameObject.transform.localPosition.y + yoff, 0);
                //����UI������һ���ƶ�
                changeUsernameUI.transform.localPosition = new Vector3(changeUsernameUI.transform.localPosition.x + 40 / 56f * (changeUsername.transform.localPosition.x - cUcurx), changeUsernameUI.transform.localPosition.y + 40 / 56f * (changeUsername.transform.localPosition.y - cUcury), 0);
            }
        }
    }

    private void Update()
    {
        newTime = DateTime.Now;
        TimeSpan t = newTime - oldTime;
        if (t.Milliseconds > 120)//��������ʱ�����120ms����Ϊ���϶�
        {
            isClick = false;
        }
    }

    private void OnMouseDown()
    {
        isClick = true;
        oldTime = DateTime.Now;//��������ʱ��
    }
}
