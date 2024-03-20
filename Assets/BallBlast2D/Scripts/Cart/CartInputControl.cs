using UnityEngine;

public class CartInputControl : MonoBehaviour
{
    [SerializeField] private Cart cart;
    [SerializeField] private Turret turret;

    private void Update()
    {
        cart.SetMovementTarget( Camera.main.ScreenToWorldPoint(Input.mousePosition) );
        if (Input.GetMouseButtonDown(0) == true)
        {
            turret.Fire();
        }
    }
}