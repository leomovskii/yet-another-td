using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour {

    static SelectionHandler _Instance;
    private ISelectable _SelectedObject;

    private void Awake() {
        if (_Instance == null)
            _Instance = this;
        else if (_Instance != this)
            Destroy(this);
    }

    public static void TrySelectObject(ISelectable selectableObject) {
        if (_Instance._SelectedObject == null) {
            _Instance._SelectedObject = selectableObject;
            _Instance._SelectedObject.OnSelected();

        } else if (_Instance._SelectedObject == selectableObject) {
            _Instance._SelectedObject.OnDeselected();
            _Instance._SelectedObject = null;

        } else {
            _Instance._SelectedObject.OnDeselected();
            _Instance._SelectedObject = selectableObject;
            _Instance._SelectedObject.OnSelected();
        }
    }

    public static void TryDeselect() {
        if (_Instance._SelectedObject != null) {
            _Instance._SelectedObject.OnDeselected();
            _Instance._SelectedObject = null;
        }
    }
}