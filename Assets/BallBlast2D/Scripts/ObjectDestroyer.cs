using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private Cart cart;

    public void DestroyAllStones()
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");

        foreach (GameObject stone in stones)
        {
            Destroy(stone);
        }
    }

    public void DestroyAllCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
    }

    public void DestroyAllBonuses()
    {
        GameObject[] bonuses = GameObject.FindGameObjectsWithTag("Bonus");

        foreach (GameObject bonus in bonuses)
        {
            Destroy(bonus);
        }

        cart.IsInvuled = false;
    }

    public void DestroyAllObjects()
    {
        DestroyAllStones();
        DestroyAllCoins();
        DestroyAllBonuses();
    }
}