using UnityEngine;

public class DropedItemMovement : ObjectMovement
{
    [Header("Slow Settings")]
    [SerializeField] private float slowReboundSpeedStep;
    [SerializeField] private float slowHorizontalSpeedStep;
    [SerializeField] private float slowRotateSpeedStep;

    public float ReboundSpeed {  get => reboundSpeed; set => reboundSpeed = value; }
    public float HorizontalSpeed {  get => horizontalSpeed; set => horizontalSpeed = value; }

    protected override void Move()
    {
        base.Move();

        horizontalSpeed -= slowHorizontalSpeedStep * Time.deltaTime;
        reboundSpeed -= slowReboundSpeedStep * Time.deltaTime;
        rotateSpeed -= slowRotateSpeedStep * Time.deltaTime;

        if (horizontalSpeed <= 0) horizontalSpeed = 0;
        if (rotateSpeed <= 0) rotateSpeed = 0;
        if (reboundSpeed <= 0) { enabled = false; objectStoped = true; }
    }
}