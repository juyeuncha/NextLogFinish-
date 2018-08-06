
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play,
    Pause,
    End
}

public class coinmanager : MonoBehaviour
{

    public delegate void SpeedLevel();
    SpeedLevel speedLevel;
    public static coinmanager instance;
    public GameState GS;
    public ground_ctrl gr_ctrl;
    public GlobVar globvar;

    void Awake()
    {
        instance = this;

    }

    void OnEnable()
    {
        speedLevel += gr_ctrl.ChSpeed;
    }


    public Text text_coincnt;
    public Text text_mcnt;
    public Text final_m;
    public Text final_coin;
    public float Meter;

    public GameObject pause_ui;
    public GameObject finish_ui;

    int coincount = 0;

    public void GetCoin()
    {
        coincount++;

        text_coincnt.text = "획득코인: " + coincount;

    }

    public void Update()
    {
        if (GS == GameState.Play)
        {

            speedLevel();
            globvar.mtspeed += globvar.mtchspeed;
            Meter += Time.deltaTime * globvar.mtspeed;
            text_mcnt.text = string.Format("달린거리: {0:0.00}", Meter);
      
        }


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
}
