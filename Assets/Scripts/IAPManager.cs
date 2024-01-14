using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;
using UnityEngine.Purchasing.Extension;
public class IAPManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string ID_5000;
    [SerializeField] private string ID_15000;
    [SerializeField] private string ID_50000;
    [SerializeField] private string ID_RemoveADS;
    [SerializeField] private string ID_GoldPack;

    public void OnPurchaseCompile(Product product)
    {
        Debug.Log(product.definition.id);
        if(product.definition.id == ID_5000)
        {
            print("receive 5000");
        }
        if (product.definition.id == ID_15000)
        {
            print("receive 15000");
        }
        if (product.definition.id == ID_50000)
        {
            print("receive 50000");
        }
        if (product.definition.id == ID_RemoveADS)
        {

        }
        if (product.definition.id == ID_GoldPack)
        {

        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log(product.definition.id +" failed as resuit of :"+failureDescription.ToString());
    }
}
