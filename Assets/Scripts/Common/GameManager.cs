using SmoothShakeFree;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _inst;
    public static GameManager Inst
    {
        get
        {
            _inst = FindAnyObjectByType<GameManager>();
            if (_inst == null)
            {
                GameObject obj = new GameObject("GameManager");
                _inst = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }
            return _inst;
        }
    }

    public GameUI game;
    public HudUI hud;

    public Camera mainCamera;
    void Start()
    {
        Init();
    }
    public void RestartGame()
    {

    }

    private void Init() // 게임 전체 초기화 함수
    {
        game.Init();
        hud.Init();
    }

    void Update()
    {

    }

    public void CameraShake()
    {
        var shake = mainCamera.GetComponent<SmoothShake>();
        if(shake != null) shake.StartShake();
    }
}
