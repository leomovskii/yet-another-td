using UnityEngine;

[DisallowMultipleComponent]
public class CreepHealth : MonoBehaviour {

    private ICreepHealth _CreepDiedEventListener;
    private CreepHealthData _CreepHealthData;

    [SerializeField] HealthBar m_HealthBar;

    public float value => _CreepHealthData.healthCurrent;

    public void Initialize(CreepHealthData healthData, ICreepHealth listener) {
        _CreepHealthData = healthData;
        _CreepDiedEventListener = listener;
    }

    public void Heal(float amount) {
        OnHealthChanged(amount);
    }

    public void Damage(float amount) {
        OnHealthChanged(-amount);
    }

    public void Restore() {
        OnHealthChanged(_CreepHealthData.healthMaximum);
    }

    public void Kill() {
        OnHealthChanged(-_CreepHealthData.healthMaximum);
    }

    private void OnHealthChanged(float amount) {
        _CreepHealthData.healthCurrent = Mathf.Clamp(_CreepHealthData.healthCurrent + amount, 0, _CreepHealthData.healthMaximum);

        m_HealthBar?.Set(_CreepHealthData.healthCurrent, _CreepHealthData.healthMaximum);

        if (_CreepHealthData.healthCurrent == 0)
            _CreepDiedEventListener?.OnCreepDied();
    }
}