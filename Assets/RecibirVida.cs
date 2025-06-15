using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RecibirVida : MonoBehaviour
{
    // Start is called before the first frame update
    public bool RequirePlayerTag = true;
    public string TargetInventoryName = "MainInventory";
    public int HealthBonus;
    private BoxCollider2D BoxCollider2D;
    void Start()
    {
        GetComponent<BoxCollider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string playerID = "Player1";
        if (RequirePlayerTag && (!collision.CompareTag("Player")))
        {
            Health characterHealth = TargetInventory(playerID).Owner.GetComponent<Health>();
            if (characterHealth != null)
            {
                characterHealth.ReceiveHealth(HealthBonus, TargetInventory(playerID).gameObject);
                Debug.Log("llamando al Player");
            }
        }
        else
        {
        }
    }

    protected Inventory _targetInventory = null;
    public virtual Inventory TargetInventory(string playerID)
    {
        if (TargetInventoryName == null)
        {
            return null;
        }
        _targetInventory = Inventory.FindInventory(TargetInventoryName, playerID);
        return _targetInventory;
    }
}

