
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameState
{
    Play,
    Pause,
    End
}

public class GameManager : MonoBehaviour
{

    public delegate void SpeedLevel();
    SpeedLevel speedLevel;
    public static GameManager instance;
    public GameObject[] Selected_Char;
    //private Quaternion turn = Quaternion.identity;
    public GameState GS;
    public ground_ctrl gr_ctrl;
    public GlobVar globvar;
    public background _background;
    public player _player;
    public GameObject select_stg;

    public Text text_coincnt;
    public Text text_mcnt;
    public Text final_m;
    public Text final_coin;
    public float Meter;
    int coincount = 0;
    int selectedchar_num = 0;
    int turnValue=0;
    float lerptiming;
    float startValue;
    public GameObject pause_ui;
    public GameObject finish_ui;

    


    void Awake()
    {
        instance = this;

    }

    void OnEnable()
    {
        speedLevel += gr_ctrl.ChSpeed;
    }

    private void Start()
    {
     
        select_stg = GameObject.Find("select_stage");
        //turn.eulerAngles = new Vector3(0, turnvalue, 0);
        
    }



    public void Update()
    {
        if (GS == GameState.Play)
        {
            _background.MoveBg();
            gr_ctrl.MoveGr();
            SpLev();
            MeterCnt();
            _player.KeyCtrl();
            _player.JumpCtrl();
        }
    }

    public void SpLev()
    {
        speedLevel();
        globvar.mtspeed += globvar.mtchspeed;
    }

    public void MeterCnt()
    {
        Meter += Time.deltaTime * globvar.mtspeed;
        text_mcnt.text = string.Format("달린거리: {0:0.00}", Meter);
    }

    public void GetCoin()
    {
        coincount++;

        text_coincnt.text = "획득코인: " + coincount;

    }

    public void Pause()
    {

            GS = GameState.Pause;
            Time.timeScale = 0f;
            pause_ui.SetActive(true);
        
    }

    public void unPause()
    {
        GS = GameState.Play;
        Time.timeScale = 1f;
        pause_ui.SetActive(false);
    }

    public void GameOver()
    {
        final_m.text= string.Format("달린기록: {0:0.00}", Meter);
        final_coin.text = "획득코인: " + coincount;

        Time.timeScale = 0f;
        GS = GameState.End;
        finish_ui.SetActive(true);
    }

    public void GoIntro()
    {
        
        GS = GameState.End;
        Time.timeScale = 1f;
        SceneManager.LoadScene("run_test3");
        
    }

    public void GoPlay()
    {
        GS = GameState.Play;
        Time.timeScale = 1f;
        SceneManager.LoadScene("run_test1");
    }

    public IEnumerator TurnRotation()
    {
        lerptiming = 0;
        while (lerptiming < 1)
        {
            lerptiming += Time.deltaTime*2;
            if (lerptiming > 1)
            {
                lerptiming = 1;
            }
            Vector3 turn = new Vector3(0, Mathf.Lerp(startValue, turnValue, lerptiming), 0);
            select_stg.transform.rotation = Quaternion.Euler(turn);
            yield return null;
         
        }
        
    }

    public void SelectChar_Left()
    {
        startValue = turnValue;
        turnValue += 360/Selected_Char.Length;
        
        selectedchar_num--;
        if (selectedchar_num < 0)
        {
            selectedchar_num = 5;
        }

        //turn.eulerAngles = new Vector3(0, turnvalue, 0);
        //select_stg.transform.rotation = Quaternion.Euler(0, turnvalue, 0);
        //float start = turnvalue;
        //float end = turnvalue + (360 / Selected_Char.Length);
        StartCoroutine(TurnRotation());

        
    }

    public void SelectChar_Right()
    {
        startValue = turnValue;
        turnValue -= 360/Selected_Char.Length;

        selectedchar_num++;
        if (selectedchar_num > 5)
        {
            selectedchar_num = 0;
        }

        //select_stg.transform.rotation = Quaternion.Euler(0, turnvalue, 0);

        //float start = turnvalue;
        //float end = turnvalue - (360 / Selected_Char.Length);
        StartCoroutine(TurnRotation());
        

        
    }

}
