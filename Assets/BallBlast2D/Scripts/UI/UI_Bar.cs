using UnityEngine.UI;
using UnityEngine;

public class UI_Bar : MonoBehaviour
{
    [SerializeField] protected Image barImage;

    [Space(10)]
    protected float colorR;
    protected float colorG;
    protected float colorB;

    [Space(10)]
    [SerializeField] protected float fillAmount;

    protected bool isStoped;

    protected float barTimer;
    public float BarTimer { get => barTimer; set => barTimer = value; }

    protected virtual void Bar_FullToEmpty(ref bool stoped, ref Image bar, ref float barTimer)
    {
        if (stoped == false)
        {
            if (bar.fillAmount > 0.75f) bar.color = Color.green;
            else if (bar.fillAmount <= 0.75f)
            {
                if (colorR < 1f) bar.color = new Color(colorR += Time.deltaTime / barTimer / 0.325f, 1, 0, 1);
                if (colorG > 0f && colorR >= 1f) bar.color = new Color(1, colorG -= Time.deltaTime / barTimer / 0.325f, 0, 1);
            }
            else if (bar.fillAmount <= 0.1f) bar.color = Color.red;
        }
    }

    public void ResetBar()
    {
        barImage.fillAmount = 1;

        colorR = 0;
        colorG = 1;
        colorB = 0;
    }
}