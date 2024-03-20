using UnityEngine;
using UnityEngine.Events;

public class Destructble : MonoBehaviour
{
    [HideInInspector] public UnityEvent Die;
    [HideInInspector] public UnityEvent Change_HitPoints;

    public int maxHitPoints;
    private int hitPoints;

    private bool isDie = false;

    private void Start()
    {
        hitPoints = maxHitPoints;
        Change_HitPoints.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        hitPoints -= damage;

        Change_HitPoints.Invoke();

        if (hitPoints <= 0) Kill();
    }

    public void Kill()
    {
        if (isDie == true) return;

        hitPoints = 0;
        isDie = true;

        Change_HitPoints.Invoke();
        Die.Invoke();

        StoneSpawner spawner = FindObjectOfType<StoneSpawner>();

        if (spawner.AmountSpawned == spawner.Amount)
        {
            UI_GameHUD calculate = FindObjectOfType<UI_GameHUD>();

            calculate.StoneLeftBar.fillAmount -= calculate.StoneLeftBarStep;
            calculate.StoneLeft--;
        }
    }

    public int GetHitPoint()
    {
        return hitPoints;
    }
}