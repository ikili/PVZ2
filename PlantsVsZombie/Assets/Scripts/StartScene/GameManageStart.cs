using System;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManageStart : MonoBehaviour
{
    //ѡ���ı�
    public Text optionText;
    //�����ı�
    public Text helpText;
    //�뿪�ı�
    public Text exitText;
    //�û����ı�
    public Text usernameText;
    //�������λ�ã���Ļ���꣩
    Vector3 mousePosition;
    //�������λ�ã��������꣩
    Vector3 mouseWorldPos;
    //����ð��ģʽ������spite ����״̬�Ͱ���״̬
    public Sprite[] adventrueSprite = new Sprite[2];
    //��������ģʽ������spite ����״̬�Ͱ���״̬
    public Sprite[] survivalSprite = new Sprite[2];
    //��������ģʽ������spite ����״̬�Ͱ���״̬
    public Sprite[] challengeSprite = new Sprite[2];
    //��������û���ľ��ѡ�������sprite
    public Sprite[] woodSign2Sprite = new Sprite[2];

    //ð��ģʽ�����ҵ���ķ���ֵ
    private GameObject adventrueScreen;
    //����ģʽ�ҵ���ķ���ֵ
    private GameObject survivalScreen;
    //����ģʽ�ҵ���ķ���ֵ
    private GameObject challengeScreen;
    //�ı��û��������ҵ���ķ���ֵ
    private GameObject woodSign2Screen;
    //�ı��û������ķ���ֵ
    private GameObject changeUsername = null;
    //�½��û������ķ���ֵ
    private GameObject createUsername = null;
    //�ı��û�����UI���
    private GameObject changeUsernameUI;
    //�½��û�����UI���
    private GameObject createUsernameUI;
    //�ı���Ϸ�û����������
    public InputField usernameInput;
    //�½���Ϸ�û������ı�Ԥ����
    public Text usernamePrefab;
    //�½���Ϸ�û�����Ĭ��y����
    private int usernamey = 92;
    //���洴����Ϸ�û��� ���10��
    private Text[] usernameTexts = new Text[10];
    //������Ϸ�û����ĸ���
    private int usernameNum = 0;
    //��������Ϸ������ɫ������Panel����
    private GameObject usernameP;
    //�жϱ��β����ǲ��������� ��Ϊ���������½����ֶ�����ó�ͬһ���Ի���
    private bool isRename = false;
    //���浱ǰ����һ���û����������
    private int usernameIndex;

    //�ı��û�����Ԥ����
    public GameObject changeUsernamePrefab;
    //�½��û�����Ԥ����
    public GameObject createUsernamePrefab;
    public GameObject optionDialogPrefab;
    private GameObject optionDialog;
    private GameObject optionDialogUI;
    public Slider volumeSlider;
    public Slider soundEffectSlider;
    private GameObject exitGame;
    private GameObject exitGameUI;

    private void Start()
    {
        //��ȡ�û���
        usernameText.text = PlayerPrefs.GetString("username", "SmallZombieZombie");
        changeUsernameUI = GameObject.Find("ChangeUsernameUI");//�ҵ���Ϸ�еĸı��û�����UI���
        createUsernameUI = GameObject.Find("CreateUsernameUI");//�ҵ���Ϸ���½��û�����UI���
        optionDialogUI = GameObject.Find("OptionDialogUI");
        exitGameUI = GameObject.Find("ExitGameUI");
        usernameP = GameObject.Find("UsernameP");
        changeUsernameUI.SetActive(false);//ʧ��UI��� ��ʼ����ʾ
        createUsernameUI.SetActive(false);
        optionDialogUI.SetActive(false);
        exitGameUI.SetActive(false);

        Transform[] f = changeUsernameUI.GetComponentsInChildren<Transform>();//����������
        usernameNum = PlayerPrefs.GetInt("usernameNum", 0);//��ȡ�������û�����
        int y = usernamey;
        for (int i = 0; i < usernameNum; i++)
        {
            Text t = Instantiate(usernamePrefab);//ʵ����һ���ı�
            foreach (Transform child in f)
            {
                if (child.name == "Username")
                {
                    t.transform.SetParent(child);//���ø�������
                    string s = PlayerPrefs.GetString("username" + i, "SmallZombieZombie");//��ȡ�洢���û���
                    t.GetComponent<Text>().text = s;
                    if (usernameText.text.Equals(s))
                    {
                        usernameP.transform.localPosition = new Vector3(-18, t.transform.localPosition.y, 0);//����Ĭ������
                    }
                    t.transform.localScale = new Vector3(1, 1, 1);//�ָ�ԭ���ı�����С ��Ϊ����UI�лᱻ����
                    t.transform.localPosition = new Vector3(-18, y, 0);//�����ı�����
                    usernameTexts[i] = t;//�����û���������
                }
                else if (child.name == "NameNull")
                {
                    Vector3 pos = child.transform.localPosition;
                    child.transform.localPosition = new Vector3(pos.x, pos.y - 20, 0);//���½��û����İ�ť����
                }
            }
            y -= 20;//ÿ�εõ�һ���û��������������ƶ�
        }
    }

    private void Update()
    {

        mousePosition = Input.mousePosition; //��ȡ��Ļ����
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition); //��Ļ����ת��������
        //�����ĵ�ǰλ�÷������ߣ�2D״̬�µ���2D�����ߣ�
        //�������� ����Ϊ1f //������������� UI�������ײ��ʶ�𲻳���
        //RaycastHit2D c = Physics2D.Raycast(new Vector2(mouseWorldPos.x, mouseWorldPos.y), Vector2.left, 1f);
        //�����������ʶ��UI�������ײ��
        Collider2D[] c = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //�ҵ���Ϸ��ĸ�������
        adventrueScreen = GameObject.Find("SelectorScreenAdventure");
        survivalScreen = GameObject.Find("SelectorScreenSurvival");
        challengeScreen = GameObject.Find("SelectorScreenChallenges");
        woodSign2Screen = GameObject.Find("SelectorScreen_WoodSign2");

        if (c.Length > 0)
        {
            foreach (Collider2D d in c)
            {
                if (changeUsername == null&&optionDialog==null&&exitGame==null)//�ı��û������Ϊ�յ�ʱ��ſ��Բ��������
                {
                    switch (d.gameObject.name)
                    {
                        case "AdventrueCollision"://ð��ģʽ ����sprite����ʾ
                            //��ֹ��� �����������
                            survivalScreen.GetComponent<SpriteRenderer>().sprite = survivalSprite[0];
                            challengeScreen.GetComponent<SpriteRenderer>().sprite = challengeSprite[0];
                            adventrueScreen.GetComponent<SpriteRenderer>().sprite = adventrueSprite[1];
                            if (Input.GetMouseButtonDown(0))
                            {
                                SceneManager.LoadScene(2); //������һ������
                            }
                            break;
                        case "SurvivalCollision"://����ģʽ
                            //��ֹ��� �����������
                            adventrueScreen.GetComponent<SpriteRenderer>().sprite = adventrueSprite[0];
                            challengeScreen.GetComponent<SpriteRenderer>().sprite = challengeSprite[0];
                            survivalScreen.GetComponent<SpriteRenderer>().sprite = survivalSprite[1];
                            break;
                        case "ChallengeCollision"://����ģʽ
                            //��ֹ��� �����������
                            adventrueScreen.GetComponent<SpriteRenderer>().sprite = adventrueSprite[0];
                            survivalScreen.GetComponent<SpriteRenderer>().sprite = survivalSprite[0];
                            challengeScreen.GetComponent<SpriteRenderer>().sprite = challengeSprite[1];
                            break;
                        case "SelectorScreen_WoodSign2C"://�ı��û���ѡ�
                            woodSign2Screen.GetComponent<SpriteRenderer>().sprite = woodSign2Sprite[1];
                            if (Input.GetMouseButtonDown(0))
                            {
                                //ʵ�����ı��û��������
                                changeUsername = Instantiate(changeUsernamePrefab, new Vector3(148, 69, 0), Quaternion.identity);
                                changeUsernameUI.SetActive(true);//����UI���
                                woodSign2Screen.GetComponent<SpriteRenderer>().sprite = woodSign2Sprite[0];
                            }
                            break;
                        case "Option"://ѡ��
                            optionText.color = new UnityEngine.Color(80 / 255f, 231 / 255f, 35 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                //ʵ�����ı��û��������
                                optionDialog = Instantiate(optionDialogPrefab, new Vector3(-14, 0, 0), Quaternion.identity);
                                optionDialogUI.SetActive(true);//����UI���
                                optionText.color = new UnityEngine.Color(50 / 255f, 50 / 255f, 50 / 255f);
                                //��ȡ��һ�α����ֵ
                                volumeSlider.value = PlayerPrefs.GetFloat("volume");
                                soundEffectSlider.value = PlayerPrefs.GetFloat("soundEffect");
                            }
                            break;
                        case "Help"://����
                            helpText.color = new UnityEngine.Color(80 / 255f, 231 / 255f, 35 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                SceneManager.LoadScene(3); //������һ������
                            }
                            break;
                        case "Exit"://�뿪
                            exitText.color = new UnityEngine.Color(80 / 255f, 231 / 255f, 35 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                //ʵ�����ı��û��������
                                exitGame = Instantiate(createUsernamePrefab, new Vector3(-44, -23, 0), Quaternion.identity);
                                exitGameUI.SetActive(true);//����UI���
                                exitText.color = new UnityEngine.Color(50 / 255f, 50 / 255f, 50 / 255f);
                            }
                            break;
                    } 
                }
                else if (changeUsername != null&&createUsername==null)
                {
                    switch (d.name)
                    {
                        case "NameNull"://���û� �½��û�
                            //�����ı���ɫ
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(255 / 255f, 255 / 255f, 255 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                //ʵ�����½��û������
                                createUsername = Instantiate(createUsernamePrefab, new Vector3(-44, -23, 0), Quaternion.identity);
                                createUsernameUI.SetActive(true);//�����½��û���UI���
                                d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(219 / 255f, 210 / 255f, 169 / 255f);
                            }
                            isRename = false;//����������
                            break;
                        case "Confirm"://ȷ��
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                for (int i = 0; i < usernameNum; i++)
                                {
                                    if (usernameTexts[i].transform.localPosition.y == usernameP.gameObject.transform.localPosition.y)
                                    {
                                        usernameText.text = usernameTexts[i].text;//���Ľ�����ʾ���û���
                                        PlayerPrefs.SetString("username", usernameTexts[i].text);//�洢��ǰ���û���
                                    }
                                }
                                Destroy(changeUsername);//���ٸı��û������
                                changeUsernameUI.SetActive(false);
                            }
                            break;
                        case "Delete"://ɾ���û���
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            break;
                        case "Cancel"://ȡ������
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                Destroy(changeUsername);//���ٸ����û������
                                changeUsernameUI.SetActive(false);//ʧ�ܸ����û���UI���
                            }
                            break;
                        case "Rename"://������
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                //ʵ�����½��û������
                                createUsername = Instantiate(createUsernamePrefab, new Vector3(-44, -23, 0), Quaternion.identity);
                                createUsernameUI.SetActive(true);//�����½��û���UI���
                                d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);

                                for (int i = 0; i < usernameNum; i++)
                                {
                                    //��ΪPanel�������ĳһ��text������һ���Ƕ�Ӧ�� ���Կ�����ô����
                                    if (usernameTexts[i].transform.localPosition.y == usernameP.gameObject.transform.localPosition.y)
                                    {
                                        //������ڵ��ĵ���ѡ�е��ı�
                                        usernameInput.text = usernameTexts[i].text;
                                        usernameIndex = i;//����˴�ѡ�е��ı�����
                                    }
                                }
                                isRename = true;//��������
                            }
                            break;
                        case "UsernameT(Clone)"://�ı�
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(255 / 255f, 255 / 255f, 255 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                //����ı��󽫱����ƶ����ı���λ��
                                usernameP.transform.localPosition = d.gameObject.transform.localPosition;
                            }
                            break;
                        default:
                            //�ָ�Ĭ����ɫ
                            Transform[] father = changeUsernameUI.GetComponentsInChildren<Transform>();
                            foreach (Transform child in father)
                            {
                                if (child.name == "NameNull")
                                {
                                    child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(219 / 255f, 210 / 255f, 169 / 255f);
                                }
                                else if (child.name == "Delete" || child.name == "Confirm" || child.name == "Cancel" || child.name == "Rename")
                                {
                                    child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                                }
                            }
                            for (int i = 0; i < usernameNum; i++)
                            {
                                usernameTexts[i].gameObject.GetComponent<Text>().color = new UnityEngine.Color(219 / 255f, 210 / 255f, 169 / 255f);
                            }
                            break;
                    }
                }
                else if (createUsername != null)
                {
                    switch (d.name)
                    {
                        case "Confirm"://ȷ��
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                if (isRename)//������
                                {
                                    //�������ڵ��ı�����
                                    usernameTexts[usernameIndex].text = usernameInput.text;
                                    //���´洢�ı���ֵ
                                    PlayerPrefs.SetString("username" + usernameIndex, usernameInput.text);
                                    Destroy(createUsername);//���ٴ����û����Ի���
                                    createUsernameUI.SetActive(false);
                                }
                                else
                                {
                                    //�洢�û�������
                                    PlayerPrefs.SetString("username" + usernameNum, usernameInput.text);
                                    //�洢������ʾ���û���
                                    PlayerPrefs.SetString("username", usernameInput.text);
                                    //ֱ�Ӹ��ĵ�ǰ��ʾ���û���
                                    usernameText.text = usernameInput.text;
                                    //��ȡ������
                                    Transform[] f = changeUsernameUI.GetComponentsInChildren<Transform>();
                                    Text t = Instantiate(usernamePrefab);//ʵ�����ı�
                                    foreach (Transform child in f)
                                    {
                                        if (child.name == "Username")//�ҵ�Username ��Ϊ�û�������ĸ�����
                                        {
                                            t.transform.SetParent(child);//���ø�������
                                        }
                                        else if (child.name == "NameNull")
                                        {
                                            Vector3 pos = child.transform.localPosition;
                                            //�½��û�����ť����
                                            child.transform.localPosition = new Vector3(pos.x, pos.y - 20, 0);
                                        }
                                    }

                                    t.text = usernameInput.text;//�����ı�Ϊ�������ı�
                                    t.transform.localScale = new Vector3(1, 1, 1);//�ָ�ԭ��������
                                    t.transform.localPosition = new Vector3(-18, usernamey, 0);//����λ��
                                    usernameTexts[usernameNum] = t;//�����û�����������
                                    for (int i = 0; i < usernameNum; i++)
                                    {
                                        //ÿ���½�һ���û����ͽ�ԭ�����û�������
                                        usernameTexts[i].transform.localPosition = new Vector3(usernameTexts[i].transform.localPosition.x, usernameTexts[i].transform.localPosition.y - 20, 0);
                                    }
                                    usernameNum += 1;//�û�������������
                                    PlayerPrefs.SetInt("usernameNum", usernameNum);//�����û���������

                                    Destroy(createUsername);
                                    createUsernameUI.SetActive(false);
                                    Destroy(changeUsername);
                                    changeUsernameUI.SetActive(false);
                                }
                            }
                            break;
                        case "Cancel"://ȡ��
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                Destroy(createUsername);//���ٸ����û������
                                createUsernameUI.SetActive(false);//ʧ�ܸ����û���UI���
                            }
                            break;
                        default:
                            //�ָ�ԭ������ɫ
                            Transform[] father = createUsernameUI.GetComponentsInChildren<Transform>();
                            foreach (Transform child in father)
                            {

                                if (child.name == "Confirm" || child.name == "Cancel")
                                {
                                    child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                                }
                            }
                            break;
                    }
                }
                else if(optionDialog!=null)
                {
                    switch (d.name)
                    {
                        case "Confirm":
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                Destroy(optionDialog);//���ٸ����û������
                                optionDialogUI.SetActive(false);//ʧ�ܸ����û���UI���
                            }
                            break;
                        default:
                            //�ָ�ԭ������ɫ
                            Transform[] father = optionDialogUI.GetComponentsInChildren<Transform>();
                            foreach (Transform child in father)
                            {
                                if (child.name == "Confirm")
                                {
                                    child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                                }
                            }
                            break;
                    }
                }
                else if (exitGame != null)
                {
                    switch (d.name)
                    {
                        case "Confirm":
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButton(0))
                            {
                                ExitGame();
                            }
                            break;
                        case "Cancel":
                            d.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 255 / 255f, 0 / 255f);
                            if (Input.GetMouseButtonDown(0))
                            {
                                Destroy(exitGame);//���ٸ����û������
                                exitGameUI.SetActive(false);//ʧ�ܸ����û���UI���
                            }
                            break;
                        default:
                            //�ָ�ԭ������ɫ
                            Transform[] father = exitGameUI.GetComponentsInChildren<Transform>();
                            foreach (Transform child in father)
                            {
                                if (child.name == "Confirm" || child.name == "Cancel")
                                {
                                    child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                                }
                            }
                            break;
                    }
                }
            }
        }
        else
        {
            if (changeUsername == null&&optionDialog==null)
            {
                //û�м�⵽�����ʱ��ͽ���ʾ��ΪĬ��״̬
                adventrueScreen.GetComponent<SpriteRenderer>().sprite = adventrueSprite[0];
                survivalScreen.GetComponent<SpriteRenderer>().sprite = survivalSprite[0];
                challengeScreen.GetComponent<SpriteRenderer>().sprite = challengeSprite[0];
                woodSign2Screen.GetComponent<SpriteRenderer>().sprite = woodSign2Sprite[0];
                optionText.color = new UnityEngine.Color(50 / 255f, 50 / 255f, 50 / 255f);
                helpText.color = new UnityEngine.Color(50 / 255f, 50 / 255f, 50 / 255f);
                exitText.color = new UnityEngine.Color(50 / 255f, 50 / 255f, 50 / 255f);
            }
            else if (changeUsername != null&&createUsername==null)
            {
                //�ָ�ԭ������ɫ
                Transform[] father = changeUsernameUI.GetComponentsInChildren<Transform>();
                foreach (Transform child in father)
                {
                    if (child.name == "NameNull")
                    {
                        child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(219 / 255f, 210 / 255f, 169 / 255f);
                    }
                    else if (child.name == "Delete" || child.name == "Confirm" || child.name == "Cancel" || child.name == "Rename")
                    {
                        child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                    }
                }
            }
            else if (createUsername != null)
            {
                //�ָ�ԭ������ɫ
                Transform[] f = createUsernameUI.GetComponentsInChildren<Transform>();
                foreach (Transform child in f)
                {

                    if (child.name == "Confirm" || child.name == "Cancel")
                    {
                        child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                    }
                }
            }
            else if (optionDialog != null)
            {
                //�ָ�ԭ������ɫ
                Transform[] father = optionDialogUI.GetComponentsInChildren<Transform>();
                foreach (Transform child in father)
                {

                    if (child.name == "Confirm")
                    {
                        child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                    }
                }
            }
            else if (exitGame != null)
            {
                //�ָ�ԭ������ɫ
                Transform[] father = exitGameUI.GetComponentsInChildren<Transform>();
                foreach (Transform child in father)
                {
                    if (child.name == "Confirm" || child.name == "Cancel")
                    {
                        child.gameObject.GetComponent<Text>().color = new UnityEngine.Color(0 / 255f, 200 / 255f, 0 / 255f);
                    }
                }
            }
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR    //�ڱ༭��ģʽ��
            EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    //��Чֵ
    public void SoundEffectSlideChanged()
    {
        //����������ֵ
        PlayerPrefs.SetFloat("soundEffect", soundEffectSlider.value);
    }

    //����ֵ
    public void VolumeSlideChanged()
    {
        //����������ֵ
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
