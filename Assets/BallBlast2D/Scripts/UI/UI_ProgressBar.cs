using UnityEngine.UI;
using UnityEngine;

public class UI_ProgressBar : UI_Bar
{
    [SerializeField] private UI_GameHUD gameHUD;
    [SerializeField] private LevelState levelState;

    public bool IsStoped { get => isStoped; set => isStoped = value; }
    public Image BarImage { get => barImage; set => barImage = value; }

    private void Awake()
    {
        Reset_ProgressBar();

        levelState.Passed.AddListener(OnBarStoped);
        levelState.Defeat.AddListener(OnBarStoped);
    }

    private void OnDestroy()
    {
        levelState.Passed.RemoveListener(OnBarStoped);
        levelState.Defeat.RemoveListener(OnBarStoped);
    }

    private void OnBarStoped()
    {
        isStoped = true;
        Cursor.visible = true;
    }

    private void Update()
    {
        Bar_FullToEmpty(ref isStoped, ref barImage, ref barTimer);
    }

    protected override void Bar_FullToEmpty(ref bool stoped, ref Image bar, ref float barTimer)
    {
        base.Bar_FullToEmpty(ref stoped, ref bar, ref barTimer);

        if (isStoped == false) barImage.fillAmount -= Time.deltaTime / barTimer;
    }

    public void Reset_ProgressBar()
    {
        ResetBar();
        BarTimer = levelState.levelTime;
    }
}