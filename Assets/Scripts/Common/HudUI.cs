using UnityEngine;

// 게임에 실질적으로 영향을 주지 않는 유저 인터페이스 스크립트 (체력바, 플레이 시간, 스킬 쿨타임 등)
public class HudUI : MonoBehaviour
{
    public NonsenseUI nonsenseUI;
    public WireUI wireUI;
    public PlayerUI playerUI;
    public MissionUI missionUI;
    void Start()
    {

    }
    void Update()
    {

    }

    public void Init() // 초기화 함수
    {
        playerUI.Init();
        wireUI.Show(false);
        wireUI.Init();
        nonsenseUI.Show(false);
        nonsenseUI.Init();
        missionUI.Init();
    }
}
