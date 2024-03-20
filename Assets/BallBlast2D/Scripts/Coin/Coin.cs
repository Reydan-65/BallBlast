using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] private GameObject impactEffect;

    private bool old = false;
    public bool Old { get => old; set => old = value; }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.transform.root.TryGetComponent(out Wallet wallet) == true)
        {
            wallet.AddCoins(1);

            Instantiate(impactEffect);
        }
    }
}