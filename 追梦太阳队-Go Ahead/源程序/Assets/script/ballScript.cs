using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ballScript : MonoBehaviour {


    private bool flag_jump;

    private Vector3 starPosition;
    private Rigidbody2D ball;
    private Vector3 starPosition_camera;
    private int gameTimes;
    private float runTime;
    public Button[] pausebtn;
    public Button btn_pause;
    public int upForce;
    public float speed;
    public float jump_speed;
    public GameObject control;
    public TextMesh textMesh;
    public GameObject bgObj,touchTop;
    public Text winTime, winTimes;
    public AudioSource audiosource,audiosourceControl;
    public AudioClip music_level, music_menu;

    //public GameObject preBall;
    private bool finish_flag;
    private Vector3 bg_zero;


    // Use this for initialization
    void Start () {
        bg_zero = control.GetComponent<startControlScript>().bg_zero;
        bgObj.transform.position = bg_zero;

        flag_jump = false;
        finish_flag = false;

        gameTimes = 1;
        ball = GetComponent<Rigidbody2D>();
        Input.multiTouchEnabled = false;
        foreach(Button a in pausebtn)
        {
            a.gameObject.SetActive(false);
        }    
        starPosition = transform.position;
        starPosition_camera = control.GetComponent<startControlScript>().reStartCamera();
        Camera.main.transform.position = starPosition_camera;

        textMesh.text = gameTimes.ToString();
    }

    public void init()
    {
        textMesh.text = "1";
        gameTimes = 1;
        textMesh.gameObject.SetActive(true);
        runTime = Time.time;
        audiosource.Play();
    }

    public void btn_pauseClick()
    {
        btn_pause.gameObject.SetActive(false);
        foreach (Button b in pausebtn)
        {
            
            b.gameObject.SetActive(true);
            Debug.Log(b.name);
        }
        audiosource.Pause();
        Time.timeScale = 0;
        
    }

    public void btn_continueClick()
    {
        foreach (Button b in pausebtn)
        {

            b.gameObject.SetActive(false);
            Debug.Log(b.name);
        }
        Time.timeScale = 1;
        btn_pause.gameObject.SetActive(true);
        audiosource.Play();
    }

    public void btn_return()
    {
        reStar();
        gameTimes = 1;
        btn_pause.gameObject.SetActive(false);
        foreach (Button b in pausebtn)
        {

            b.gameObject.SetActive(false);
        }
        control.GetComponent<startControlScript>().enabled = true;
        textMesh.gameObject.SetActive(false);
        if(finish_flag)
        {
            finish_flag = false;
            winTime.gameObject.SetActive(false);
            winTimes.gameObject.SetActive(false);
        }
        Debug.Log("load: xml " + "Level" + control.GetComponent<startControlScript>().text.text);
        control.GetComponent<startControlScript>().loadLevelXML("Level"+control.GetComponent<startControlScript>().text.text);
        audiosource.Stop();
        audiosourceControl.Play();
        control.GetComponent<startControlScript>().restart();
        Time.timeScale = 1;   
        GetComponent<ballScript>().enabled = false;
    }

    public void btn_restart()
    {
        
        foreach (Button b in pausebtn)
        {
            b.gameObject.SetActive(false);
        }
        btn_pause.gameObject.SetActive(true);
        if (finish_flag)
        {
            finish_flag = false;
            winTime.gameObject.SetActive(false);
            winTimes.gameObject.SetActive(false);
            init();
        }
        Time.timeScale = 1;
        
        reStar();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ball.velocity.x);
        //Debug.Log(ball.velocity.y);
        transform.Translate(speed * Time.deltaTime, 0f, 0f);
        Camera.main.transform.Translate(Time.deltaTime *speed , 0, 0);
        bgObj.transform.Translate(Time.deltaTime * (speed - 0.048f), 0, 0);
    }
	void FixedUpdate ()
    {
        if ((ball.velocity.y == 0) && (Input.GetKey(KeyCode.Space) || (Input.touchCount == 1 && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < touchTop.transform.position.y)))
        {            
            ball.velocity = Vector3.up * jump_speed;
            StartCoroutine(Example2(0.001f));
            StartCoroutine(Example1(0.20f));
        }
        if(ball.velocity.y<0&&ball.velocity.y>-5)
        {
            ball.velocity = Vector3.down * jump_speed;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (flag_jump )
        {
            flag_jump = false;

        }
        string str = coll.gameObject.tag;
        if (str.Equals("1"))
        {
            //Debug.Log("coll tag 1");
            //if ((Mathf.Abs(coll.gameObject.transform.position.x - transform.position.x) >= coll.collider.bounds.size.x / 2*1.1) && (transform.position.y*1.1 < (coll.collider.bounds.size.y / 2 + coll.gameObject.transform.position.y)))
            //{
            //ball.velocity = Vector2.zero;
            Debug.Log("tag 1");
                reStar();
           // }
        }
        else if (str.Equals("2"))
        {
            ball.velocity = Vector2.up*0;
            reStar();
        }
        else if(str.Equals("4"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ball.velocity = Vector3.up * jump_speed;
                StartCoroutine(Example2(0.001f));
                StartCoroutine(Example1(0.20f));
            }
        }
        else if (str.Equals("10"))
        {
            finishGame();
            audiosource.Stop();
        }
        else;
    }

    void finishGame()
    {
        btn_pauseClick();
        pausebtn[0].gameObject.SetActive(false);
        finish_flag = true;
        float totalTime = Time.time - runTime;
        winTimes.text="Times:" + gameTimes.ToString() ;
        winTime.text = "TIME:" + string.Format("{0:F}", totalTime) + "s";
        winTime.gameObject.SetActive(true);
        winTimes.gameObject.SetActive(true);

    }

   
    IEnumerator Example(float ttime)
    {
        yield return  new WaitForSeconds(ttime);
        reStar();

    }
    IEnumerator Example1(float ttime)
    {
        yield return new WaitForSeconds(ttime);
        ball.velocity = Vector3.down * jump_speed*0.8f;
        //StartCoroutine(Example2(0.5f));
    }
    IEnumerator Example2(float ttime)
    {
        yield return new WaitForSeconds(ttime);
        flag_jump = true;
        //flag_jump = false;
    }


    void reStar()
    {
        gameTimes++;
        bgObj.transform.position = bg_zero;
        ball.velocity = new Vector2(ball.velocity.x, 0f);
        transform.position = starPosition;
        audiosource.Stop();
        audiosource.Play();
        Camera.main.transform.position = starPosition_camera;
        textMesh.text = gameTimes.ToString();
    }
   

}
