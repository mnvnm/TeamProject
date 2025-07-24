using UnityEngine;

public class ExitButton : MonoBehaviour
{
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
