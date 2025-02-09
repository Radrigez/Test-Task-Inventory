using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName ="Item/Weapon")]

public class WeaponClass : ItemClass
{
    [Header("Weapon")]

    public int damage;
    public int Damage
    {
        get { damage = Damage; return damage; }
        set { damage = value; }
    }

    public WeaponType weaponType;

    public enum WeaponType
    {
        gun,
        machineGun
    }

    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        caller.inventory.UseBull();
        
        caller.GetDamage(damage);
    }
    public override void Use(EnemyController caller)
    {
        base.Use(caller);
        caller.inventory.UseBull();
        caller.GetDamage(damage);
    }

    public override WeaponClass GetWeapon() { return this; }
}
