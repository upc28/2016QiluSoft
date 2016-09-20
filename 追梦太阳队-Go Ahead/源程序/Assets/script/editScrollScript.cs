using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class editScrollScript : MonoBehaviour
{
    private string[] filepath;
    private Vector3 rectPos;


    public ScrollRect scrollrect;
    public GameObject displayRect;
    public Font font;
    public Sprite spr;
    public float iint;
    public ColorBlock colorblock;
    public int ttimes;
    public GameObject editCameraScript;
    public Button btn_xx;
    public Button btn_doc;
    public Button btn_load;

    public GameObject t_content;
    public GameObject t_btn;
    public GameObject t_scrollview;
    private Stack<GameObject> sstack;
    private string curName;

    // Use this for initialization
    void Start()
    {
        filepath = Directory.GetFiles(Application.persistentDataPath + Path.DirectorySeparatorChar + "user");
        t_content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, filepath.Length * 50);
        for(int i=0;i<filepath.Length;i++)
        {
            GameObject t_obj =  ((GameObject)Instantiate(t_btn, new Vector3(t_scrollview.transform.position.x+1, t_scrollview.transform.position.y + (filepath.Length*25f-25- 50 * i), 1), Quaternion.identity));
            t_obj.transform.SetParent(t_content.transform, false);
            t_obj.GetComponentInChildren<Text>().text = Path.GetFileName(filepath[i]);
        }
        sstack = new Stack<GameObject>();
        rectPos = displayRect.transform.position;
        Debug.Log(rectPos.y);
    }

    void Update()
    {

    }

    void displayScroll()
    {
        filepath = Directory.GetFiles(Application.persistentDataPath + Path.DirectorySeparatorChar + "user");
        t_content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, filepath.Length * 50);
        for (int i = 0; i < filepath.Length; i++)
        {
            GameObject t_obj = ((GameObject)Instantiate(t_btn, new Vector3(t_scrollview.transform.position.x + 1, t_scrollview.transform.position.y + (filepath.Length * 25f - 25 - 50 * i), 1), Quaternion.identity));
            t_obj.transform.SetParent(t_content.transform, false);
            t_obj.GetComponentInChildren<Text>().text = Path.GetFileName(filepath[i]);
            t_obj.GetComponent<Button>().onClick.AddListener(delegate () { curName = t_obj.GetComponentInChildren<Text>().text; });
            sstack.Push(t_obj);
        }
    }

    public void btn_openClick()
    {
        scrollrect.gameObject.SetActive(true);

        btn_load.gameObject.SetActive(true);
        btn_xx.gameObject.SetActive(true);
        btn_doc.gameObject.SetActive(false);
        displayScroll();
    }



    public void btn_xxClick()
    {
        while (sstack.Count != 0)
        {
            Destroy(sstack.Pop());
        }
        scrollrect.gameObject.SetActive(false);
        btn_load.gameObject.SetActive(false);
        btn_xx.gameObject.SetActive(false);
        btn_doc.gameObject.SetActive(true);
    }

    public void btn_loadClick()
    {
        PlayerPrefs.SetString("load", Application.persistentDataPath + Path.DirectorySeparatorChar + "user" + Path.DirectorySeparatorChar + curName);
        SceneManager.LoadScene("edit");
    }

    

}