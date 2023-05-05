using UnityEngine;

[DisallowMultipleComponent]
public class Missle : MonoBehaviour {

    private Creep _Target;
    private float _Damage;

    [SerializeField] float m_ReactionDistance = 0.1f;
    [SerializeField] float m_Speed = 1f;

    public void Fire(Creep target, float damage) {
        _Target = target;
        _Damage = damage;
    }

    private void FixedUpdate() {
        if (_Target == null) {
            Destroy(gameObject);
            return;
        }

        if (Vector3.Distance(transform.position, _Target.transform.position) <= m_ReactionDistance) {
            _Target.health.Damage(_Damage);
            Destroy(gameObject);
            return;
        }

        transform.right = _Target.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _Target.transform.position, m_Speed * Time.fixedDeltaTime);
    }
}