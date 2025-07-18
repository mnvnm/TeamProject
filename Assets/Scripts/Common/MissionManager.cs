using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour // 미션 관련 스크립트
{
    private static MissionManager _inst;
    public static MissionManager Inst
    {
        get
        {
            _inst = FindAnyObjectByType<MissionManager>();
            if (_inst == null)
            {
                GameObject obj = new GameObject("MissionManager");
                _inst = obj.AddComponent<MissionManager>();
                DontDestroyOnLoad(obj);
            }
            return _inst;
        }
    }

    public bool IsInteractable = false; // 상호작용 가능한 상태인지 확인하는 논리 변수
    public bool IsCarryingOxygen = false; // 산소 운반중인지 확인하는 논리 변수
    public bool IsNeedWelding = false; // 용접이 필요한지 확인하는 논리 변수

    public float LifeSupportGauge = 100;
    public float MaxLifeSupportGauge = 100;
    public float GeneratorGauge = 100;
    public float MaxGeneratorGauge = 100;
    public WeldingPoint[] weldingPoints;
    void Start()
    {
        Init();
    }

    public void Init() // 초기화
    {
        LifeSupportGauge = MaxLifeSupportGauge;
        GeneratorGauge = MaxGeneratorGauge;
        IsCarryingOxygen = false;
        IsNeedWelding = false;
    }

    void Update()
    {
        SetNeedWelding();
    }

    void SetNeedWelding()
    {
        for (int i = 0; i < weldingPoints.Length; i++)
        {
            if (!weldingPoints[i].activeSelf)
            {
                IsNeedWelding = true;
                break;
            }
            IsNeedWelding = false;
        }
    }
}
