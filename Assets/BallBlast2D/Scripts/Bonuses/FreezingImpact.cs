using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FreezingImpact : MonoBehaviour
{
    [SerializeField] private float freezingTime;
    [SerializeField] private AudioSource freeze_Sound;

    [HideInInspector] public UnityEvent IsActive;

    public float FreezingTime => freezingTime;

    private void Awake()
    {
        // передать время действия эффекта
        UI_GameHUD gameHUD = FindObjectOfType<UI_GameHUD>();
        gameHUD.transform.GetChild(0).GetChild(5).GetComponent<UI_BonusBar>().BarTimer = freezingTime;

        IsActive.Invoke();
        freeze_Sound.Play();

        StartCoroutine(ChangeBackAfterDelay(freezingTime));
    }

    private IEnumerator ChangeBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        LevelState levelState = FindObjectOfType<LevelState>();

        if (levelState.IsDefeat == false && levelState.IsPassed == false)
        {
            Freeze[] freezes = FindObjectsOfType<Freeze>();

            foreach (Freeze freeze in freezes)
            {
                freeze.gameObject.GetComponentInChildren<SpriteRenderer>().color = freeze.BaseColor;
                freeze.gameObject.GetComponent<ObjectMovement>().enabled = true;
                freeze.gameObject.GetComponent<Stone>().IsFreezed = false;

                Destroy(freeze);
            }

            Destroy(gameObject);
        }
    }
}