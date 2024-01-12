using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example HUD class for health, shield and endurance values.
/// </summary>
public class HUD : MonoBehaviour {

    [Header("References")]
    public Hero hero;
    public Slider slider_Endurance;
    public CanvasGroup canvasGroup_DamageFX;

    private void Awake () {
        if (hero) {

            slider_Endurance.maxValue = hero.movement.referenceEndurance;
        }
    }

    private void Update () {
        if (hero) {
        
            slider_Endurance.value = hero.movement.GetEnduranceValue();
        }
        canvasGroup_DamageFX.alpha = Mathf.MoveTowards (canvasGroup_DamageFX.alpha, 0, Time.deltaTime * 2);
    }
    
}
