using UnityEngine;

public class Wire : InteractableObject
{
    public int WireIndex = 0;
    public bool IsSuccess = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Init()
    {
        base.Init();
        GameManager.Inst.hud.wireUI.Init();
    }

    public override void Interactive()
    {
        if (IsSuccess) return; // 이미 완료한 와이어는 반환
        base.Interactive();
        Debug.Log("안 ㅣ띠발 와이어 유아이 오픈한다니꼐? : " + isInteractContinue);
        MissionManager.Inst.WireingIndex = WireIndex;
        GameManager.Inst.hud.wireUI.Show(isInteractContinue);
        GameManager.Inst.hud.wireUI.Init();
    }
}
