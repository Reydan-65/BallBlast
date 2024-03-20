using UnityEngine;

public class Crack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer crackSprite;
    private float offset;

    private Transform stoneScale;

    // Трещина
    public void Init(Vector3 position)
    {
        stoneScale = gameObject.GetComponent<Transform>();
        offset = (gameObject.GetComponentInChildren<SpriteRenderer>().size.y - stoneScale.localScale.y)/ (2 + stoneScale.localScale.y) ;

        transform.root.position = position + new Vector3(0, -offset , 0);
        transform.root.eulerAngles = new Vector3(0, Random.Range(-35f, 36f), 0);

        transform.root.localScale = stoneScale.root.localScale;
    }
}