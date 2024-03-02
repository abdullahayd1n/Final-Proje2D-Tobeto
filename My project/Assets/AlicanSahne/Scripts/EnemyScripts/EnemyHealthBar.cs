using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image healthBarImage;

    private void Start()
    {
        // 'Image' bile�enini al
        healthBarImage = GetComponent<Image>();
    }

    // Sa�l�k �ubu�unu g�ncellemek i�in bu metod
    public void UpdateHealthBar(float enemyHealth, float health)
    {
        // Sa�l�k �ubu�unun doluluk oran�n� hesapla ve ayarla
        healthBarImage.fillAmount = enemyHealth / health;
    }
}
