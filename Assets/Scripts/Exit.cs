using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    void Start()
    {
        // "Exit"라는 이름의 버튼을 찾아서 Button 컴포넌트 가져오기
        Button exitButton = GameObject.Find("Exit").GetComponent<Button>();

        // 버튼 클릭 시 OnExitGame 함수 호출
        exitButton.onClick.AddListener(OnExitGame);
    }

    void OnExitGame()
    {
        // 에디터에서는 종료되지 않으므로 로그 출력
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}