using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [Space(10)]
    [Header("Spawn Drop Settings")]
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private int extraCoins;

    [Header("Value for spawn")]
    [SerializeField] private int spawnValue;

    [Header("Value Generator")]
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;

    [Space(10)]
    [Header("Spawn Rare Drop Settings")]
    [SerializeField] private GameObject[] rareDropPrefab;

    [Header("Value for Rare Drop")]
    [SerializeField] private int spawnRareValue;

    [Header("Value Rare Drop Generator")]
    [SerializeField] private int minRareValue;
    [SerializeField] private int maxRareValue;

    [Space(10)]
    [SerializeField] private float spawnUpForce;

    private Color freezedBaseColor;

    private int correctValue;

    private void Awake()
    {
        GetComponent<Stone>().Die.AddListener(GetFreezedBaseColor);
        GetComponent<Stone>().Die.AddListener(SetOldCoins);
        GetComponent<Stone>().Die.AddListener(SpawnDrop);
        GetComponent<Stone>().Die.AddListener(SpawnRareDrop);
    }

    // отметка уже созданных монет
    private void SetOldCoins()
    {
        Coin[] coins = FindObjectsOfType<Coin>();

        foreach (Coin coin in coins)
        {
            coin.Old = true;
        }
    }

    private void GetFreezedBaseColor()
    {
        if (TryGetComponent(out Freeze freeze) == true)
            freezedBaseColor = gameObject.GetComponent<Freeze>().BaseColor;
    }

    // ƒроп: шанс выподени€ одной монеты, либо,если камень желтый или был желтым до заморозки, extraCoins монет с шансом в 100%.
    private void SpawnDrop()
    {
        correctValue = Random.Range(minValue, maxValue);

        if (GetComponentInChildren<SpriteRenderer>().color == new Color(1f, 1f, 0f, 1f) || freezedBaseColor == new Color(1f, 1f, 0f, 1f))
        {
            for (int i = 0; i < extraCoins; i++)
            {
                Instantiate(dropPrefab, transform.position, Quaternion.identity);

                Coin[] coins = FindObjectsOfType<Coin>();

                for (int j = 0; j < coins.Length; j++)
                {
                    if (coins[j].TryGetComponent(out DropedItemMovement movement) == true)
                    {
                        // в рассыпную
                        if (coins[j].Old == false)
                        {
                            if (movement.GetComponentInParent<Coin>() == true)
                            {
                                movement.SetHorizontalDirection((j % 2 * 2) - 1);

                                if (movement.objectStoped == false)
                                {
                                    float range = Random.Range(-1f, 1.1f);

                                    movement.AddVerticalVelocity(range);
                                    movement.HorizontalSpeed += range;
                                    movement.ReboundSpeed += range;
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (correctValue == spawnValue)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
    }

    // –едкий дроп: либо заморозка, либо неу€звимость тележки
    private void SpawnRareDrop()
    {
        correctValue = Random.Range(minRareValue, maxRareValue);

        if (correctValue == spawnRareValue)
        {
            int bonusIndex = Random.Range(0, rareDropPrefab.Length);

            Instantiate(rareDropPrefab[bonusIndex], transform.position, Quaternion.identity);
        }
    }
}