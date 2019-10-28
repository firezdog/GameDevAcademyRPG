using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayUnit : MonoBehaviour
{
    [SerializeField] Slider healthSlider, manaSlider;
    [SerializeField] Image unitPicture;
    [SerializeField] TextMeshProUGUI healthText;

    internal void Activate(UnitStats activePlayer)
    {
        int health = activePlayer.Health;
        int totalHealth = activePlayer.TotalHealth;
        gameObject.SetActive(true);
        unitPicture.sprite = activePlayer.Portrait;
        healthSlider.value = (float) health / totalHealth;
        healthText.text = $"Health: {health} / {totalHealth}";
    }

    internal void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
