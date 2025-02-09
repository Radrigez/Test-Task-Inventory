using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Item/Pritaction")]

public class ProtectionClass : ItemClass 
{
    [Header("Protaction")]

    public int protection;

    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        Debug.Log("Consumable");
        caller.inventory.UseSelected();
        caller.ProtectedAdd();
    }
    public override void Use(EnemyController caller)
    {
        base.Use(caller);
        Debug.Log("Consumable");
        caller.inventory.UseSelected();
        caller.ProtectedAdd();
    }
    public ProtactionType protactionType;

    public enum ProtactionType
    {
        Head,
        Body
    }
    public override ProtectionClass GetProtection() { return this; }

}
