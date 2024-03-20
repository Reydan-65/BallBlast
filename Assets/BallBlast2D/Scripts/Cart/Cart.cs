using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Cart : MonoBehaviour
{
    [SerializeField] private LevelState levelState;
    [SerializeField] private Stone stone;

    [SerializeField] private GameObject gameOver_HUD;
    [SerializeField] private float timerToGameOver;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float vehicleWidth;

    [Header("Wheels")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelRadius;

    [HideInInspector] public UnityEvent CollisionStone;

    private Vector3 movementTarget;

    private float deltaMovement;
    private float lastPositionX;

    private bool isInvuled;
    public bool IsInvuled { get => isInvuled; set => isInvuled = value; }

    private void Start()
    {
        movementTarget = transform.localPosition;
    }

    private void OnEnable()
    {
        levelState.Defeat.AddListener(OnDefeat);
    }

    private void OnDisable()
    {
        levelState.Defeat.RemoveListener(OnDefeat);
    }

    private void OnDefeat()
    {
        StartCoroutine(TimeCoroutine());
    }

    private IEnumerator TimeCoroutine()
    {
        yield return new WaitForSeconds(timerToGameOver);

        stone.StopMovement();
        gameOver_HUD.SetActive(true);
        Cursor.visible = true;
    }

    private void Update()
    {
        Move();
        RotateWheel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent(out Stone stone) == true && stone.TryGetComponent(out Freeze freeze) == false) CollisionStone.Invoke();
    }

    private void Move()
    {
        lastPositionX = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed * Time.deltaTime);
        deltaMovement = transform.position.x - lastPositionX;
    }

    private void RotateWheel()
    {
        float angle = (180 * deltaMovement) / (Mathf.PI * wheelRadius * 2);

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate(0, 0, -angle);
        }
    }

    public void SetMovementTarget(Vector3 target)
    {
        movementTarget = ClampMovementTarget(target);
    }

    private Vector3 ClampMovementTarget(Vector3 target)
    {
        float leftBorder = LevelBoundary.Instance.LeftBorder + vehicleWidth * 0.5f;
        float rightBorder = LevelBoundary.Instance.RightBorder - vehicleWidth * 0.5f;

        Vector3 movTarget = target;
        movTarget.z = transform.position.z;
        movTarget.y = transform.position.y;

        if (movTarget.x < leftBorder) movTarget.x = leftBorder;
        if (movTarget.x > rightBorder) movTarget.x = rightBorder;

        return movTarget;
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position - new Vector3(vehicleWidth * 0.5f, 0.5f, 0), transform.position + new Vector3(vehicleWidth * 0.5f, -0.5f, 0));
    }

#endif

}