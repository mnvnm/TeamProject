using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider lifeSupportGaugeBar;
    [SerializeField] Slider generatorGaugeBar;
    [SerializeField] Image dangerImg;
    bool reserve = false;
    float alpha = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Init()
    {
        lifeSupportGaugeBar.value = 1;
        generatorGaugeBar.value = 1;
        dangerImg.color = new Color(dangerImg.color.r, dangerImg.color.g, dangerImg.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        lifeSupportGaugeBar.value = MissionManager.Inst.LifeSupportGauge / MissionManager.Inst.MaxLifeSupportGauge;
        generatorGaugeBar.value = MissionManager.Inst.GeneratorGauge / MissionManager.Inst.MaxGeneratorGauge;

        if (MissionManager.Inst.IsNeedWelding)
        {
            if (alpha >= 0.2f || alpha <= 0f)
            {
                if (alpha > 0.2f) alpha = 0.2f;
                if (alpha < 0f) alpha = 0f;
                reserve = !reserve;
            }
            alpha += Time.deltaTime * (reserve ? -1 : 1) * 0.25f;
            dangerImg.color = new Color(dangerImg.color.r, dangerImg.color.g, dangerImg.color.b, alpha);
        }
        else if(alpha > 0 && !MissionManager.Inst.IsNeedWelding)
        {
            alpha -= Time.deltaTime * 0.25f;
            dangerImg.color = new Color(dangerImg.color.r, dangerImg.color.g, dangerImg.color.b, alpha);
        }
    }
}
