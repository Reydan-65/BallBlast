using UnityEngine;

public class DropAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Play_IdleAnimation()
    {
        animator.SetTrigger("idle");
    }

    public void Play_FadeAnimation()
    {
        animator.SetTrigger("stopMovement");
    }
}