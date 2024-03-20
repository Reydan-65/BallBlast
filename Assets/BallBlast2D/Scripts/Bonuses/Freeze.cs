using UnityEngine;

public class Freeze : MonoBehaviour
{
    private Color baseColor;
    public Color BaseColor => baseColor;

    private void Awake()
    {
        Color freezeColor = new Color(0, 0.95f, 1, 1);

        // ����������� ���������� ����� �����
        baseColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        
        // ��������� ����� �� ������������ � ���������
        gameObject.GetComponentInChildren<SpriteRenderer>().color = freezeColor;
        gameObject.GetComponentInChildren<ObjectMovement>().enabled = false;
    }
}