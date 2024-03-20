using UnityEngine;
using UnityEngine.Events;

public class ObjectMovement : MonoBehaviour
{
    [Header("Gravity Settings")]
    [SerializeField] protected float gravity;
    [SerializeField] protected float gravityOffset;

    [Header("Rebound Speed Settings")]
    [SerializeField] protected float reboundSpeed;

    [Header("Horizontal Speed Settings")]
    [SerializeField] protected float horizontalSpeed;

    [Header("Rotation Settings")]
    [SerializeField] protected float rotateSpeed;

    [HideInInspector] public bool objectStoped = false;

    private bool UseGravity;
    protected Vector3 velocity;

    [HideInInspector] public UnityEvent StoneCollission;

    public Vector3 Velocity => velocity;

    private void Awake()
    {
        velocity.x = -Mathf.Sign(transform.position.x) * horizontalSpeed;
    }

    private void Update()
    {
        TryEnableGravity();
        Move();
    }

    private void TryEnableGravity()
    {
        if (Mathf.Abs(transform.position.x) <= Mathf.Abs(LevelBoundary.Instance.LeftBorder) - gravityOffset)
        {
            UseGravity = true;
        }
    }

    protected virtual void Move()
    {
        if (UseGravity == true)
        {
            velocity.y -= gravity * Time.deltaTime;
            transform.Rotate(0, 0, -Mathf.Sign(velocity.x) * rotateSpeed * Time.deltaTime);
        }

        velocity.x = Mathf.Sign(velocity.x) * horizontalSpeed;

        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LevelEdge levelEdge) == true)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                if(gameObject.TryGetComponent(out Stone stone) == true)
                {
                    stone.GroundCollision.Play();
                }

                velocity.y = reboundSpeed;
                StoneCollission.Invoke();
            }

            if (levelEdge.Type == EdgeType.Left && velocity.x < 0 ||
                levelEdge.Type == EdgeType.Right && velocity.x > 0)
            {
                velocity.x *= -1;
            }
        }
    }

    public void AddVerticalVelocity(float velocity)
    {
        this.velocity.y += velocity;
    }

    public void SetHorizontalDirection(float direction)
    {
        velocity.x = Mathf.Sign(direction) * horizontalSpeed;
    }
}