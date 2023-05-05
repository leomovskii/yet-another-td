using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoneyInGame : MonoBehaviour {

    private int _Money;

    [SerializeField] private Text m_MoneyDisplayText;

    public int moneyValue {
        get => _Money;
        set {
            _Money = value;
            m_MoneyDisplayText.text = $"{value}";
            UpdateMoneyUIs();
        }
    }

    private void Start() {
        UpdateMoneyUIs();
    }

    private void UpdateMoneyUIs() {
        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IMoneyAmountChange>().ToList();
        if (listeners.Count > 0)
            foreach (var item in listeners)
                item?.OnMoneyAmountChanged(_Money);
    }
}