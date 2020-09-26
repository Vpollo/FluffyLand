using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    private Shop _ShopScript;

    void Start()
    {
        _ShopScript = shop.GetComponent<Shop>();
        shop.active = false;
    }

    void Update()
    {
        
    }

    public void CallShop()
    {
        shop.active = true;
    }
}
