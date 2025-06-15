using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public float time;

    public Text Text_Timer;
    
    private PlayerData playerData;
    public bool isTimerRunning = true;
    
    void Start()
    {
        if (playerData == null)
        {
            playerData = FindObjectOfType<PlayerData>();
        }
        if(Text_Timer == null)
        {
            Text_Timer = GetComponent<Text>();
        }
    }

    void Update()
    {
        TimerUI();
        PlayerDie();
    }
    void TimerUI()
    {
        if (!isTimerRunning) return;
        
        time += Time.deltaTime;
        Text_Timer.text = "시간 : " + Mathf.Round(time);
    }

    void PlayerDie()
    {
        if (playerData.currentHp <= 0)
        {
            isTimerRunning = false;
        }
    }
}
