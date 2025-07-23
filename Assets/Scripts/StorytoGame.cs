using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGameSceneWhenReady : MonoBehaviour
{
    public bool allImagesPassed = false; // 외부에서 true로 바뀌게 만드세요

    void Update()
    {
        // 이미지가 다 넘어간 상태에서 마우스 왼쪽 버튼 클릭 시 GameScene으로 전환
        if (allImagesPassed && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}