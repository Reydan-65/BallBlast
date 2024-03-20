using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private LevelState levelState;

    [Header("Spawn Settings")]
    [SerializeField] private Stone stonePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate;

    [Header("Balance Settings")]
    [SerializeField] private Turret turret;
    [SerializeField] private int startAmount;
    [SerializeField][Range(0.0f, 1.0f)] private float stoneMinHitPointsPercentage;
    [SerializeField] private float startMaxHitPointsRate;

    [Space(10)]
    public UnityEvent Complete;

    private float maxHitPointsRate;

    private bool stopSpawner = false;

    private float timer;

    private int amount;
    private int amountSpawned;
    private int stoneMinHitPoints;
    private int stoneMaxHitPoints;

    public int AmountSpawned { get => amountSpawned; set => amountSpawned = value; }
    public int Amount { get => amount; set => amount = value; }
    public bool StopSpawner { get => stopSpawner; set => stopSpawner = value; }

    private void Start()
    {
        amount = startAmount;
        maxHitPointsRate = startMaxHitPointsRate;

        StoneState();
    }

    private void Update()
    {
        if (levelState.IsStart == true)
        {
            if (stopSpawner == false)
            {
                timer += Time.deltaTime;

                if (timer > spawnRate)
                {
                    Spawn();
                    timer = 0;
                }

                if (amountSpawned >= amount)
                {
                    enabled = false;
                    Complete.Invoke();
                }
            }
        }
    }

    // Спавн камней по заданным параметрам
    private void Spawn()
    {
        for (int i = 0; i < 1; i++)
        {
            Stone stone = Instantiate(stonePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

            stone.SetSize((Size)Random.Range(1, 4));
            stone.maxHitPoints = Random.Range(stoneMinHitPoints, stoneMaxHitPoints + 1);

            // для отображения текста здоровья в разных плоскостях, смещение камней по оси Z
            for (int j = 0; j < amountSpawned; j++)
                stone.transform.position = new Vector3(stone.transform.position.x, stone.transform.position.y, stone.transform.position.z + j * 0.1f);

            amountSpawned++;
        }
    }

    private void StoneState()
    {
        int damagePerSecond = (int)((turret.Damage * turret.ProjectileAmount) * (1 / turret.FireRate));

        stoneMaxHitPoints = (int)(damagePerSecond * maxHitPointsRate) + amount;
        stoneMinHitPoints = Mathf.CeilToInt(stoneMaxHitPoints * stoneMinHitPointsPercentage);

        if (stoneMinHitPoints < 1) stoneMinHitPoints = 1;

        timer = spawnRate;
    }

    // Загрузка параметров спавна
    public void Load_SpawnerParametrs()
    {
        amount++;
        amountSpawned = 0;
        StoneState();
        enabled = true;
    }

    public void Reset_CurrentStoneSpawnerParametrs()
    {
        amountSpawned = 0;
        StoneState();
        enabled = true;
    }

    public void Reset_StoneSpawnerParametrs()
    {
        amount = startAmount;
        amountSpawned = 0;

        
        maxHitPointsRate = startMaxHitPointsRate;
        StoneState();
        enabled = true;
    }
}