using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Cart cart;

    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private int damage;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent(out Destructble destructble) == true && destructble != cart)
        {
            destructble.ApplyDamage(damage);

            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = (int)damage;
    }
}