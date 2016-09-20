using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Linq;

public class startControlScript : MonoBehaviour {

    public Text text;
    private int maxLevel;
    public Button[] get;
    public GameObject com1;
    public GameObject com2;
    public GameObject com3;
    public GameObject com4,com10;
    public GameObject obj_zero;
    public Button btn_pause;
    public GameObject ui_left, ui_right;
    public GameObject ball;
    public TextMesh textMesh;
    public Sprite musicOn_pic, musicOff_pic;
    public GameObject bgObj,wallObj;
    public GameObject bg1_1, bg1_2, bg2_1, bg2_2, bg3_1, bg3_2, bg4_1, bg4_2, bg5_1, bg5_2;
    public AudioSource audiosource,audiosourceBall;
    public AudioClip music_level, music_menu;
    private Vector3 loc_zero;
    private Stack<Object> stack;
    private float cameraSpeed;
    private Vector3 cameraLoc_zero;
    private bool musicOn;
    private float endCameraX;
    public Vector3 bg_zero;
    public int curLevel;
    
    // Use this for initialization
    void Start () {
        wallObj = Instantiate(bg1_1);
        bgObj = Instantiate(bg1_2);
        ball.GetComponent<ballScript>().bgObj = bgObj;
        float distance_screen = ui_right.transform.position.x - ui_left.transform.position.x;
        float distance_zero = ui_left.transform.position.x;
        bg_zero = bgObj.transform.position;
        musicOn = true;
        stack = new Stack<Object>();
        maxLevel = 8;
        curLevel = 1;
        cameraSpeed = 0f;
        audiosourceBall.Stop();
        btn_pause.gameObject.SetActive(false);
        loc_zero = obj_zero.transform.position;
        cameraLoc_zero = Camera.main.transform.position;
        loadLevelXML("Level1");
        get[0].transform.position = new Vector3(distance_screen/8+ distance_zero, get[0].transform.position.y, get[0].transform.position.z);
        get[6].transform.position = new Vector3(distance_screen / 4 + distance_zero, get[6].transform.position.y, get[6].transform.position.z);
        get[5].transform.position = new Vector3(distance_screen*7/ 8 + distance_zero, get[5].transform.position.y, get[5].transform.position.z);
        get[1].transform.position = new Vector3(distance_screen*7/ 8 + distance_zero, get[1].transform.position.y, get[1].transform.position.z);
        btn_pause.transform.position = new Vector3(distance_screen* 7 / 8 + distance_zero, btn_pause.transform.position.y, btn_pause.transform.position.z);
    }

	// Update is called once per frame
	void Update () {
        Camera.main.transform.Translate(Time.deltaTime * cameraSpeed, 0, 0);
        bgObj.transform.Translate(Time.deltaTime * (cameraSpeed - 0.01f), 0, 0);
       if (Camera.main.transform.position.x > endCameraX)
        {
            bgObj.transform.position = bg_zero;
            Camera.main.transform.position = cameraLoc_zero;
        }
	}

    public void loadLevel(string path)
    {
        GameObject t1 = bgObj, t2 = wallObj;
        bgObj =Instantiate(bg5_2);wallObj = Instantiate(bg5_1);
        ball.GetComponent<ballScript>().bgObj = bgObj;
        Destroy(t1);Destroy(t2);
        Camera.main.transform.position = cameraLoc_zero;
        cameraSpeed = 0;
        StartCoroutine(IENpauseCamera());
        while (stack.Count!=0)
        {
            Destroy(stack.Pop());
        }
        StreamReader reader = new StreamReader(path);
        int maxLocX = 0,loc1;
        string str1, str2, str3;
        while ((str1 = reader.ReadLine()) != null)
        {
            str2 = reader.ReadLine();
            str3 = reader.ReadLine();
            loc1 = int.Parse(str1);
            create(loc1, int.Parse(str2), int.Parse(str3));
            maxLocX = loc1 > maxLocX ? loc1 : maxLocX;
        }
        create(maxLocX+5, 0, 10);
        reader.Close();
    }
    public void loadLevelXML(string path)
    {
        if(path[5]=='1'|| path[5]=='2')
        {
            GameObject t1 = bgObj, t2 = wallObj;
            bgObj = Instantiate(bg1_2); wallObj = Instantiate(bg1_1);
            ball.GetComponent<ballScript>().bgObj = bgObj;
            Destroy(t1); Destroy(t2);
        }
        else if(path[5]=='3'|| path[5]=='4')
        {
            GameObject t1 = bgObj, t2 = wallObj;
            bgObj = Instantiate(bg2_2); wallObj = Instantiate(bg2_1);
            ball.GetComponent<ballScript>().bgObj = bgObj;
            Destroy(t1); Destroy(t2);
        }
        else if(path[5]=='5'|| path[5]=='6')
        {
            GameObject t1 = bgObj, t2 = wallObj;
            bgObj = Instantiate(bg3_2); wallObj = Instantiate(bg3_1);
            ball.GetComponent<ballScript>().bgObj = bgObj;
            Destroy(t1); Destroy(t2);
        }
        else
        {
            GameObject t1 = bgObj, t2 = wallObj;
            bgObj = Instantiate(bg4_2); wallObj = Instantiate(bg4_1);
            ball.GetComponent<ballScript>().bgObj = bgObj;
            Destroy(t1); Destroy(t2);
        }
        Camera.main.transform.position = cameraLoc_zero;
        bgObj.transform.position = bg_zero;
        cameraSpeed = 0;
        StartCoroutine(IENpauseCamera());
        while (stack.Count != 0)
        {
            Destroy(stack.Pop());
        }

        string result = Resources.Load(path).ToString();
        string[] sss = result.Split('\n');
        Debug.Log(sss.Length);
        int maxLocX = 0, loc1;
        for(int i=0;i<sss.Length-1;i+=3)
        {
            loc1 = int.Parse(sss[i]);
            create(loc1, int.Parse(sss[i + 1]), int.Parse(sss[i + 2]));
            maxLocX = loc1 > maxLocX ? loc1 : maxLocX;
        }
        create(maxLocX+5, 0, 10);
    }


    void create(int x, int y, int tag)
    {
        switch (tag)
        {
            case 1:
                stack.Push(Instantiate(com1, new Vector3(x * 1.18f + loc_zero.x, y * 1.18f + loc_zero.y, 0), Quaternion.identity));
                break;
            case 2:
                stack.Push(Instantiate(com2, new Vector3(x * 1.18f + loc_zero.x, y * 1.18f + loc_zero.y, 0), Quaternion.identity));
                break;
            case 3:
                stack.Push(Instantiate(com3, new Vector3(x * 1.18f + loc_zero.x, y * 1.18f + loc_zero.y, 0), Quaternion.identity));
                break;
            case 4:
                stack.Push(Instantiate(com4, new Vector3(x * 1.18f + loc_zero.x, y * 1.18f + loc_zero.y, 0), Quaternion.identity));
                break;
            case 10:
                stack.Push(Instantiate(com10, new Vector3(x * 1.18f + loc_zero.x, com10.transform.position.y, 0), Quaternion.identity));
                endCameraX = x * 1.18f + loc_zero.x;
                break;
            default:
                break;
        }
    }

    public void btn_startClick()
    {
        foreach(Button a in get)
        {

            a.gameObject.SetActive(false);
        }
        text.enabled = false;
        Camera.main.transform.position = cameraLoc_zero;
        bgObj.transform.position = bg_zero;
        btn_pause.gameObject.SetActive(true);
        ball.GetComponent<ballScript>().enabled = true;
        ball.GetComponent<ballScript>().init();
        audiosource.Stop();
        
        GetComponent<startControlScript>().enabled = false;
        
    }

    public  void restart()
    {
        foreach (Button a in get)
        {

            a.gameObject.SetActive(true);
        }
        text.enabled = true;
    }

    public void btn_leftClick()
    {
        int t = int.Parse(text.text);
        if(t!=1)
        {
            t--;
            curLevel = t;
            text.text = t.ToString();
            if (t == 2)
            {
                GameObject t1 = bgObj, t2 = wallObj;
                bgObj = Instantiate(bg1_2); wallObj = Instantiate(bg1_1);
                ball.GetComponent<ballScript>().bgObj = bgObj;
                Destroy(t1); Destroy(t2);
            }
            if (t == 4)
            {
                GameObject t1 = bgObj, t2 = wallObj;
                bgObj = Instantiate(bg2_2); wallObj = Instantiate(bg2_1);
                ball.GetComponent<ballScript>().bgObj = bgObj;
                Destroy(t1); Destroy(t2);
            }
            if (t == 6)
            {
                GameObject t1 = bgObj, t2 = wallObj;
                bgObj = Instantiate(bg3_2); wallObj = Instantiate(bg3_1);
                ball.GetComponent<ballScript>().bgObj = bgObj;
                Destroy(t1); Destroy(t2);
            }
            loadLevelXML("Level" + t.ToString());
        }
    }

    public void btn_rightClick()
    {
        int t = int.Parse(text.text);
        if (t != maxLevel)
        {
            t++;
            curLevel = t;
            text.text = t.ToString();
            if(t==3)
            {
                GameObject t1 = bgObj, t2 = wallObj;
                bgObj = Instantiate(bg2_2); wallObj = Instantiate(bg2_1);
                ball.GetComponent<ballScript>().bgObj = bgObj;
                Destroy(t1); Destroy(t2);
            }
            if(t==5)
            {
                GameObject t1 = bgObj, t2 = wallObj;
                bgObj = Instantiate(bg3_2); wallObj = Instantiate(bg3_1);
                ball.GetComponent<ballScript>().bgObj = bgObj;
                Destroy(t1); Destroy(t2);
            }
            if(t==7)
            {
                GameObject t1 = bgObj, t2 = wallObj;
                bgObj = Instantiate(bg4_2); wallObj = Instantiate(bg4_1);
                ball.GetComponent<ballScript>().bgObj = bgObj;
                Destroy(t1); Destroy(t2);
            }
                 
            loadLevelXML("Level" + t.ToString());
        }
    }

    public void btn_musicClick()
    {
        if(musicOn)
        {
            musicOn = false;
            get[5].GetComponent<Image>().sprite = musicOff_pic;
            audiosource.volume = 0;
            audiosourceBall.volume = 0;
        }
        else
        {
            musicOn = true;
            get[5].GetComponent<Image>().sprite = musicOn_pic;
            audiosource.volume = 100;
            audiosourceBall.volume = 100;
        }
    }

    public void btn_editClick()
    {
        PlayerPrefs.SetString("load", "");
        SceneManager.LoadScene("edit");
    }

   

    public void btn_exitClick()
    {
        Application.Quit();
    }

    IEnumerator IENpauseCamera()
    {
        yield return new WaitForSeconds(0.5f);
        cameraSpeed = 1f;
    }

    public Vector3 reStartCamera()
    {
        return cameraLoc_zero;
    }
}
