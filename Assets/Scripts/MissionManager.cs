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

    public bool IsInteractable = false;

    public float LifeSupportGauge = 100;
    void Start()
    {
        Init();
    }

    public void Init() // 초기화
    {
        LifeSupportGauge = 100;
    }
}
