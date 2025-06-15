using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{

    public void OnStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
