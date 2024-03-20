using UnityEngine;

public class CartAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Play_CartDestroyAnimation()
    {
        animator.SetTrigger("defeat");
    }

    public void Play_CartShootAnimation()
    {
        animator.SetTrigger("fire");
    }

    public void Play_CartIdleAnimation()
    {
        animator.SetTrigger("idle");
    }

    public void Reset_CartTriggersAnimation()
    {
        animator.ResetTrigger("defeat");
        animator.ResetTrigger("fire");
        animator.ResetTrigger("idle");
    }
}