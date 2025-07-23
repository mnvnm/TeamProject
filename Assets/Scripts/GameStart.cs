using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        // "GameStart"라는 이름의 버튼 오브젝트를 찾아서 Button 컴포넌트 가져오기
        Button startButton = GameObject.Find("GameStart").GetComponent<Button>();

        // 버튼 클릭 시 OnStartGame 함수 호출
        startButton.onClick.AddListener(OnStartGame);
    }

    void OnStartGame()
    {
        // "GameScene"은 시작할 씬의 이름입니다. 필요에 따라 변경하세요.
        SceneManager.LoadScene("TestJhin");
    }
}