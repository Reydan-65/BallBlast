using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [SerializeField] private LevelState levelState;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private CartAnimator animator;

    [Header("Base Parametrs")]
    [SerializeField] private float startFireRate;
    [SerializeField] private float startProjectileAmount;
    [SerializeField] private float startDamage;

    [Space(10)]
    public float fireRate;
    public float projectileAmount;
    public float damage;

    [Space(10)]
    [SerializeField] private float projectileInterval;

    public float StartFireRate => startFireRate;
    public float StartProjectileAmount => startProjectileAmount;
    public float StartDamage => startDamage;
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ProjectileAmount { get => projectileAmount; set => projectileAmount = value; }
    public float Damage { get => damage; set => damage = value; }

    [Space(10)]
    public UnityEvent StartFire;

    private float timer;

    [SerializeField] private AudioSource shoot_Sound;

    private Color targetColor;
    public Color TargetColor { get => targetColor; set => targetColor = value; }

    private void Start()
    {
        fireRate = startFireRate;
        projectileAmount = startProjectileAmount;
        damage = startDamage;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    // Снаряд
    private void SpawnProjectile()
    {
        float startPosX = shootPoint.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, new Vector3(startPosX + i * projectileInterval, shootPoint.position.y, shootPoint.position.z), transform.rotation);

            projectile.SetDamage(damage);
        }
    }

    // Выстрел снарядом
    public void Fire()
    {
        if (levelState.IsDefeat == false && levelState.IsPassed == false)
        {
            if (timer > fireRate)
            {
                StartFire.Invoke();
                animator.Play_CartShootAnimation();
                SpawnProjectile();
                timer = 0;
                shoot_Sound.Play();
            }
        }
    }

    // Увеличение параметров при покупке в магазине
    public void Add_Item(ref float item, ref float value)
    {
        item += value;
    }

    // Текущее значение параметров
    public float GetCurrent_FireRate()
    {
        return fireRate;
    }

    public float GetCurrent_ProjectileAmount()
    {
        return projectileAmount;
    }

    public float GetCurrent_Damage()
    {
        return damage;
    }

    // сброс параметров
    public void Reset_Turret()
    {
        fireRate = startFireRate;
        projectileAmount = startProjectileAmount;
        damage = startDamage;
    }
}