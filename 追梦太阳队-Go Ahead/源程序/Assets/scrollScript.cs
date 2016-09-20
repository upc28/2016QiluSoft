using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class scrollScript : MonoBehaviour {
    private string[] filepath;
    private Vector3 rectPos;
    private Stack<GameObject> sstack;

    public GameObject t_scrollview;
    public GameObject t_content;
    public Font font;
    public Sprite spr;
    public float iint;
    public ColorBlock colorblock;
    public Graphic targetGraphic;
    public int ttimes;
    public Button btn_load;
    public Button btn_xx;
    public Button btn_doc;
    public GameObject controlObj;
    public GameObject t_btn;
    public Text ttext;


    private string curName;

	// Use this for initialization
	void Start () {
        sstack = new Stack<GameObject>();
        Debug.Log(rectPos.y);
    }
	
	// Update is called once per frame
	void Update () {
	
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

    public void btn_docClick()
    {

        t_scrollview.gameObject.SetActive(true);
        btn_load.gameObject.SetActive(true);
        btn_xx.gameObject.SetActive(true);
        btn_doc.gameObject.SetActive(false);
        displayScroll();
    }

    public void btn_loadClick()
    {
        Debug.Log(curName);
        btn_xxClick();
        controlObj.GetComponent<startControlScript>().loadLevel(Application.persistentDataPath+Path.DirectorySeparatorChar+"user"+Path.DirectorySeparatorChar+curName);
        controlObj.GetComponent<startControlScript>().btn_startClick();
        

    }

    public void btn_xxClick()
    {
        while(sstack.Count!=0)
        {
            Destroy(sstack.Pop());
        }

        t_scrollview.gameObject.SetActive(false);
        btn_load.gameObject.SetActive(false);
        btn_xx.gameObject.SetActive(false);
        btn_doc.gameObject.SetActive(true);
    }

}
