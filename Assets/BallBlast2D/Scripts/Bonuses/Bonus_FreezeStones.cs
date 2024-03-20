using UnityEngine;

public class Bonus_FreezeStones : Pickup
{
    [SerializeField] private GameObject freezingImpactPrefab;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        LevelState level = FindObjectOfType<LevelState>();

        if (level.IsDefeat == false && level.IsPassed == false)
        {
            base.OnTriggerEnter2D(other);

            if (other.transform.root.TryGetComponent(out Cart cart) == true)
            {
                Instantiate(freezingImpactPrefab);

                UI_GameHUD gameHUD = FindObjectOfType<UI_GameHUD>();
                Stone[] stones = FindObjectsOfType<Stone>();
                FreezingImpact[] impacts = FindObjectsOfType<FreezingImpact>();

                // активация визуального отображения времени действия бонуса
                gameHUD.transform.GetChild(0).GetChild(5).GetComponent<UI_BonusBar>().ResetBar();
                gameHUD.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);

                foreach (Stone stone in stones)
                {
                    // если компонент уже добавлен, удалить
                    if (stone.TryGetComponent(out Freeze freeze) == true)
                    {
                        for (int i = 0; i < impacts.Length; i++)
                        {
                            Destroy(impacts[impacts.Length - 1].gameObject);
                        }

                        freeze.gameObject.GetComponentInChildren<SpriteRenderer>().color = freeze.BaseColor;
                        Destroy(freeze);
                        stone.IsFreezed = false;
                    }

                    if (stone.IsFreezed == false)
                    {
                        stone.gameObject.AddComponent<Freeze>();
                        stone.IsFreezed = true;
                    }
                    else return;
                }
            }
        }
    }
}