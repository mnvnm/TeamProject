using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUI : MonoBehaviour
{
    [SerializeField] GameObject panel;  // 나타나고 사라질 패널
    private bool isPanelVisible = false;

    public void Init()
    {
        panel.SetActive(false);
    }
    // 이 함수는 버튼의 OnClick 이벤트에 연결됩니다.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !MissionManager.Inst.nonsense.isInteractContinue)
        {
            isPanelVisible = !isPanelVisible;

            if (panel != null)
            {
                panel.SetActive(isPanelVisible);
            }
        }
    }
    // 이 함수는 Button의 OnClick 이벤트에 연결됩니다.
    public void ExitGame()
    {
        // 유니티 에디터에서는 Play 모드 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서는 종료
        Application.Quit();
#endif
    }
}