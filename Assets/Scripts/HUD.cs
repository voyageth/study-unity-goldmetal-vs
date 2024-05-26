using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum HUDInfoType
    {
        EXP,
        LEVEL,
        KILL_TEXT,
        TIME,
        HEALTH,
    }
    public HUDInfoType type;
    Text text;
    Slider slider;

    private void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case HUDInfoType.EXP:
                slider.value = GameManager.instance.GetExpPercentage();
                break;
            case HUDInfoType.LEVEL:
                text.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case HUDInfoType.KILL_TEXT:
                text.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case HUDInfoType.TIME:
                (int, int) time = GameManager.instance.GetRemainTime();
                text.text = string.Format("{0:D2}:{1:D2}", time.Item1, time.Item2);
                break;
            case HUDInfoType.HEALTH:
                slider.value = GameManager.instance.GetHealthPercentage();
                break;
            default:
                break;
        }
    }
}
