using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName ="Item/Consumable")]

public class ConsumableClass : ItemClass
{
    [Header("Consunable")]

    public float helthAdded;

    public override void Use(PlayerController caller)
    {
        base.Use(caller);
        Debug.Log("Eat Consumable");
        caller.inventory.UseSelected();
        caller.HealAdd();

    }

    public override ConsumableClass GetConsumable() { return this; }
}
