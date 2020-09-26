using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private float _incStep;
    [SerializeField] private float _money = 0;
    [SerializeField] private Text _moneyUI;

    void Start()
    {
        StartCoroutine(addMoney());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool subMoney(int amount)
    {
        if (amount > _money)
        {
            return false;
        }
        _money -= amount;
        _moneyUI.text = "Money: " + _money;
        return true;
    }

    IEnumerator addMoney()
    {
        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            _money += 200;
            _moneyUI.text = "Money: " + _money;
            yield return new WaitForSeconds(_incStep);
        }
    }
}
