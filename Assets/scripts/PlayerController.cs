using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InventoryManager inventory;
    public ProtectionClass protectionClass;
    public ConsumableClass consumableClass;
    public WeaponClass weaponClass;
    public MiscClass miscClass;


    public UnityEngine.UI.Image heathBar;

    public Text healthCountTxt;
    public Text pritectionTxt;


    public float maxHealth = 100;
    public float currentHelth;
    public float pritection = 0;
    public float bar = 1;

    private void Start()
    {
        currentHelth = maxHealth;
        pritection = protectionClass.protection;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
           if(inventory.selectedItem != null)
            {
                inventory.selectedItem.Use(this);
            }
        }
        healthCountTxt.text = currentHelth.ToString();
        heathBar.rectTransform.anchorMax = new Vector3(bar, 0.5f);

    }



    public void GetDamage(int damage)
    {
        currentHelth -= damage / pritection;

        for (int i = 0; i < bar; i++)
        {
            {
                bar = bar - 1f/currentHelth;

                heathBar.rectTransform.anchorMax = new Vector3(bar, 0.5f);
            }
            healthCountTxt.text = currentHelth.ToString();
            pritectionTxt.text = pritection.ToString();
        }
    }

    public void HealAdd()
    {
        currentHelth += consumableClass.helthAdded;

        bar = bar + 1 / currentHelth;
    }
    public void ProtectedAdd()
    {
        pritection += protectionClass.protection;

    }
    public void BulletToShot(int shot)
    {
        //miscClass.CountBullet;
    }
}
