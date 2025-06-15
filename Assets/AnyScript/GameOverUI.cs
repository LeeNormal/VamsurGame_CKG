using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public GameObject GameOverPanel;
    private UI_Manager UImgr;
    
    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //UImgr.isTimerRunning = true;
    }

    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
