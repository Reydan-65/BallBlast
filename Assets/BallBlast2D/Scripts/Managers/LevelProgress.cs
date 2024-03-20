using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioSource backgroundMusic;

    [Header("HUD Objects")]
    [SerializeField] private UI_GameHUD gameHUD;
    [SerializeField] private UI_ProgressBar progressBar;
    [SerializeField] private GameObject pause;
    [SerializeField] private UI_Shop shop;
    [SerializeField] private Button continue_Button;

    [Header("Game Objects")]
    [SerializeField] private ObjectDestroyer objectDestroyer;
    [SerializeField] private Cart cart;
    [SerializeField] private Turret turret;
    [SerializeField] private Wallet wallet;
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private LevelState levelState;

    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;

    private void Start()
    {
        continue_Button.interactable = false;
        levelState.IsPassed = false;
        levelState.IsDefeat = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) == true) PauseGame();

#if UNITY_EDITOR

        if (Input.GetKeyUp(KeyCode.F1) == true) Reset();
        if (Input.GetKeyUp(KeyCode.F2) == true) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

#endif

    }

    // Загрузка параметров следующего уровня
    public void Load_NextLevel()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        currentLevel++;

        levelState.Load_LevelStateParametrs();
        wallet.Reset_SpentAmountCoin();
        progressBar.Reset_ProgressBar();
        objectDestroyer.DestroyAllObjects();
        stoneSpawner.Load_SpawnerParametrs();
        gameHUD.Reset_GameHUDParametrs();
    }

    // Выход в главное меню
    public void InMainMenu()
    {
        Cursor.visible = true;
        levelState.IsStart = false;
    }

    // Загрузка параметров текущего уровня
    public void Reset_CurrentLevel()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;

        levelState.Reset_CurrentLevelStateParametrs();
        objectDestroyer.DestroyAllObjects();
        progressBar.Reset_ProgressBar();
        stoneSpawner.Reset_CurrentStoneSpawnerParametrs();
        wallet.Reset_SpentAmountCoin();
        gameHUD.Reset_GameHUDParametrs();
    }

    // Пауза
    public void PauseGame()
    {
        if (levelState.IsStart == true)
        {
            if (levelState.IsDefeat == false && levelState.IsPassed == false)
            {
                if (Time.timeScale > 0f)
                {
                    Cursor.visible = true;
                    backgroundMusic.Pause();
                    cart.GetComponent<CartInputControl>().enabled = false;
                    Time.timeScale = 0f;
                    pause.SetActive(true);
                }
                else
                {
                    Cursor.visible = false;
                    backgroundMusic.UnPause();

                    cart.GetComponent<CartInputControl>().enabled = true;
                    Time.timeScale = 1f;
                    pause.SetActive(false);
                }
            }
        }
    }

    // Начало новой игры - сброс прогресса
    public void Load_NewGame()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;

        continue_Button.interactable = true;
        currentLevel = 1;

        levelState.Reset_LevelState();
        turret.Reset_Turret();
        wallet.Reset_Wallet();
        stoneSpawner.Reset_StoneSpawnerParametrs();
        progressBar.Reset_ProgressBar();
        shop.Reset_Shop();
        gameHUD.Reset_GameHUDParametrs();
    }

    // Проверка прохождения уровня
    public void Check_LevelPassed()
    {
        Cursor.visible = false;

        if (levelState.IsPassed == true) Load_NextLevel();
        else Reset_CurrentLevel();

        levelState.IsPassed = false;
        levelState.IsDefeat = false;
    }

#if UNITY_EDITOR

    // Сброс данных и перезапуск приложения (в инспекторе)
    private void Reset()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

#endif

    // Сброс данных при закрытии приложения
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}