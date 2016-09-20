using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Example());
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("start");
        
    }
}
