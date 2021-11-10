using UnityEngine;

public class Card : MonoBehaviour
{
    //ֲ�￨Ƭ��ֲ��cd
    public float cd;
    //ֲ�￨Ƭ�Ļ���
    public int cost;
    //��Ƭ��������ӰԤ����
    public GameObject cardBkPrefab;
    //��Ƭ��Ӧ��ֲ��Ԥ����
    public GameObject plantPrefab;
    //��������ƶ���ֲ��Ԥ����
    public GameObject cardPlantPrefab;
    //��������ƶ���ֲ��Ԥ�����sprite
    public Sprite cardPlantSprite;
    //��Ӱʵ������ķ���ֵ
    public GameObject cardBk;
    //ֲ�￨Ƭ���ò���ʱ��ͼƬ
    public Sprite disablePrefab;
    //ֲ�￨Ƭ���ó���ʱ��ͼƬ
    public Sprite enablePrefab;
    //ֲ�￨Ƭ�Ƿ���Ա�����ı�־
    private bool isCanClick = true;
    private void Update()
    {
        //��ǰ��������С�ڻ��ѵ�ʱ�� ��Ƭ����Ҳ����Ե��
        if (GameManage.sunNum < cost)
        {
            isCanClick = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = disablePrefab;
        }
        else
        {
            isCanClick = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = enablePrefab;
        }
        if (GameManage.isHadPlanting)//�Ѿ���ֲ�ı�־
        {
            if (cardBk != null)
            {
                //��ȴʱ�䲻���Ե��
                isCanClick = false;
                //ͨ����С����ķ���ʵ����Ӱ�ı仯
                cardBk.transform.localScale = Vector3.MoveTowards(cardBk.transform.localScale, new Vector3(1, 0, 1), cd * Time.deltaTime);
                cardBk.transform.localPosition = Vector3.MoveTowards(cardBk.transform.localPosition, new Vector3(cardBk.transform.localPosition.x, 263+(1- cardBk.transform.localScale.y)*35, 0), cd * Time.deltaTime* 35);
                //��û��֮����������
                if (cardBk.transform.localScale.y == 0)
                {
                    isCanClick = true;
                    Destroy(cardBk);
                }
            }
        }
        //�����Ƭ�����Ե�� ��ʧ��������ײ�����
        gameObject.GetComponent<BoxCollider2D>().enabled = isCanClick ? true : false;
    }
}
