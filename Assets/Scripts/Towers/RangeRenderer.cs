using UnityEngine;

public class RangeRenderer : MonoBehaviour {

    enum PlaneType {
        XY, XZ, YZ
    }

    [SerializeField] LineRenderer m_Line;
    [SerializeField] PlaneType m_Plane = PlaneType.XY;
    [Space(10)]
    [SerializeField][Min(8)] int m_Segments = 360;
    [SerializeField] float m_Radius = 1;
    [SerializeField] float m_Width = 0.05f;

    public float radius {
        get => m_Radius;
        set => m_Radius = value < 0 ? 0 : value;
    }

    private void Awake() {
        if (m_Line == null)
            m_Line = (LineRenderer) gameObject.AddComponent<LineRenderer>();
        m_Line.useWorldSpace = false;
    }

    private void Update() {
        DrawCircle();
    }

    private void DrawCircle() {
        m_Line.startWidth = m_Width;
        m_Line.endWidth = m_Width;
        m_Line.positionCount = m_Segments + 1;

        var points = new Vector3[m_Segments + 1];
        for (int i = 0; i < points.Length; i++) {
            var rad = Mathf.Deg2Rad * (i * 360f / m_Segments);
            float a = Mathf.Sin(rad) * m_Radius;
            float b = Mathf.Cos(rad) * m_Radius;

            if (m_Plane == PlaneType.XY)
                points[i] = new Vector3(Mathf.Sin(rad) * m_Radius, Mathf.Cos(rad) * m_Radius, 0);
            else if (m_Plane == PlaneType.XZ)
                points[i] = new Vector3(Mathf.Sin(rad) * m_Radius, 0, Mathf.Cos(rad) * m_Radius);
            else
                points[i] = new Vector3(0, Mathf.Sin(rad) * m_Radius, Mathf.Cos(rad) * m_Radius);
        }
        m_Line.SetPositions(points);
    }

    private void OnEnable() {
        m_Line.enabled = true;
    }

    private void OnDisable() {
        m_Line.enabled = false;
    }
}