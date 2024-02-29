using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image healthBarImage;

    private void Start()
    {
        // 'Image' bileþenini al
        healthBarImage = GetComponent<Image>();
    }

    // Saðlýk çubuðunu güncellemek için bu metod
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        // Saðlýk çubuðunun doluluk oranýný hesapla ve ayarla
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }
}
