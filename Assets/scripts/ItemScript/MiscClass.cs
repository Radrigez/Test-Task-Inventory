using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName ="Item/Misc")]

public class MiscClass : ItemClass
{
    public float CountBullet;
    public override void Use(PlayerController caller)
    { 
        base.Use(caller);
        Debug.Log("Shot");
    }
    public override MiscClass GetMisc() {  return this; }
}
