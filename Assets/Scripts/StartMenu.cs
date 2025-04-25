using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // 메인 게임씬으로 이동
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료!");
        Application.Quit(); // 실제 빌드에서만 동작
    }
}
