using UnityEngine;

public class Tower : MonoBehaviour {

    private Creep _ActualTarget;
    private float _AttackTimer;

    [SerializeField] Transform aimPoint;
    [SerializeField] Missle misslePrefab;
    [Space(10)]
    [SerializeField] bool canAttack = true;
    [SerializeField] TowerTargetingMode targetingMode;
    [SerializeField] int countTargets = 1;
    [Space(10)]
    [SerializeField] float attackRange = 2;
    [SerializeField] float attackDelay = 1;
    [SerializeField] float attackDamage = 1;
    [Space(10)]
    [SerializeField] RangeRenderer rangeRenderer;

    private void Update() {
        rangeRenderer.radius = attackRange;
        if (canAttack && _AttackTimer <= 0) {
            ConfirmTargetOrSelectNext();
            TryFire(_ActualTarget);
        }
    }

    public void ConfirmTargetOrSelectNext() {
        if (_ActualTarget == null || Vector3.Distance(transform.position, _ActualTarget.transform.position) > attackRange)
            if (Targeting.TryPickCreepInRange(transform.position, attackRange, targetingMode, out Creep creep))
                _ActualTarget = creep;
    }

    public void TryFire(Creep target) {
        if (target != null) {
            transform.right = target.transform.position - transform.position;
            var missle = Instantiate(misslePrefab, aimPoint.position, Quaternion.identity);
            missle.Fire(target, attackDamage);
            _AttackTimer = attackDelay;
        }
    }

    private void FixedUpdate() {
        if (canAttack && _AttackTimer > 0)
            _AttackTimer -= Time.fixedDeltaTime;
    }
}