using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private Cart cart;
    [SerializeField] private Stone stone;

    [SerializeField] private LevelProgress levelProgress;
    [SerializeField] private UI_ProgressBar progressBar;

    [SerializeField] private float startLevelTime;

    [HideInInspector] public float levelTime;

    private bool isStart;

    [Space(10)]
    public UnityEvent Passed;
    private bool isPassed = false;

    [Space(10)]
    public UnityEvent Defeat;
    private bool isDefeat = false;

    private float timerToLevelEnd;
    private float timer;

    private bool checkPassed;

    public float LevelTime { get => levelTime; set => levelTime = value; }
    public bool IsStart { get => isStart; set => isStart = value; }
    public bool IsPassed { get => isPassed; set => isPassed = value; }
    public bool IsDefeat { get => isDefeat; set => isDefeat = value; }

    private void Start()
    {
        levelTime = startLevelTime;
    }

    private void Awake()
    {
        stoneSpawner.Complete.AddListener(OnSpawnCompleted);
        cart.CollisionStone.AddListener(OnCartCollisionStone);
    }

    private void OnDestroy()
    {
        stoneSpawner.Complete.RemoveListener(OnSpawnCompleted);
        cart.CollisionStone.RemoveListener(OnCartCollisionStone);
    }

    private void OnCartCollisionStone()
    {
        if (cart.IsInvuled == false && isDefeat == false)
        {
            isDefeat = true;
            stoneSpawner.enabled = false;
            Defeat.Invoke();
        }
    }

    private void OnSpawnCompleted()
    {
        checkPassed = true;
    }

    // ѕроверка завершени€ уровн€ или поражени€
    private void Update()
    {
        if (isStart == true)
        {
            if (isDefeat == false && isPassed == false)
            {
                timerToLevelEnd += Time.deltaTime;
                timer += Time.deltaTime;

                if (timer >= 0.5f)
                {
                    if (checkPassed == true)
                    {
                        if (FindObjectsOfType<Stone>().Length == 0)
                        {
                            isPassed = true;
                            Passed.Invoke();
                            checkPassed = false;

                            Cursor.visible = true;
                        }

                        timer = 0f;
                    }
                }

                if (timerToLevelEnd >= levelTime)
                {
                    isDefeat = true;
                    Defeat.Invoke();
                    stoneSpawner.enabled = false;
                    stone.StopMovement();
                    checkPassed = false;
                }
            }
        }
    }

    private void Reset_Flags()
    {
        timerToLevelEnd = 0;

        isStart = true;
        isPassed = false;
        isDefeat = false;
        checkPassed = false;
    }

    // «агрузка параметров состо€ни€ уровн€
    public void Load_LevelStateParametrs()
    {
        levelTime += levelProgress.CurrentLevel + stoneSpawner.Amount;
        progressBar.BarTimer = levelTime;
        Reset_Flags();
    }

    public void Reset_CurrentLevelStateParametrs()
    {
        Reset_Flags();
    }

    public void Reset_LevelState()
    {
        levelTime = startLevelTime;
        Reset_Flags();
    }
}