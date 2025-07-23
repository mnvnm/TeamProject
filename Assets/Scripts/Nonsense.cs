using System.Collections.Generic;
using UnityEngine;

public class Nonsense : InteractableObject
{
    [SerializeField] GameObject SuccessObj;
    public bool IsSuccess = false;
    public List<string> nonsenseProblem = new List<string>();

    public List<string> nonsenseAnswer = new List<string>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isInteractContinue)
        {
            isInteractContinue = !isInteractContinue;
            GameManager.Inst.hud.nonsenseUI.Show(isInteractContinue);
            GameManager.Inst.game.player.SetIsInteractive(isInteractContinue);
        }
    }
    public override void Init()
    {
        base.Init();
        IsSuccess = false;
        SuccessObj.SetActive(false);
        GameManager.Inst.hud.nonsenseUI.Init();
    }

    public override void Interactive()
    {
        if (IsSuccess) return; // 이미 완료한 와이어는 반환
        base.Interactive();
        GameManager.Inst.hud.nonsenseUI.Show(isInteractContinue);
    }

    public bool CheckAnswer(string inputText, int problemIndex)
    {
        return inputText == nonsenseAnswer[problemIndex];
    }
    public void Success()
    {
        IsSuccess = true;
        isInteractContinue = false;
        SuccessObj.SetActive(false);
    }

    public void ShowInteractableObj()
    {
        SuccessObj.SetActive(true);
    }
}
