using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��Ϸ���ƽű� �����ڿ�������
 */
public class GameManage : MonoBehaviour
{
    //��������ƶ�����ʵ�������صĶ���
    private GameObject withMousePlant;

    //���������������ת��Ϊ��Ļ����
    Vector3 screenPosition;
    //��ȡ�������Ļ����Ļ����
    Vector3 mousePositionOnScreen;
    //�������Ļ����Ļ����ת��Ϊ��������
    Vector3 mousePositionInWorld;

    //�����Ԥ����
    public GameObject sunPrefab;
    //����������UI
    public Text sunText;
    //������������
    public static int sunNum = 100;

    //���汻����Ŀ�Ƭ
    private GameObject clickedCard;
    //�Ƿ��п�Ƭ������ı�־
    private bool isClickedCard = false;
    //ֲ���Ƿ��Ѿ���ֲ�ı�־
    public static bool isHadPlanting = false;

    //��ʬ��Ԥ����
    public GameObject zombiePrefab;
    //��ʬ������y����
    int[] spawnY = { 196, 96, -4, -97, -195 };

    //��ʾ�ı�
    public Text hintText;
    //��Ƭѡ����
    public GameObject chooseBk;
    //������� ������һ������ ˵������Component�µ�camera �����Թ��˲�����
    public GameObject camera;
    //�����ƶ����ٶ�
    public float moveSpeed;
    //�����ƶ���״̬ ��Ϊ����״̬
    private int moveState = 0;
    //���泡���ƶ������ұ����ɵĽ�ʬ����
    private GameObject[] zombie = new GameObject[10];
    //������Ԥ����
    public GameObject soilPrefab;

    //��ֲֲ�����Ч
    public AudioClip plantClip;
    //����������Ч
    public AudioClip sunClickClip;
    //��ʬ��Ҫ��ʱ����Ч
    public AudioClip zombieComing;
    //����������ٶȿ���
    public int createSunSpeed = 3;
    //��������Ԥ����
    public GameObject progressBarPrefab;
    //�������ϵĽ�ʬͷ
    private GameObject flagMeterHead;
    //��������С����
    private GameObject flagMeterFull;
    //��ʬ���ܲ���
    public int zombieWave = 10;
    //��һ����ʬһ��������ʱ��
    private int nextZombieWaveTime;
    //ͳ�Ƶ�ǰ�Ĵ��ڵĽ�ʬ����
    public int totalZombie = 0; 
    //�������ƶ����ٶ�
    private float progressBarSpeed = 0.01f;
    //С�Ƴ���Ԥ����
    public GameObject lawnCleanerPrefab;
    //������ʵ����С�Ƴ������ɵ�����
    private GameObject[] lawnCleaner = new GameObject[5];
    //С�Ƴ���ʼʱ��ǰ��һ�µ��ٶ�
    public float lawnCleanerSpeed;
    //С�Ƴ���ʼ����y����
    private int[] lawnCleanery = { 172, 87, -15, -112, -211 };
    public GameObject shovelSlotPrefab;
    public GameObject shovelPrefab;
    private GameObject shovel;
    private bool isShovelClicked = false;

    private void Start()
    {
        //ʹ��Я��
        StartCoroutine(ShowText(1));
    }

    /*
     * ���ĵ��ʱ�����һ��ȽϺÿ���
     */
    private void Update()
    {
        //��ȡ���б�ǩΪzombie������
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");
        totalZombie = zombies.Length;//Ȼ���ܸ���������������ĳ���

        if (moveState == 1)
        {
            //����ƶ� ͬʱ��Ƭѡ���ҲҪͬʱ�ƶ� ��Ȼ��ͬ��
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(300, 0, -10), moveSpeed * Time.deltaTime);
            chooseBk.transform.position = Vector3.MoveTowards(chooseBk.transform.position, new Vector3(-232 + 300, 256, 0), moveSpeed * Time.deltaTime);
            if (camera.transform.position.x == 300)//�ƶ������ұ���
            {
                moveState = 0;//��ʱ��ֵΪ0 ��Ȼ��������� ��Ϊ�����Ǹ����򱻵����˶��
                for(int i = 0; i < 10; i++)
                {
                    int x = Random.Range(609, 757);//�������x����
                    int y = Random.Range(-128, 174);//�������y����
                    //ʵ������ʬ����
                    zombie[i] = Instantiate(zombiePrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                Destroy(hintText);//ɾ����ʾ�ı�
            }
        }
        else if (moveState == 2)
        {
            //����Ϳ�Ƭѡ������ �ƶ���ȥ
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(0, 0, -10), moveSpeed * Time.deltaTime);
            chooseBk.transform.position = Vector3.MoveTowards(chooseBk.transform.position, new Vector3(-232, 256, 0), moveSpeed * Time.deltaTime);
            if (camera.transform.position.x == 0)//�ƶ���ȥ�� ��ʼ��Ϸ
            {
                moveState = 0;
                InvokeRepeating("UpdateUI", 0, 0.1f);//����UI����ʾ
                InvokeRepeating("SpawnSun", createSunSpeed, createSunSpeed);//������������
                StartCoroutine(SpawnZombieFirst(6));//��һ����ʬ6������

                for (int i = 0; i < 10; i++)
                {
                    Destroy(zombie[i]);//�ƶ���ɺ�ʬ����
                }
                
                for(int i = 0; i < 5; i++)
                {
                    lawnCleaner[i] = Instantiate(lawnCleanerPrefab, new Vector3(-393, lawnCleanery[i], 0), Quaternion.identity);
                }

                Instantiate(shovelSlotPrefab, new Vector3(70, 256, 0), Quaternion.identity);
               shovel = Instantiate(shovelPrefab, new Vector3(68, 254, 0), Quaternion.identity);
            }
        }

        if (Input.GetMouseButtonDown(0))//���
        {
            Collider2D[] col = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col.Length > 0)
            {
                foreach (Collider2D c in col)
                {
                    switch(c.tag)
                    {
                        case "ground":
                            //��ȡ�黯��ֲ����� Ȼ���ڸ�λ����ֲֲ��
                            GameObject blurPlant = GameObject.FindGameObjectWithTag("blur_plant");
                            if (blurPlant != null)
                            {
                                //ʵ����ֲ�����
                                Instantiate(clickedCard.GetComponent<Card>().plantPrefab, blurPlant.transform.position, Quaternion.identity);
                                //��ֲֲ�����ȡ����ײ���� ���ٽ�����ײ���
                                //c.GetComponent<BoxCollider2D>().enabled = false;

                                isHadPlanting = true;//ֲ���Ѿ���ֲ
                                sunNum -= clickedCard.GetComponent<Card>().cost;//�������
                                Destroy(withMousePlant);

                                Instantiate(soilPrefab, new Vector3(blurPlant.transform.position.x, blurPlant.transform.position.y - 32, 0), Quaternion.identity);
                                
                                AudioSource.PlayClipAtPoint(plantClip, Vector3.zero);
                            }
                            break;
                        case "card":
                            isClickedCard = true;//�����Ƭ
                            isHadPlanting = false;//��û����ֲ
                            //�������Ŀ�Ƭ����
                            clickedCard = c.gameObject;
                            //ʵ������������ƶ��Ķ���
                            withMousePlant = Instantiate(clickedCard.GetComponent<Card>().cardPlantPrefab, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
                            //���ĸ�������ƶ��������ʾ
                            withMousePlant.GetComponent<SpriteRenderer>().sprite = clickedCard.GetComponent<Card>().cardPlantSprite;
                            clickedCard.GetComponent<Card>().cardBk = Instantiate(clickedCard.GetComponent<Card>().cardBkPrefab, clickedCard.transform.position, Quaternion.identity);
                            break;
                        case "sun":
                            //�������󲥷���Ч
                            AudioSource.PlayClipAtPoint(sunClickClip, Vector3.zero);
                            c.GetComponent<sunMove>().isClicked = true;
                            break;
                        case "shovel":
                            print("shovel");
                            isShovelClicked = true;
                            break;
                        case "plant":
                            if (isShovelClicked)
                            {
                                Destroy(c.gameObject);
                                isShovelClicked = false;
                                shovel.transform.position = new Vector3(68, 254, 0);
                            }
                            break;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))//�Ҽ�
        {
            if (isClickedCard)
            {
                Destroy(withMousePlant);//�����Ҽ��дݻٸ�������ƶ��Ķ���
                Destroy(clickedCard.GetComponent<Card>().cardBk);
                isClickedCard = false;
            }
            if (isShovelClicked)
            {
                isShovelClicked = false;
                shovel.transform.position = new Vector3(68, 254, 0);
            }
            
        }
        else if (Input.GetMouseButtonDown(2))//�м�
        {

        }

        //����Ҫ��ֲֲ���ʱ��ʵ��������һ��ֲ���������ƶ�
        if (withMousePlant != null||isShovelClicked)
        {
            //��ȡ���������У������У���λ�ã�ת��Ϊ��Ļ���ꣻ
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            //��ȡ����ڳ���������
            mousePositionOnScreen = Input.mousePosition;
            //�ó����е�Z=��������Z
            mousePositionOnScreen.z = screenPosition.z;
            //������е�����ת��Ϊ��������
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
            if(withMousePlant != null)
            {
                //�����������ƶ�
                withMousePlant.transform.position = mousePositionInWorld;
            }
            else if (isShovelClicked)
            {
                shovel.transform.position = mousePositionInWorld;
            }
        }

        if (flagMeterFull != null)//�����������Ϊ��
        {
            float temp = (float)((10 - zombieWave) / 10.0);//�ܲ���Ϊ10 �仯��
            //�ı��������scale ͬʱ����ı�position ��Ϊscale�Ǵ�������ѹ������չ��
            flagMeterFull.transform.localScale = Vector3.MoveTowards(flagMeterFull.transform.localScale, new Vector3(-temp, 1, 1), progressBarSpeed * Time.deltaTime);
            flagMeterFull.transform.localPosition = Vector3.MoveTowards(flagMeterFull.transform.localPosition, new Vector3(72 - temp*70, 0, 0), progressBarSpeed * Time.deltaTime*70);
            //�ı佩ʬͷ��λ��
            flagMeterHead.transform.localPosition = Vector3.MoveTowards(flagMeterHead.transform.localPosition, new Vector3(61 - temp*130, 0, 0), progressBarSpeed * Time.deltaTime*130);
        }

        for(int i = 0; i < 5; i++)
        {
            if (lawnCleaner[i] != null)
            {
                //�ɿ쵽��
                if (lawnCleaner[i].GetComponent<LawnCleaner>().startMove == false)
                {
                    lawnCleaner[i].transform.position = Vector3.Lerp(lawnCleaner[i].transform.position, new Vector3(-336, lawnCleanery[i], 0), lawnCleanerSpeed * Time.deltaTime);
                } 
            }
        }
    }

    /*
     * ��ʼ��ʱ����ʾ���ֵ�
     */
    IEnumerator ShowText(float t)
    {
        yield return new WaitForSeconds(t);//��һ�εȴ�ʱ��
        hintText.text = PlayerPrefs.GetString("username", "SmallZombieZombie") + "�ķ���";
        hintText.color = new Vector4(255, 255, 255, 255);//��ʾ�ı� ��Text��͸������͸��
        yield return new WaitForSeconds(3);//�ڶ�����ʾʱ��
        moveState = 1;
        yield return new WaitForSeconds(3);//��������ʾʱ��
        moveState = 2;
    }

    /*
     * ��һ�����ɽ�ʬ
     */
    IEnumerator SpawnZombieFirst(float t)
    {
        yield return new WaitForSeconds(t);//��һ�εȴ�ʱ��
        AudioSource.PlayClipAtPoint(zombieComing, Vector3.zero);//���Ž�ʬ��������Ч
        yield return new WaitForSeconds(2);//�ڶ��εȴ�ʱ��
        StartCoroutine(SpawnZombie(0.1f, true));//���ɽ�ʬ
        zombieWave -= 1;//����-1
        //ʵ��������������
        GameObject progressBar = Instantiate(progressBarPrefab, new Vector3(351, -286, 0), Quaternion.identity);
        //���ҽ������µ�������
        Transform[] father = progressBar.GetComponentsInChildren<Transform>();
        foreach (Transform child in father)
        {
            if(child.name == "FlagMeterParts1")//��ʬͷ
            {
                flagMeterHead = child.gameObject;
            }
            else if(child.name == "FlagMeterFull2")//��������״̬��ʱ��
            { 
                flagMeterFull = child.gameObject;
                flagMeterFull.transform.localScale = new Vector3(0, 1, 1);
                flagMeterFull.transform.localPosition = new Vector3(72, 0, 0);
            }
        }
        nextZombieWaveTime = Random.Range(25, 31);//��һ�����ɵľ���ʱ�� �����ʱ�����������һ����ʬ
        InvokeRepeating("SpawnZombieGoon", 1, 1);//������ʬ������
    }

    int times = 0;
    void SpawnZombieGoon()
    {
        times++;
        float totalBlood = 0;//�ܵ�Ѫ��
        float curtotalBlood = 0;//��ǰ����Ѫ��
        if (times > nextZombieWaveTime)
        {
            StartCoroutine(SpawnZombie(0.1f, false));//������һ����ʬ
            zombieWave -= 1;
            nextZombieWaveTime = Random.Range(25, 31);//��������ʱ��
            times = 0;//��ʱ����
            return;
        }

        for(int i = 0; i < totalZombie; i++)
        {
            if (zombie[i] != null)
            {
                curtotalBlood += zombie[i].GetComponent<Helth>().bloodNumber;//ͳ�Ƶ�ǰѪ��
                totalBlood += zombie[i].GetComponent<Helth>().blood;//ͳ���ܵ�Ѫ��
            }
        }
        
        if(totalBlood< totalBlood * 0.5)//��ǰѪ��С����Ѫ����ʱ��һ���ʱ��������һ��
        {
            StartCoroutine(SpawnZombie(0.1f, false));//������һ��
            zombieWave -= 1;
            nextZombieWaveTime = Random.Range(25, 31);//��һ��ʱ����������
            return;
        }
    }

    /*
     * ��������
     */
    void SpawnSun()
    {
        int x = Random.Range(-300, 250);
        GameObject sun = Instantiate(sunPrefab, new Vector3(x, 242, 0), Quaternion.identity);
        sun.GetComponent<sunMove>().isSunflowerCreate = false;
    }

    /*
     * ���ɽ�ʬ
     */
    IEnumerator SpawnZombie(float t,bool first)
    {
        yield return new WaitForSeconds(t);//��һ�εȴ�ʱ��
        int count = first ? 1: Random.Range(1, 5);
        for (int i = 0; i < count; i++)
        {
            int y = Random.Range(0, 5);
            zombie[totalZombie] = Instantiate(zombiePrefab, new Vector3(530, spawnY[y], 0), Quaternion.identity);
            yield return new WaitForSeconds(2);//ÿһ�����ɶ����2��
        }
    }

    /*
     * ����UI����ʾ
     */
    void UpdateUI()
    {
        sunText.text = "" + sunNum;
    }
}
