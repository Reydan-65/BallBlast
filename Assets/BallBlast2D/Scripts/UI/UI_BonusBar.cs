using UnityEngine.UI;
using UnityEngine;

public class UI_BonusBar : UI_Bar
{
    private void Awake()
    {
        ResetBar();
    }

    private void Update()
    {
        Bar_FullToEmpty(ref isStoped, ref barImage, ref barTimer);
    }

    protected override void Bar_FullToEmpty(ref bool stoped, ref Image bar, ref float barTimer)
    {
        base.Bar_FullToEmpty(ref stoped, ref bar, ref barTimer);

        barImage.fillAmount -= Time.deltaTime / barTimer;

        if (barImage.fillAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}