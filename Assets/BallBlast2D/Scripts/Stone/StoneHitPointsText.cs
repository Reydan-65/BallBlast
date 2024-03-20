using UnityEngine;
using TMPro;

[RequireComponent(typeof(Destructble))]
public class StoneHitPointsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hitPointsText;

    private Destructble destructble;

    private void Awake()
    {
        destructble = GetComponent<Destructble>();

        destructble.Change_HitPoints.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        destructble.Change_HitPoints.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        int hitPoints = destructble.GetHitPoint();

        if (hitPoints >= 1000) hitPointsText.text = hitPoints / 1000 + "K";
        else hitPointsText.text = hitPoints.ToString();
    }
}