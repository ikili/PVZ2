using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainCamera : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 mouseWorldPos;
    void Update()
    {
        mousePosition = Input.mousePosition; //��ȡ��Ļ����
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition); //��Ļ����ת��������

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit;

    }
}
