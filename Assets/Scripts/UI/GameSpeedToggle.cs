using UnityEngine;
using UnityEngine.UI;

public class GameSpeedToggle : MonoBehaviour {

    private int _TimeScale;

    [SerializeField] Text m_DisplayText;

    private void Start() {
        Time.timeScale = _TimeScale;
        m_DisplayText.text = $"x{_TimeScale}";
    }

    public void OnSpeedChanged() {
        _TimeScale++;
        if (_TimeScale == 4)
            _TimeScale = 1;
        Time.timeScale = _TimeScale;
        m_DisplayText.text = $"x{_TimeScale}";
    }

    private void OnDestroy() {
        Time.timeScale = 1f;
    }
}