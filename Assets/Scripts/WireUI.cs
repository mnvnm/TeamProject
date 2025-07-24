using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireUI : MonoBehaviour
{
    public int curWireNum = -1;
    int wireCount = 4;
    [SerializeField] List<Button> leftBtns = new List<Button>(); // 왼쪽 버튼 리스트
    [SerializeField] List<Button> rightBtns = new List<Button>(); // 오른쪽 버튼 리스트
    [SerializeField] List<LineRenderer> wireLineRenderer = new List<LineRenderer>(); // 배선 연걸을 보여줄 

    int[] setWireNum = { };

    List<int> leftBtnColorNumList = new List<int>();
    List<int> rightBtnColorNumList = new List<int>();

    [HideInInspector] public List<int> successNums = new List<int>(); // 각 와이어가 연결되었는지 확인하는 변수
    //기본적으로 왼쪽을 기준으로 하여 왼쪽과 오른쪽이 이어졌을때 해당 왼쪽 버튼의 인덱스를 1로 하여 완료 되었음을 저장  
    int curPickWireNum = 0; // 선택한 와이어 번호
    int curBtnIndex = 10; // 선택한 버튼 인덱스

    [SerializeField] GameObject panelObj;

    void Start()
    {
    }

    void Update()
    {
        if (GameManager.Inst.IsGameOver) return;
        if (!GetIsShow()) return;
        // 마우스 포인터를 따라오는 라인 렌더러
        for (int i = 0; i < wireLineRenderer.Count; i++)
        {
            if (!wireLineRenderer[i].enabled) continue;
            if (curBtnIndex != i) continue;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wireLineRenderer[i].SetPosition(0, leftBtns[i].transform.position);
            if (successNums == null || successNums.Count < wireLineRenderer.Count) continue;
            if (successNums[i] != 1 && curBtnIndex != 10)
                wireLineRenderer[i].SetPosition(1, mousePos);
        }

    }
    public void Show(bool isShow)
    {
        panelObj.SetActive(isShow);
    }

    public void Init() // 모든 Wire 관련 UI 초기화
    {
        curBtnIndex = 10;
        if (curWireNum == MissionManager.Inst.WireingIndex) return;
        curWireNum = MissionManager.Inst.WireingIndex;

        for (int i = 0; i < wireLineRenderer.Count; i++)
            wireLineRenderer[i].enabled = false;
        ShuffleWire();
        SetButtonColor();
    }

    void ShuffleWire()
    {
        // 1. setWireNum에 중복 없는 난수 생성 및 저장
        setWireNum = new int[wireCount];
        List<int> uniqueRandoms = new List<int>();
        for (int i = 0; i < wireCount; i++)
            uniqueRandoms.Add(i + 1);

        // Fisher-Yates Shuffle
        for (int i = 0; i < uniqueRandoms.Count; i++)
        {
            int rnd = Random.Range(i, uniqueRandoms.Count);
            int temp = uniqueRandoms[i];
            uniqueRandoms[i] = uniqueRandoms[rnd];
            uniqueRandoms[rnd] = temp;
        }
        for (int i = 0; i < wireCount; i++)
            setWireNum[i] = uniqueRandoms[i];

        // 2. 왼쪽 리스트에 setWireNum 값 복사
        leftBtnColorNumList = new List<int>(setWireNum);

        // 3. 오른쪽 리스트에 왼쪽 값 복사 후 섞기
        rightBtnColorNumList = new List<int>(leftBtnColorNumList);
        for (int i = 0; i < rightBtnColorNumList.Count; i++)
        {
            int rnd = Random.Range(i, rightBtnColorNumList.Count);
            int temp = rightBtnColorNumList[i];
            rightBtnColorNumList[i] = rightBtnColorNumList[rnd];
            rightBtnColorNumList[rnd] = temp;
        }

        Debug.Log("Init 함수 들어왔으며, successNums 는 : " + successNums);
        successNums.Clear();
        for (int i = 0; i < setWireNum.Length; i++)
        {
            successNums.Add(0);
        }
    }

    void SetButtonColor()
    {
        for (int i = 0; i < leftBtnColorNumList.Count; i++)
        {
            Color btnColor = Color.white;
            switch (leftBtnColorNumList[i])
            {
                case 1:
                    btnColor = Color.red;
                    break;
                case 2:
                    btnColor = Color.green;
                    break;
                case 3:
                    btnColor = Color.blue;
                    break;
                case 4:
                    btnColor = Color.yellow;
                    break;
            }
            leftBtns[i].image.color = btnColor;
        }
        for (int i = 0; i < rightBtnColorNumList.Count; i++)
        {
            Color btnColor = Color.white;
            switch (rightBtnColorNumList[i])
            {
                case 1:
                    btnColor = Color.red;
                    break;
                case 2:
                    btnColor = Color.green;
                    break;
                case 3:
                    btnColor = Color.blue;
                    break;
                case 4:
                    btnColor = Color.yellow;
                    break;
            }
            rightBtns[i].image.color = btnColor;
        }
    }

    public void OnClickWire(int index)
    {
        if (successNums[index] == 1)
        {
            return;
        }
        curPickWireNum = leftBtnColorNumList[index];
        if (curBtnIndex != 10 && curBtnIndex != index) // 만약 선택한 와이어가 있는데 다른 와이어를 클릭시
        {
            wireLineRenderer[curBtnIndex].enabled = false; // 기존 선택했던 와이어 라인렌더러를 비활성화
            GameManager.Inst.CameraShakeWire();
        }
        curBtnIndex = index;
        wireLineRenderer[index].enabled = true; // 현재 새로 선택한 와이어 라인렌더러 활성화
    }

    public void OnClickMatchWire(int index)
    {
        if (curBtnIndex == 10) return;
        if (curPickWireNum == rightBtnColorNumList[index])
        {
            successNums[curBtnIndex] = 1;
            wireLineRenderer[curBtnIndex].SetPosition(1, rightBtns[index].transform.position);
            curBtnIndex = 10;
            
            if (MissionManager.Inst.CheckIndexWireSuccess(curWireNum, successNums)) curWireNum = 10;
        }
        else
        {
            GameManager.Inst.CameraShakeWire();
            wireLineRenderer[curBtnIndex].enabled = false;
            curPickWireNum = 0;
            curBtnIndex = 10;
        }
    }

    public bool GetIsShow()
    {
        return gameObject.activeInHierarchy;
    }
}
