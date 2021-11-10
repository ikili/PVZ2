using System;
using UnityEngine;

public class CreateUsername : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 mouseWorldPos;

    public int xoff;
    public int yoff;

    private float cUcurx;
    private float cUcury;

    private DateTime oldTime;
    private DateTime newTime;

    private bool isClick = true;

    private void OnMouseDrag()
    {
        if (isClick == false)
        {
            mousePosition = Input.mousePosition; //��ȡ��Ļ����
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition); //��Ļ����ת��������
            GameObject changeUsername = GameObject.FindWithTag("CreateUserName");
            GameObject changeUsernameUI = GameObject.Find("CreateUsernameUI");
            //�����λ�ô�������
            if (changeUsername != null)
            {
                cUcurx = changeUsername.transform.localPosition.x;
                cUcury = changeUsername.transform.localPosition.y;
                changeUsername.transform.localPosition = new Vector3(mouseWorldPos.x - gameObject.transform.localPosition.x + xoff, mouseWorldPos.y - gameObject.transform.localPosition.y + yoff, 0);
                changeUsernameUI.transform.localPosition = new Vector3(changeUsernameUI.transform.localPosition.x + 40 / 56f * (changeUsername.transform.localPosition.x - cUcurx), changeUsernameUI.transform.localPosition.y + 40 / 56f * (changeUsername.transform.localPosition.y - cUcury), 0);
            }
        }
    }

    private void Update()
    {
        newTime = DateTime.Now;
        TimeSpan t = newTime - oldTime;
        if (t.Milliseconds > 120)
        {
            isClick = false;
        }
    }

    private void OnMouseDown()
    {
        isClick = true;
        oldTime = DateTime.Now;
    }
}
