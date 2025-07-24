using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject panelObj;
    [SerializeField] GameObject playerDeadObj;
    float screenWidth = -Screen.width / 2;

    public void Init()
    {
        if (panelObj != null) panelObj.SetActive(false);
    }
    void Start()
    {
        playerDeadObj.transform.localPosition = new Vector2(screenWidth - 70, 0);
    }
    public void Show()
    {
        if (panelObj != null) panelObj.SetActive(true);
        playerDeadObj.transform.localPosition = new Vector2(screenWidth - 70, 0);

    }
    void Update()
    {
        if (!panelObj.activeSelf || !GameManager.Inst.IsGameOver) return;
        playerDeadObj.transform.Rotate(new Vector3(0f, 0f, 0.4f));
        playerDeadObj.transform.localPosition = new Vector3(playerDeadObj.transform.localPosition.x + (60 * Time.deltaTime), 0, 0);
    }
}
