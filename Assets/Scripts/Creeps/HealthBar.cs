using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image m_Bar;
    [SerializeField] Gradient m_Gradient;

    public void Set(float healthCurrent, float healthMaximum) {
        float ratio = healthCurrent / healthMaximum;
        m_Bar.fillAmount = ratio;
        m_Bar.color = m_Gradient.Evaluate(ratio);
    }
}