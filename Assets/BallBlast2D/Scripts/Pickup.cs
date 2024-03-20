using UnityEngine;

public class Pickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.TryGetComponent(out Cart cart) == true) Destroy(gameObject);
    }
}