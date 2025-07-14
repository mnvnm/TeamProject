using UnityEngine;

// 게임에 직접 영향을 주는 오브젝트를 관리하기 위한 스크립트 (플레이어, 적, 총알 등)
public class GameUI : MonoBehaviour
{
    public PlayerController player;
    void Start()
    {

    }
    void Update()
    {

    }

    public void Init() // 초기화 함수
    {
        player.Init();
    }

}
