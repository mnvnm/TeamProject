using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nonsense : InteractableObject
{
    [SerializeField] GameObject SuccessObj;
    [SerializeField] GameObject exitObj;   // 출구 오브젝트
    [SerializeField] GameObject doorObj;   // 출구문 오브젝트
    [SerializeField] float exitDelay = 60f; // 지연 시간(초)
    [SerializeField] TMPro.TextMeshProUGUI exitTimerText; // 남은 시간 표시용
    private BoxCollider2D doorCol2D;
    public bool IsSuccess = false;
    public List<string> nonsenseProblem = new List<string>();

    public List<string> nonsenseAnswer = new List<string>();
    void Start()
    {
        if (exitObj != null)
            exitObj.SetActive(false);

        if (exitObj != null)
            doorCol2D = doorObj.GetComponent<BoxCollider2D>();
        
        if (exitTimerText != null)
            exitTimerText.gameObject.SetActive(false);
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

        // 60초 뒤 출구 등장
        StartCoroutine(ShowExitAfterDelay());
    }

    private IEnumerator ShowExitAfterDelay()
    {
        float remain = exitDelay;
        if (exitTimerText != null)
            exitTimerText.gameObject.SetActive(true);

        while (remain > 0f)
        {
            // 소수점 필요 없으면 CeilToInt, 필요하면 ToString("F1") 등
            if (exitTimerText)
                exitTimerText.text = $"RESCUE SHIP ARRIVES : {Mathf.CeilToInt(remain)}";

            yield return null;               // 매 프레임 갱신
            remain -= Time.deltaTime;
        }

        exitTimerText.gameObject.SetActive(false);

        if (exitObj != null)
            exitObj.SetActive(true); 
        if (doorCol2D != null)
            doorCol2D.enabled = false;
    }

    public void ShowInteractableObj()
    {
        SuccessObj.SetActive(true);
    }
}
