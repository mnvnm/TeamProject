using UnityEngine;

public class TogglePanelWithEsc : MonoBehaviour
{
    public GameObject panel;  // 나타나고 사라질 패널

    private bool isPanelVisible = false;

    void Start()
    {
        // 시작할 때는 패널이 꺼져 있어야 함
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPanelVisible = !isPanelVisible;

            if (panel != null)
            {
                panel.SetActive(isPanelVisible);
            }
        }
    }
}
