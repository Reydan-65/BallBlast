using UnityEngine;
using System.Collections;

public class CoinImpact : MonoBehaviour
{
    [SerializeField] private float coinTime;
    [SerializeField] private AudioSource coin_Sound;

    private void Awake()
    {
        coin_Sound.Play();

        StartCoroutine(ChangeBackAfterDelay(coinTime));
    }

    private IEnumerator ChangeBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}