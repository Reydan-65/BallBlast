using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    private int amountCoin;
    private int spentAmountCoin;

    [HideInInspector] public UnityEvent Change_CoinAmount;

    public int AmountCoin { get => amountCoin; set => amountCoin = value; }
    public int SpentAmountCoin { get => spentAmountCoin; set => spentAmountCoin = value; }

    // Начисление монет
    public void AddCoins(int amount)
    {
        amountCoin += amount;

        Change_CoinAmount.Invoke();
    }

    // Списание монет
    public bool DrawCoin(int amount)
    {
        if (amountCoin - amount < 0) return false;

        amountCoin -= amount;
        spentAmountCoin += amount;

        Change_CoinAmount.Invoke();

        return true;
    }

    // Сброс количества затрат
    public void Reset_SpentAmountCoin()
    {
        spentAmountCoin = 0;
    }

    // Получение текущего количества монет
    public int GetAmountCoin()
    {
        return amountCoin;
    }

    // Получение количества затрат
    public int GetSpentAmountCoin()
    {
        return spentAmountCoin;
    }

    // сброс значений кошелька
    public void Reset_Wallet()
    {
        amountCoin = 0;
        DrawCoin(amountCoin);
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5) == true) AddCoins(100);
    }

#endif

}