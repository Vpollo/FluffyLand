using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private float _recoverBoost;

    void Start()
    {
        _capacity = 2;
        _recoverBoost = 0.5f;
    }

    void Update()
    {
        
    }

    public void upgradeCapacity()
    {

    }
}
