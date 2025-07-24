using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainMissionText;
    [SerializeField] TextMeshProUGUI subMissionGeneratorText;
    [SerializeField] TextMeshProUGUI subMissionLifeSupportText;
    [SerializeField] TextMeshProUGUI emergencyMissionText;

    Color commonColor;
    Color warningColor;
    Color dangerColor;

    void Start()
    {
        commonColor = HexColor("#8FFF8D");
        warningColor = HexColor("#FFDA00");
        dangerColor = HexColor("#FF462B");
    }

    public void Init()
    {
    }
    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        emergencyMissionText.gameObject.SetActive(MissionManager.Inst.IsNeedWelding);



        if (MissionManager.Inst.GetIsWireAllSuccess())
        {
            mainMissionText.text = string.Format("- 통신기 수리를 완료 하여 구조선을 부르세요!");
        }
        else
        {
            mainMissionText.text = string.Format("- 통신기 수리를 위해 배선 수리를 완료하세요! {0} / 4", MissionManager.Inst.WireAllSuccessNum);
        }
        if (MissionManager.Inst.nonsense.IsSuccess)
        {
            mainMissionText.text = string.Format("- 구조선이 올 때까지 생존하세요!");
        }

        if (MissionManager.Inst.LifeSupportGauge > 50.0f)
        {
            subMissionLifeSupportText.text = string.Format("- 생명유지장치가 0% 가 되지 않도록 주의하세요!\n0% 가 되면 플레이어가 사망합니다.");
            subMissionLifeSupportText.color = commonColor;
        }
        if (MissionManager.Inst.LifeSupportGauge <= 50.0f)
        {
            subMissionLifeSupportText.text = string.Format("- !주의! 생명유지장치가 50% 미만입니다.");
            subMissionLifeSupportText.color = warningColor;
        }
        if (MissionManager.Inst.LifeSupportGauge < 25.0f)
        {
            subMissionLifeSupportText.text = string.Format("- !위험! 생명유지장치가 25% 미만입니다.\n어서 산소를 넣으세요!");
            subMissionLifeSupportText.color = dangerColor;
        }

        if (MissionManager.Inst.GeneratorGauge > 50.0f)
        {
            subMissionGeneratorText.text = string.Format("- 발전기가 0% 가 되지 않도록 주의하세요!\n0% 가 되면 생명유지장치 수치가 급감합니다.");
            subMissionGeneratorText.color = commonColor;
        }
        if (MissionManager.Inst.GeneratorGauge <= 50.0f)
        {
            subMissionGeneratorText.text = string.Format("- !주의! 발전기 충전량이 50% 미만입니다.");
            subMissionGeneratorText.color = warningColor;
        }
        if (MissionManager.Inst.GeneratorGauge < 25.0f)
        {
            subMissionGeneratorText.text = string.Format("- !위험! 발전기 충전량이 25% 미만입니다.\n어서 발전기를 돌리세요!");
            subMissionGeneratorText.color = dangerColor;
        }
    }
    public static Color HexColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }

        Debug.LogError("[UnityExtension::HexColor]invalid hex code - " + hexCode);
        return Color.white;
    }
}
