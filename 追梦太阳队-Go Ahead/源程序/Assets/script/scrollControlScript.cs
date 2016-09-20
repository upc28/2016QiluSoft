using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class scrollControlScript : MonoBehaviour {
    private string[] filename;
    public ScrollRect scrollContens;
    public GameObject rect;
    public Button btn_zero;
    private Transform lastBtn_Transform;
	// Use this for initialization
	void Start () {
        lastBtn_Transform = btn_zero.transform;
        Debug.Log(Application.dataPath + Path.DirectorySeparatorChar + "user");
        filename = Directory.GetFiles(Application.dataPath+Path.DirectorySeparatorChar+"user");       
        for (int i = 0; i < filename.Length; i++)
        {

            GameObject GoCloneObject = new GameObject("Button");

            GoCloneObject.transform.SetParent(rect.transform, false);

            GoCloneObject.transform.position = new Vector3(lastBtn_Transform.transform.position.x, lastBtn_Transform.transform.position.y-50, lastBtn_Transform.transform.position.z+1);

            lastBtn_Transform.transform.position = GoCloneObject.transform.position;
        }
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
