using UnityEngine;

public class BuildPlace : MonoBehaviour, ISelectable {

    private Tower _PlacedTower;

    [SerializeField] GameObject m_SelectIndicator;

    public Tower tower => _PlacedTower;

    public void Select() {
        SelectionHandler.TrySelectObject(this);
    }

    public void OnSelected() {
        m_SelectIndicator.SetActive(true);
    }

    public void OnDeselected() {
        m_SelectIndicator.SetActive(false);
    }

    public void Build(Tower buildedTower) {
        _PlacedTower= buildedTower;
    }
}