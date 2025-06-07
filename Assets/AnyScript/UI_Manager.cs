using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public float time;

    public Text Text_Timer;

    void Start()
    {
        if(Text_Timer == null)
        {
            Text_Timer = GetComponent<Text>();
        }
        else { }
    }

    void Update()
    {
        TimerUI();
    }
    void TimerUI()
    {
        time += Time.deltaTime;
        Text_Timer.text = "½Ã°£ : " + Mathf.Round(time);
    }
}
