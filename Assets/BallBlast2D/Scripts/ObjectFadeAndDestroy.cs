using UnityEngine;

public class ObjectFadeAndDestroy : MonoBehaviour
{
    [SerializeField] private DropedItemMovement dropMovement;

    [Header("Destroy Settings")]
    [SerializeField] private float timeToFade;
    [SerializeField] private float timeToDestroy;

    private float timer;

    private void Update()
    {
        if (dropMovement.objectStoped == true)
        {
            timer += Time.deltaTime;

            if (timer > timeToFade)
            {
                if (gameObject.TryGetComponent(out Animator animator) == true)
                {
                    animator.SetTrigger("stopMovement");
                }

                Destroy(gameObject, timeToDestroy);
            }
        }
    }
}