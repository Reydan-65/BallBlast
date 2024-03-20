using UnityEngine;

public enum Size
{
    Small, Medium, Large, Huge
}

[RequireComponent(typeof(ObjectMovement))]
public class Stone : Destructble
{
    [SerializeField] private Crack crackPrefab;
    [SerializeField] private CrackImpact crackImpact;
    [SerializeField] private SpriteRenderer stoneRender;
    [SerializeField] private Size size;
    [SerializeField] private float spawnUpForce;
    [SerializeField] private int stoneAmountSpawned;
    [SerializeField] private AudioSource groundCollision;

    public AudioSource GroundCollision { get => groundCollision; set => groundCollision = value; }
    private ObjectMovement movement;

    private bool isFreezed = false;
    public bool IsFreezed { get => isFreezed; set => isFreezed = value; }
    public Size Size => size;

    private void Awake()
    {
        movement = GetComponent<ObjectMovement>();

        Die.AddListener(OnStoneDestroyed);
        movement.StoneCollission.AddListener(OnStoneCollission);

        SetSize(size);
    }

    private void OnDestroy()
    {
        Die.RemoveListener(OnStoneDestroyed);
        movement.StoneCollission.RemoveListener(OnStoneCollission);
    }

    // Создание трещин
    private void OnStoneCollission()
    {
        Crack crack = Instantiate(crackPrefab);
        crack.Init(new Vector3(stoneRender.transform.position.x, transform.position.y, stoneRender.transform.position.z));
        Instantiate(crackImpact);
    }

    public void OnStoneDestroyed()
    {
        if (size != Size.Small)
        {
            SpawnStones();
        }

        Destroy(gameObject);
    }

    private void SpawnStones()
    {
        for (int i = 0; i < stoneAmountSpawned; i++)
        {
            Turret turret = FindObjectOfType<Turret>();
            Stone stone = Instantiate(this, transform.position, Quaternion.identity);

            // для отображения текста здоровья в разных плоскостях, смещение камней по оси Z
            for (int j = 0; j < stoneAmountSpawned; j++)
                stone.transform.position = new Vector3(stone.transform.position.x, stone.transform.position.y, stone.transform.position.z + j * 0.1f);

            stone.SetSize(size - 1);
            stone.maxHitPoints = Mathf.Clamp(maxHitPoints / 2, 1, maxHitPoints);
            stone.movement.AddVerticalVelocity(spawnUpForce);
            stone.movement.SetHorizontalDirection((i % 2 * stoneAmountSpawned) - 1);

            // включить движение, если уничтоженный камень был заморожен
            if (stone.TryGetComponent(out Freeze freeze) == true)
            {
                Destroy(freeze);
                stone.movement.enabled = true;
            }
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0) return;

        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    public Vector3 GetVectorFromSize(Size size)
    {
        if (size == Size.Huge) return new Vector3(1, 1, 1);
        if (size == Size.Large) return new Vector3(0.75f, 0.75f, 0.75f);
        if (size == Size.Medium) return new Vector3(0.6f, 0.6f, 0.6f);
        if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

        return Vector3.one;
    }

    public void StopMovement()
    {
        Stone[] stones = FindObjectsOfType<Stone>();

        foreach (Stone stone in stones)
        {
            stone.GetComponent<ObjectMovement>().enabled = false;
        }
    }
}