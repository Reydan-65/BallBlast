using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private Wallet wallet;
    [SerializeField] private Turret turret;

    [Header("Price List")]
    [SerializeField] private int startPricePerOne_FireRate;
    [SerializeField] private int startPricePerOne_Projectile;
    [SerializeField] private int startPricePerOne_Damage;
    
    [Header("Prices Texts")]
    [SerializeField] private TextMeshProUGUI pricePerOne_FireRate_Text;
    [SerializeField] private TextMeshProUGUI pricePerOne_Projectile_Text;
    [SerializeField] private TextMeshProUGUI pricePerOne_Damage_Text;

    [Header("Turret Stats Text")]
    [SerializeField] private TextMeshProUGUI fireRate_Text;
    [SerializeField] private TextMeshProUGUI projectile_Text;
    [SerializeField] private TextMeshProUGUI damage_Text;

    [Header("Spent Coin")]
    [SerializeField] private GameObject spentAmountCoin_GameObject;
    [SerializeField] private TextMeshProUGUI spentAmountCoin_Text;

    [Header("Count Selected Texts")]
    [SerializeField] private GameObject countSelected_FireRate_GameObject;
    [SerializeField] private GameObject countSelected_Projectile_GameObject;
    [SerializeField] private GameObject countSelected_Damage_GameObject;
    [SerializeField] private TextMeshProUGUI countSelected_FireRate_Text;
    [SerializeField] private TextMeshProUGUI countSelected_Projectile_Text;
    [SerializeField] private TextMeshProUGUI countSelected_Damage_Text;

    [Header("Turret Upgrade")]
    [SerializeField] private float fireRateStep;
    [SerializeField] private float projectileStep;
    [SerializeField] private float damageStep;

    [Header("Turren Max Values")]
    [SerializeField] private float max_FireRate;
    [SerializeField] private float max_Projectile;
    [SerializeField] private float max_Damage;
    
    [Header("Interactable Buttons")]
    [SerializeField] Button fireRate_Button;
    [SerializeField] Button projectile_Button;
    [SerializeField] Button damage_Button;
    [SerializeField] Button apply_Button;
    [SerializeField] Button reset_Button;

    [Space(10)]
    private int pricePerOne_FireRate;
    private int pricePerOne_Projectile;
    private int pricePerOne_Damage;

    [Space(10)]
    private int thisTimeBuyedCount_FireRate;
    private int thisTimeBuyedCount_Projectile;
    private int thisTimeBuyedCount_Damage;

    [Space(10)]
    private int buyedCount_FireRate;
    private int buyedCount_Projectile;
    private int buyedCount_Damage;

    [Space(10)]
    private int tempBuyedCount_FireRate;
    private int tempBuyedCount_Projectile;
    private int tempBuyedCount_Damage;

    [Space(10)]
    private int tempPricePerOne_FireRate;
    private int tempPricePerOne_Projectile;
    private int tempPricePerOne_Damage;

    [Space(10)]
    private float temp_FireRate;
    private float temp_Projectile;
    private float temp_Damage;

    [Space(10)]
    private int oneHandredPercent = 100;

    private bool declined = false;

    private void Start()
    {
        Cursor.visible = true;

        // Запомнить начальные параметры при входе в магазин
        pricePerOne_FireRate = startPricePerOne_FireRate;
        pricePerOne_Projectile = startPricePerOne_Projectile;
        pricePerOne_Damage = startPricePerOne_Damage;

        tempPricePerOne_FireRate = pricePerOne_FireRate;
        tempPricePerOne_Projectile = pricePerOne_Projectile;
        tempPricePerOne_Damage = pricePerOne_Damage;

        SaveShopParametrs();

        temp_FireRate = turret.StartFireRate;
        temp_Projectile = turret.StartProjectileAmount;
        temp_Damage = turret.StartDamage;
    }

    // Сохранение измененных параметров

    private void Update()
    {
        // Цены товаров
        pricePerOne_FireRate_Text.text = pricePerOne_FireRate.ToString();
        pricePerOne_Projectile_Text.text = pricePerOne_Projectile.ToString();
        pricePerOne_Damage_Text.text = pricePerOne_Damage.ToString();

        // Описание товара
        if (buyedCount_FireRate == tempBuyedCount_FireRate)
            fireRate_Text.text = ("Fire rate = " + (oneHandredPercent + (temp_FireRate + (-fireRateStep * buyedCount_FireRate)) * 2 * buyedCount_FireRate) + "%").ToString();
        else fireRate_Text.text = ("Fire rate = " + (oneHandredPercent + (-fireRateStep * oneHandredPercent * buyedCount_FireRate)) + "%").ToString();

        if (buyedCount_Projectile == tempBuyedCount_Projectile)
            projectile_Text.text = ("Projectile    = " + turret.ProjectileAmount).ToString();
        else projectile_Text.text = ("Projectile    = " + projectileStep * (buyedCount_Projectile + 1)).ToString();

        if (buyedCount_Damage == tempBuyedCount_Damage)
            damage_Text.text = ("Damage       = " + turret.Damage).ToString();
        else damage_Text.text = ("Damage       = " + damageStep * (buyedCount_Damage + 1)).ToString();

        // Цвет кнопки при нажатии
        if (wallet.AmountCoin >= pricePerOne_FireRate && fireRate_Button.interactable == true)
            ButtonPressedColor(fireRate_Button, fireRate_Button.colors, Color.green);
        else ButtonPressedColor(fireRate_Button, fireRate_Button.colors, Color.red);

        if (wallet.AmountCoin >= pricePerOne_Projectile && projectile_Button.interactable == true)
            ButtonPressedColor(projectile_Button, projectile_Button.colors, Color.green);
        else ButtonPressedColor(projectile_Button, projectile_Button.colors, Color.red);

        if (wallet.AmountCoin >= pricePerOne_Damage && damage_Button.interactable == true)
            ButtonPressedColor(damage_Button, damage_Button.colors, Color.green);
        else ButtonPressedColor(damage_Button, damage_Button.colors, Color.red);

        // Отключение кнопки при максимальном значении параметра
        ButtonDisabler(turret.FireRate, max_FireRate, fireRate_Button, pricePerOne_FireRate_Text);
        ButtonDisabler(turret.ProjectileAmount, max_Projectile, projectile_Button, pricePerOne_Projectile_Text);
        ButtonDisabler(turret.Damage, max_Damage, damage_Button, pricePerOne_Damage_Text);

        // Затраты
        if (wallet.SpentAmountCoin >= 1000) spentAmountCoin_Text.text = ("- " + wallet.SpentAmountCoin / 1000 + "K").ToString();
        else spentAmountCoin_Text.text = ("- " + wallet.SpentAmountCoin).ToString();

        // Выбранные товары
        countSelected_FireRate_Text.text = ("+ " + thisTimeBuyedCount_FireRate + "%").ToString();
        countSelected_Projectile_Text.text = ("+ " + thisTimeBuyedCount_Projectile).ToString();
        countSelected_Damage_Text.text = ("+ " + thisTimeBuyedCount_Damage).ToString();
    }

    // Покупка параметров
    public void Buy_Item(ref float item, ref float itemStep, ref int thisTimeBuyed, ref int buyedCount,
                         ref int pricePerOne, GameObject countSelected, GameObject spentAmountCoin)
    {
        if (wallet.AmountCoin >= pricePerOne)
        {
            wallet.DrawCoin(pricePerOne);
            thisTimeBuyed++;
            buyedCount++;
            pricePerOne += buyedCount;
            turret.Add_Item(ref item, ref itemStep);
            countSelected.SetActive(true);
            spentAmountCoin.SetActive(true);
            declined = false;

            apply_Button.interactable = true;
            reset_Button.interactable = true;
        }
        else return;
    }

    // Метод для кнопки
    public void Buy_AddFireRate()
    {
        Buy_Item(ref turret.fireRate, ref fireRateStep, ref thisTimeBuyedCount_FireRate, ref buyedCount_FireRate,
                 ref pricePerOne_FireRate, countSelected_FireRate_GameObject, spentAmountCoin_GameObject);
    }

    public void Buy_AddProjectile()
    {
        Buy_Item(ref turret.projectileAmount, ref projectileStep, ref thisTimeBuyedCount_Projectile, ref buyedCount_Projectile,
                 ref pricePerOne_Projectile, countSelected_Projectile_GameObject, spentAmountCoin_GameObject);
    }

    public void Buy_AddDamage()
    {
        Buy_Item(ref turret.damage, ref damageStep, ref thisTimeBuyedCount_Damage, ref buyedCount_Damage,
                 ref pricePerOne_Damage, countSelected_Damage_GameObject, spentAmountCoin_GameObject);
    }

    // Подтверждение покупки (кнопка Принять)
    public void ConfirmPurshase()
    {
        wallet.SpentAmountCoin = 0;

        SaveShopParametrs();

        SetPurshaseFlags();
    }

    // Проверка подтверждения покупки, при выходе из магазина (кнопка Выход)
    public void Check_Declined()
    {
        Cursor.visible = false;

        if (declined == false) ConfirmPurshase();
        else DeclinePurshase();
    }

    private void SaveShopParametrs()
    {
        tempPricePerOne_FireRate = pricePerOne_FireRate;
        tempPricePerOne_Projectile = pricePerOne_Projectile;
        tempPricePerOne_Damage = pricePerOne_Damage;

        tempBuyedCount_FireRate = buyedCount_FireRate;
        tempBuyedCount_Projectile = buyedCount_Projectile;
        tempBuyedCount_Damage = buyedCount_Damage;

        temp_FireRate = turret.FireRate;
        temp_Projectile = turret.ProjectileAmount;
        temp_Damage = turret.Damage;
    }

    // Отмена покупки (кнопка Резет)
    public void DeclinePurshase()
    {
        if (declined == false)
        {
            // Возврат количества монет
            wallet.AddCoins(wallet.SpentAmountCoin);
            wallet.SpentAmountCoin = 0;

            // Возврат количества приобретённых товаров
            buyedCount_FireRate = tempBuyedCount_FireRate;
            buyedCount_Projectile = tempBuyedCount_Projectile;
            buyedCount_Damage = tempBuyedCount_Damage;

            // Возврат цены приобретённых товаров
            pricePerOne_FireRate = tempPricePerOne_FireRate;
            pricePerOne_Projectile = tempPricePerOne_Projectile;
            pricePerOne_Damage = tempPricePerOne_Damage;

            // Возврат параметров туррели
            turret.FireRate = temp_FireRate;
            turret.ProjectileAmount = temp_Projectile;
            turret.Damage = temp_Damage;

            temp_FireRate = turret.FireRate;
            temp_Projectile = turret.ProjectileAmount;
            temp_Damage = turret.Damage;

            SetPurshaseFlags();

            declined = true;
        }
    }

    private void SetPurshaseFlags()
    {
        thisTimeBuyedCount_FireRate = 0;
        thisTimeBuyedCount_Projectile = 0;
        thisTimeBuyedCount_Damage = 0;

        countSelected_FireRate_GameObject.SetActive(false);
        countSelected_Projectile_GameObject.SetActive(false);
        countSelected_Damage_GameObject.SetActive(false);

        apply_Button.interactable = false;
        reset_Button.interactable = false;
        spentAmountCoin_GameObject.SetActive(false);
    }

    // Отключение кнопки при максимальном значении параметра
    private void ButtonDisabler(float currentStat, float maxStat, Button button, TextMeshProUGUI text)
    {
        if (currentStat < maxStat)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
            text.text = "Max";
        }
    }

    // Цвет кнопки при нажатии на неё
    private void ButtonPressedColor(Button button, ColorBlock colors, Color color)
    {
        colors.pressedColor = color;
        button.colors = colors;
    }

    // сброс (кнопка Новая игра)
    public void Reset_Shop()
    {
        Cursor.visible = false;

        pricePerOne_FireRate = startPricePerOne_FireRate;
        pricePerOne_Projectile = startPricePerOne_Projectile;
        pricePerOne_Damage = startPricePerOne_Damage;

        tempPricePerOne_FireRate = pricePerOne_FireRate;
        tempPricePerOne_Projectile = pricePerOne_Projectile;
        tempPricePerOne_Damage = pricePerOne_Damage;

        buyedCount_FireRate = 0;
        buyedCount_Projectile = 0;
        buyedCount_Damage = 0;

        temp_FireRate = turret.StartFireRate;
        temp_Projectile = turret.StartProjectileAmount;
        temp_Damage = turret.StartDamage;

        tempBuyedCount_FireRate = 0;
        tempBuyedCount_Projectile = 0;
        tempBuyedCount_Damage = 0;
    }
}