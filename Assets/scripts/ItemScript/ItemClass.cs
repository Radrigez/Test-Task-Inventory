using UnityEngine;

public class ItemClass : ScriptableObject 
{
    [Header("Item")]

    public string itemName;
    public Sprite icon;
    public bool isStacble = true;
    public bool isProtection;
    public bool isShot;

    public virtual void Use(PlayerController Caller)
    {
       // Debug.Log(Caller);
        Debug.Log("Use: Item" + itemName);

    }
    public virtual void Use(EnemyController Caller)
    {
        // Debug.Log(Caller);
        Debug.Log("Use: Item" + itemName);

    }
    public void Use()
    {
        Debug.Log("Использован предмет: " + itemName);
        // Здесь можно добавить логику использования предмета
    }
    public virtual MiscClass GetMisc() { return null; }
    public virtual ItemClass GetItem() { return this; }
    public virtual WeaponClass GetWeapon() { return null; }
    public virtual ProtectionClass GetProtection() { return null; }
    public virtual ConsumableClass GetConsumable() { return null; }
}
