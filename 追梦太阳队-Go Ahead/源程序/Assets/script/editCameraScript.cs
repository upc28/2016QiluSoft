using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class editCameraScript : MonoBehaviour {
    private float first;
    private bool flag;
    private int[,] store;
    public int curTag;

    private Button btn;
    private Texture2D texture2d;
    private float length;
    public InputField input_text;
    public GameObject sprite;
    public Button btn_mode;
    public Sprite da_pic, ad_pic;
    public Transform spirteStart, spirteEnd;
    public Button[] comBtn;
    public Slider slider;
    public GameObject top;
    public GameObject ui_left, ui_right;
    public GameObject[] btn3;
    private List<GameObject> list;
    private float topY;
    private bool addmode;
    private float positionZeroX;
    private Vector3 mousePosiVec3;

    // Use this for initialization
    void Start () {
        float distance = ui_right.transform.position.x - ui_left.transform.position.x;
        length = spirteEnd.transform.position.x - spirteStart.transform.position.x;
        positionZeroX = transform.position.x;
        addmode = true;
        list = new List<GameObject>();
        topY = top.transform.position.y;
        if(!Directory.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar+"user"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + Path.DirectorySeparatorChar + "user");
            
        }
        input_text.text = "newLevel";
        btn_mode.gameObject.transform.position = new Vector3(distance/8+ui_left.transform.position.x, btn_mode.gameObject.transform.position.y, -1);
        comBtn[0].gameObject .transform.position = new Vector3(distance*2 / 5 + ui_left.transform.position.x, comBtn[0].transform.position.y, comBtn[0].transform.position.z);
        comBtn[1].transform.position = new Vector3(distance*9 / 20 + ui_left.transform.position.x, comBtn[1].transform.position.y, comBtn[1].transform.position.z);
        comBtn[2].transform.position = new Vector3(distance / 2 + ui_left.transform.position.x, comBtn[2].transform.position.y, comBtn[2].transform.position.z);
        comBtn[3].transform.position = new Vector3(distance*11 / 20 + ui_left.transform.position.x, comBtn[3].transform.position.y, comBtn[3].transform.position.z);

        btn3[0].transform.position = new Vector3(distance * 7 / 10 + ui_left.transform.position.x, btn3[0].transform.position.y, btn3[0].transform.position.z);
        btn3[1].transform.position = new Vector3(distance * 8 / 10 + ui_left.transform.position.x, btn3[1].transform.position.y, btn3[1].transform.position.z);
        btn3[2].transform.position = new Vector3(distance * 9 / 10 + ui_left.transform.position.x, btn3[2].transform.position.y, btn3[2].transform.position.z);

        flag = false;
        curTag = 0;

        store = new int[300, 8];
        saveInit();
        string ans = PlayerPrefs.GetString("load");
        if (ans.Length>0) loadFromFile(ans);
    }

    void saveInit()
    {
        for (int i = 0; i < 300; i++)
            for (int j = 0; j < 8; j++)
                store[i, j] = -1;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0)&& (mousePosiVec3 = Camera.main.ScreenToWorldPoint(Input.mousePosition)).y< topY)
        {
            first = mousePosiVec3.x;
            flag = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            flag = false;
        }
        if(flag)
        {
            Camera.main.transform.Translate(first- Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0)).x, 0, 0);
            slider.value = (transform.position.x - spirteStart.transform.position.x) / length;
        }
	}
    public void write(int tag,Vector2 location)
    {
        Debug.Log("debug start");
        int i;
        for (i = 0; i < list.Count; i++)
        {
            if (Mathf.Abs(list[i].transform.position.x - location.x)<0.1 && Mathf.Abs(list[i].transform.position.y - location.y)<0.1)
            {
                Debug.Log("delete");
                Destroy(list[i]);
                list.RemoveAt(i);
                break;
            }
        }
        Debug.Log("debug end");
        location.x += 12.04f;location.y += 3.02f;
        store[(int)(location.x / 1.18), (int)(location.y / 1.18)] = tag;
        Debug.Log("list num:" + list.Count);
        
        
    }

    public void save()
    {

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + Path.DirectorySeparatorChar+"user"+Path.DirectorySeparatorChar+input_text.text, false);
        for(int i=0;i<300;i++)
            for(int j=0;j<8;j++)
            {
                if(store[i,j]!=-1)
                {
                    writer.WriteLine(i);
                    writer.WriteLine(j);
                    writer.WriteLine(store[i, j]);
                }
            }
        writer.Flush();
        writer.Close();
    }

    public void loadFromFile(string path)
    {
        saveInit();
        StreamReader reader = new StreamReader(path);
        string str1;
        int p1, p2, p3;
        while ((str1 = reader.ReadLine()) != null)
        {
            p1 = int.Parse(str1);
            p2 = int.Parse(reader.ReadLine());
            p3 = int.Parse(reader.ReadLine());
            store[p1, p2] = p3;
            Debug.Log(p1);
            Debug.Log(p2);
            Debug.Log(p3);             
            switch (p3)
            {
                case 1:
                    texture2d = (Texture2D)Resources.Load("pic_com1");
                    break;
                case 2:
                    texture2d = (Texture2D)Resources.Load("pic_com2");
                    break;
                case 3:
                    texture2d = (Texture2D)Resources.Load("pic_com3");
                    break;
                case 4:
                    texture2d = (Texture2D)Resources.Load("barrier4");
                    break;
                default:
                    break;
            }
            GameObject tsprite = (GameObject)Instantiate(sprite, new Vector3(p1 * 1.18f - 11.45f, p2 * 1.18f - 2.43f, -2), Quaternion.identity);
            SpriteRenderer spr = tsprite.GetComponent<SpriteRenderer>();
            spr.sortingOrder = 3;
            Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
            spr.sprite = sp;
            list.Add(tsprite);
        }
        reader.Close();
        input_text.text = Path.GetFileName(path);
        PlayerPrefs.SetString("load", "");
    }

    public void btn_modeClick()
    {
        if(addmode)
        {
            addmode = false;
            btn_mode.GetComponent<Image>().sprite = da_pic;
            curTag = -1;
            btn.image.color = Color.gray;
        }
        else
        {
            addmode = true;
            btn_mode.GetComponent<Image>().sprite = ad_pic;
        }
    }

    public void quit()
    {
        SceneManager.LoadScene("start");
    }

    public void com1_click()
    {
        if (!addmode) return;
        if (btn != null) btn.image.color = Color.gray;
        curTag = 1;
        btn = comBtn[0];
        btn.image.color = Color.white;
    }
    public void com2_click()
    {
        if (!addmode) return;
        if (btn != null) btn.image.color = Color.gray;
        curTag = 2;
        btn = comBtn[1];
        btn.image.color = Color.white;
    }
    public void com3_click()
    {
        if (!addmode) return;
        if (btn != null) btn.image.color = Color.gray;
        curTag = 3;
        btn = comBtn[2];
        btn.image.color = Color.white;
    }

    public void com4_click()
    {
        if (!addmode) return;
        if (btn != null) btn.image.color = Color.gray;
        curTag = 4;
        btn = comBtn[3];
        btn.image.color = Color.white;
    }

    public void slider_change(float value)
    {
        Debug.Log(value);
        transform.position = new Vector3(length*value+positionZeroX,transform.position.y,transform.position.z);
    }

}
