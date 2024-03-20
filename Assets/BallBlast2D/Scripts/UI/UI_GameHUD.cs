using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameHUD : MonoBehaviour
{
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private LevelState levelState;
    [SerializeField] private LevelProgress levelProgress;

    [Header("Coins Settings")]
    [SerializeField] private Wallet wallet;
    [SerializeField] private TextMeshProUGUI coinCountText;

    [Header("Stones Settings")]
    [SerializeField] private Image stoneLeftBar;

    [SerializeField] private TextMeshProUGUI currentLevelText;

    [SerializeField] private UI_ProgressBar progressBar;

    private float stoneLeftBarStep;
    private float stoneLeft;
    public Image StoneLeftBar { get => stoneLeftBar; set => stoneLeftBar = value; }
    public float StoneLeftBarStep { get => stoneLeftBarStep; set => stoneLeftBarStep = value; }
    public float StoneLeft { get => stoneLeft; set => stoneLeft = value; }

    private void Start()
    {
        stoneLeftBar.fillAmount = 1;
        Cursor.visible = true;
    }

    private void Awake()
    {
        wallet.Change_CoinAmount.AddListener(OnChange_CoinAmount);
        stoneSpawner.Complete.AddListener(OnSpawnComplete);
    }

    private void OnDestroy()
    {
        wallet.Change_CoinAmount.RemoveListener(OnChange_CoinAmount);
        stoneSpawner.Complete.RemoveListener(OnSpawnComplete);
    }

    private void OnSpawnComplete()
    {
        Stone[] stones = FindObjectsOfType<Stone>();

        foreach (Stone stone in stones)
        {
            if (stone.Size == Size.Small) stoneLeft += 1;
            if (stone.Size == Size.Medium) stoneLeft += 3;
            if (stone.Size == Size.Large) stoneLeft += 7;
            if (stone.Size == Size.Huge) stoneLeft += 15;
        }

        stoneLeftBarStep = 1 / stoneLeft;
    }

    // Отрисовка Количества Монет
    private void OnChange_CoinAmount()
    {
        int coins = wallet.GetAmountCoin();

        if (coins >= 1000) coinCountText.text = "=  " + coins / 1000 + "K";
        else coinCountText.text = ("=  " + wallet.GetAmountCoin()).ToString();
    }

    // Отрисовка Прогресса Уровня
    private void Update()
    {
        if (levelProgress.CurrentLevel >= 1000) currentLevelText.text = "Level " + levelProgress.CurrentLevel / 1000 + "K";
        else currentLevelText.text = "Level " + levelProgress.CurrentLevel.ToString();
    }

    // Сброс параметров ХУДА
    public void Reset_GameHUDParametrs()
    {
        transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(5).gameObject.SetActive(false);

        stoneLeftBarStep = 0;
        stoneLeft = 0;
        stoneLeftBar.fillAmount = 1;
        progressBar.BarImage.fillAmount = 1;
        progressBar.IsStoped = false;
    }
}