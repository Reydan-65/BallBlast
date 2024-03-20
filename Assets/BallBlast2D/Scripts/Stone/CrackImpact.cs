using UnityEngine;
using System.Collections;

public class CrackImpact : MonoBehaviour
{
    [SerializeField] private float crackTime;

    private void Awake()
    {
        StartCoroutine(ChangeBackAfterDelay(crackTime));
    }

    private IEnumerator ChangeBackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Crack[] crack = FindObjectsOfType<Crack>();

        if (crack != null)
        {
            Destroy(crack[crack.Length - 1].gameObject);
        }

        Destroy(gameObject);
    }
}