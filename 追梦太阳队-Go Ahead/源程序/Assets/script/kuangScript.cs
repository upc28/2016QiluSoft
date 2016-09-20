using UnityEngine;
using System.Collections;

public class kuangScript : MonoBehaviour {

    private SpriteRenderer spr;
    private Texture2D texture2d;
    private Vector3 position;
    public Texture2D pic_com1,pic_com2,pic_com3,barrier4,kuang;

    // Use this for initialization
    void Start () {

        spr = gameObject.GetComponent<SpriteRenderer>();  
    }
	
	// Update is called once per frame
	void Update () {
	   
	}
    void OnMouseDown()
    {
        position = Input.mousePosition;
    }


    void OnMouseUp()
    {
        if (position != Input.mousePosition) return;
        int t = GameObject.Find("Main Camera").GetComponent<editCameraScript>().curTag;
        if (t == 1)
        {
            texture2d = pic_com1;
        }
        else if (t == 2)
        {
            texture2d = pic_com2;
        }
        else if (t == 3)
        {
            texture2d = pic_com3;
        }
        else if(t==4)
        {
            texture2d = barrier4;
        }
        else if(t==-1)
        {
            texture2d = kuang;
        }

        else return;
        
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        spr.sprite = sp;
        Camera.main.GetComponent<editCameraScript>().write(t, new Vector2(transform.position.x,transform.position.y));
        Camera.main.GetComponent<editCameraScript>().save();
    }
}
