using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuButton : MonoBehaviour
{
    // 이 함수는 버튼의 OnClick 이벤트에 연결됩니다.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}