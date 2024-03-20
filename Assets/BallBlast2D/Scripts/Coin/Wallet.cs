using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    private int amountCoin;
    private int spentAmountCoin;

    [HideInInspector] public UnityEvent Change_CoinAmount;

    public int AmountCoin { get => amountCoin; set => amountCoin = value; }
    public int SpentAmountCoin { get => spentAmountCoin; set => spentAmountCoin = value; }

    // ���������� �����
    public void AddCoins(int amount)
    {
        amountCoin += amount;

        Change_CoinAmount.Invoke();
    }

    // �������� �����
    public bool DrawCoin(int amount)
    {
        if (amountCoin - amount < 0) return false;

        amountCoin -= amount;
        spentAmountCoin += amount;

        Change_CoinAmount.Invoke();

        return true;
    }

    // ����� ���������� ������
    public void Reset_SpentAmountCoin()
    {
        spentAmountCoin = 0;
    }

    // ��������� �������� ���������� �����
    public int GetAmountCoin()
    {
        return amountCoin;
    }

    // ��������� ���������� ������
    public int GetSpentAmountCoin()
    {
        return spentAmountCoin;
    }

    // ����� �������� ��������
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