using UnityEngine;

public class ColorGenerator : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private SpriteRenderer[] spriteRendererToChangeColor;

    private void Start()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRendererToChangeColor)
        {
            Color color = spriteRenderer.color;

            if (color != null)
            {
                int colorIndex = Random.Range(0, colors.Length);
                spriteRenderer.color = colors[colorIndex];
            }
        }
    }
}