using UnityEngine;

public class Waypoint : MonoBehaviour {

    [SerializeField] Waypoint[] m_Next = new Waypoint[0];

    public bool TryPickNextWaypoint(int index, out Waypoint next) {
        if (m_Next.Length == 0) {
            next = null;
            return false;
        }
        next = m_Next[index >= m_Next.Length ? m_Next.Length - 1 : index];
        return true;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
        if (m_Next.Length > 0)
            foreach (var waypoint in m_Next)
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
    }
}