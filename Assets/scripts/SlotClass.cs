using UnityEngine;
[System.Serializable]
public class SlotClass 
{
    [SerializeField] private ItemClass item;
    [SerializeField] private WeaponClass weaponClass;
    [SerializeField] private ProtectionClass protectionClass;
    [SerializeField] private int quantity;
    [SerializeField] private int protection;
    [SerializeField] private int damage;


   
    public SlotClass()
    {
        item = null;
        quantity = 0;
        protection = 0;
    }
    //public SlotClass(ItemClass item)
    //{
    //    this.item.isProtection = item;
    //}
    public SlotClass(ItemClass _item, int _quantity, int _protcted, int _damage )
    {
        item = _item;
        quantity = _quantity;
        protection = _protcted;
        damage = _damage;
    }
 

    public SlotClass(SlotClass slot)
    {
        this.item = slot.GetItem();
        this.quantity = slot.GetQuantity();
        this.protection = slot.GetProtection();
        this.damage = slot.GetDamage();
    }

    ///
    public WeaponClass GetWeapon() { return weaponClass; }
    public ProtectionClass GetProtectionClass() { return protectionClass; }
    ///
    public ItemClass GetItem() {return item;}
    public int GetQuantity() {return quantity;}
    public int GetProtection() { return protection; }
    public int GetDamage() {return damage;}
    public void AddQuantity(int _quantity) { quantity += _quantity; }
    
    public void SubQuantity(int _quantity)
    {
        quantity -= _quantity;
        if(quantity <= 0)
        {
            Clear();
        }
    }
    public void CountPritection(int _protection) { protection = _protection; }
    public void CountDamage(int _damage) { damage = _damage; }

    public void AddItem(ItemClass item, int quantity, int protection , int damage)
    {
        this.item = item;
        this.quantity = quantity;
        this.protection = protection;
        this.damage = damage;
    }
    public void Clear()
    {
        this.item = null;
        this.quantity = 0;
    }
}
