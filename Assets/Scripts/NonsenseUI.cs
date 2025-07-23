using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonsenseUI : MonoBehaviour
{

    public int curNonsenseNum = 0;

    [SerializeField] InputField answerInput;
    [SerializeField] Text problemText;

    [SerializeField] GameObject panelObj;

    string m_problem = "";
    void Start()
    {

    }

    void Update()
    {

    }
    public void Show(bool isShow)
    {
        panelObj.SetActive(isShow);
        if (isShow)
        {
            if (curNonsenseNum < MissionManager.Inst.nonsense.nonsenseProblem.Count)
                SetProblem(MissionManager.Inst.nonsense.nonsenseProblem[curNonsenseNum]);
        }
    }

    public void Init() // 초기화 함수
    {
        curNonsenseNum = 0;
    }

    public void SetProblem(string problem)
    {
        Debug.Log("문제 설정 // 문제는 : " + problem);
        m_problem = problem;
        problemText.text = m_problem;
    }

    public void CheckAnswer()
    {
        if (MissionManager.Inst.nonsense.CheckAnswer(answerInput.text, curNonsenseNum))
        {
            curNonsenseNum++;
            if (curNonsenseNum < MissionManager.Inst.nonsense.nonsenseProblem.Count) // 문제가 더 있는지 확인
            {
                SetProblem(MissionManager.Inst.nonsense.nonsenseProblem[curNonsenseNum]);
            }
            else // 다 풀었다면
            {
                Show(false);
                MissionManager.Inst.nonsense.IsSuccess = true;
                MissionManager.Inst.nonsense.isInteractContinue = false;
                MissionManager.Inst.nonsense.Success();
                GameManager.Inst.game.player.SetIsInteractive(false);
            }
        }
        answerInput.text = "";
    }
}
