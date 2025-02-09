using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InventoryManager : MonoBehaviour  
{
    [SerializeField] private GameObject itemCursor;
    [SerializeField] private GameObject slotHolder;
    
    [SerializeField] private GameObject slotHolderWeapon;
    [SerializeField] private GameObject slotHolderArmor;

    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] private GameObject hotBarSelector;

    [SerializeField] SlotClass[] startingItem;
    
    private GameObject[] armorSlots;
    private GameObject[] weaponSlots;

    private GameObject[] slots;

    private SlotClass[] items;

    private SlotClass[] armorItems;
    private SlotClass[] weaponItems;

    private SlotClass tempSlot;
    private SlotClass movingSlot;
    private SlotClass originalSlot;

    [SerializeField] private int selectedSlotIndex = 0;

    public ItemClass selectedItem;
    public GameObject[] selectedItemWeapon;

    public GameObject popapPanel;

    bool isMovingItem;


    private void Start()
    {
        
        //Inventory slots
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();
        }
        //armor slots
        armorSlots = new GameObject[slotHolderArmor.transform.childCount];
        armorItems = new SlotClass[armorSlots.Length];
        for (int i = 0; i < armorItems.Length; i++)
        {
            armorItems[i] = new SlotClass();
        }
        //weapon slots
        weaponSlots = new GameObject[slotHolderWeapon.transform.childCount];
        weaponItems = new SlotClass[weaponSlots.Length];
        //for (int i = 0; i < weaponSlots.Length; i++)
        //{
        //    weaponItems[i] = new SlotClass();
        //}

        for (int i = 0; i < weaponItems.Length; i++)
        {
            weaponSlots[i] = slotHolderWeapon.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < startingItem.Length; i++)
        {
            items[i] = startingItem[i];
    
        }
       

        for (int i = 0; i < slotHolder.transform.childCount; i++)
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

        //for (int i = 0; i < slotHolderWeapon.transform.childCount; i++)
        //    slots[i] = slotHolderWeapon.transform.GetChild(i).gameObject;

        for (int i = 0; i < slotHolderArmor.transform.childCount; i++)
            armorSlots[i] = slotHolderArmor.transform.GetChild(i).gameObject;


        RefreshUI();

        Add(itemToAdd, 1, 1, 1);
        Remove(itemToRemove);
    }
    private void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;

        if (isMovingItem)
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().icon;

        if (Input.GetMouseButtonDown(0))
        {
            if (isMovingItem)
            {
                EndItemMove();
            }
            else
                BeginItemMove();
        }
        if (Input.GetMouseButtonDown(1))
        {

        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if(isMovingItem)
        //    {
        //        EndItemMove_Singled();
        //    }
        //    else
        //        BeginClosedMove_Half();
        //}


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex + 1, 0, weaponSlots.Length - 1);
            Debug.Log(selectedSlotIndex);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex - 1, 0, weaponSlots.Length - 1);
        }

        hotBarSelector.transform.position = weaponSlots[selectedSlotIndex].transform.position;
        selectedItem = items[selectedSlotIndex + (weaponSlots.Length / 3)].GetItem();
    }
   
    
    public void UseSelected()
    {
        items[selectedSlotIndex + (weaponSlots.Length / 3)].SubQuantity(1);
        RefreshUI();    
    }
    public void UseBull()
    {
        items[2].SubQuantity(1);
        RefreshUI();
    }

    public void RefreshUI()
    {
        for(int i = 0;i < slots.Length;i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().icon;

                if (items[i].GetItem().isStacble)
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity() + "";
                else
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }

        RefreshArmorWeaponSlots();
    }
    public void RefreshArmorWeaponSlots()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            try
            {
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i + (weaponSlots.Length / 3)].GetItem().icon;

                if (items[i + (weaponSlots.Length / 3)].GetItem().isShot)
                    weaponSlots[i].transform.GetChild(1).GetComponent<Text>().text = items[i + (weaponSlots.Length / 3)] + " ";

                else
                    weaponSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                weaponSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
        for (int i = 0; i < armorSlots.Length; i++)
        {
            try
            {
                armorSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                armorSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = armorItems[i].GetItem().icon;


                if (armorItems[i].GetItem().isProtection)
                    armorSlots[i].transform.GetChild(1).GetComponent<Text>().text = armorItems[i].GetProtection() + "";

                else
                    armorSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                armorSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                armorSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                armorSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }

        }
    }
    public bool Add(ItemClass item, int quantity, int protection, int damage)
    {
        SlotClass slot = Contains(item);

        if (slot != null && slot.GetItem().isStacble)
        {
            slot.AddQuantity(quantity);
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity, protection, damage);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    }
    public bool Remove(ItemClass item)
    {
        SlotClass temp = Contains(item);

        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
                temp.SubQuantity(1);
            else
            {
                int slotToRemoveIndex = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }
    public SlotClass Contains(ItemClass item)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item)
                return items[i];
        }
        //for (int i = 0; i < armorItems.Length; i++)
        //{
        //    if (armorItems[i].GetItem() == item)
        //        return armorItems[i];
        //}
        //for (int i = 0; i < weaponItems.Length; i++)
        //{
        //    if (weaponItems[i].GetItem() == item)
        //        return weaponItems[i];
        //}
        return null;
    }



    #region Inventory Stuff
    private bool BeginItemMove()
    {
        OpnenPoap();
        originalSlot = GetClossedSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false;

        movingSlot = new SlotClass(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private bool BeginClosedMove_Half()
    {
        originalSlot = GetClossedSlot();
        if(originalSlot == null || originalSlot.GetItem() == null)
            return false;

        movingSlot = new SlotClass(originalSlot.GetItem(), Mathf.CeilToInt(originalSlot.GetQuantity() / 2f), 0, 0);
        originalSlot.SubQuantity(Mathf.CeilToInt(originalSlot.GetQuantity() / 2f));
        if (originalSlot.GetQuantity() == 0)
            originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private void OpnenPoap()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
            {
                if (items[i].GetItem().isProtection)
                {
                    Debug.Log(items[i].GetItem().isProtection.ToString());

                    //    if(popapPanel.transform.childCount != 0)
                    //    {
                    //       // popapPanel.SetActive(true);

                    //        slots[i] = popapPanel.transform.GetChild(0).gameObject;

                    //        try
                    //        {
                    //            popapPanel.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    //            popapPanel.transform.GetChild(0).GetComponent<Image>().sprite = armorItems[i].GetItem().icon;
                    //        }
                    //        catch
                    //        {
                    //            popapPanel.transform.GetChild(0).GetComponent<Image>().sprite = null;
                    //            popapPanel.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    //        }
                    //    }
                    //}

                }
                //else
                //{
                //    popapPanel.SetActive(false);
                //    RefreshUI();
            }

        }
    }
    //public void SlotArmor()
    //{
    //    for (int i = 0; i < armorItems.Length; i++)
    //    {
    //        slots[i] = slotHolder.transform.GetChild(i).gameObject;

    //        //try
    //        //{
    //        //    armorSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
    //        //    armorSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = armorItems[i].GetItem().icon;


    //        //    if (armorItems[i].GetItem().isProtection)
    //        //        armorSlots[i].transform.GetChild(1).GetComponent<Text>().text = armorItems[i].GetProtection() + "";

    //        //    else
    //        //        armorSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
    //        //}
    //        //catch
    //        //{
    //        //    armorSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
    //        //    armorSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
    //        //    armorSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
    //        //}
    //    }
    //    popapPanel.SetActive(false);
    //    RefreshUI();
    //}
    private bool EndItemMove()
    {
        originalSlot = GetClossedSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity(), movingSlot.GetProtection(), movingSlot.GetDamage());
            movingSlot.Clear();
        }
        else
        {
            if(originalSlot.GetItem() != null)
            {
                if(originalSlot.GetItem() == movingSlot.GetItem())
                {
                    if(originalSlot.GetItem().isStacble)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        originalSlot.CountDamage(movingSlot.GetDamage());
                        movingSlot.Clear();
                    }
                    else
                        return false;
                }
                else
                {
                    tempSlot = new SlotClass(originalSlot);
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity(),movingSlot.GetProtection(), movingSlot.GetDamage());
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity(), tempSlot.GetProtection(), tempSlot.GetDamage());
                    RefreshUI();    
                    return true;
                }
            }
            else
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity(), movingSlot.GetProtection(), movingSlot.GetDamage());
                movingSlot.Clear();
            }
        }

        isMovingItem = false;
        RefreshUI();
        return true;
    }
    private bool EndItemMove_Singled()
    {
        originalSlot = GetClossedSlot();
        if(originalSlot == null || movingSlot.GetItem().isStacble == false)
            return false;
        if(originalSlot.GetItem() != null && originalSlot.GetItem() != movingSlot.GetItem())
            return false;

        movingSlot.SubQuantity(1);
        if (originalSlot.GetItem() != null && originalSlot.GetItem() == movingSlot.GetItem())
            originalSlot.AddQuantity(1);
        else
            originalSlot.AddItem(movingSlot.GetItem(), 1, 1, movingSlot.GetDamage());

        if(movingSlot.GetQuantity() < 1)
        {
            isMovingItem = false;
            movingSlot.Clear();
        }
        else
            isMovingItem = true;

        RefreshUI();
        return true;
    }
    private SlotClass GetClossedSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
                return items[i];
        }

        //for (int i = 0; i < armorSlots.Length; i++)
        //{
        //    if (Vector2.Distance(armorSlots[i].transform.position, Input.mousePosition) <= 32)
        //        return armorItems[i];
        //}
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (Vector2.Distance(weaponSlots[i].transform.position, Input.mousePosition) <= 32)
                return weaponItems[i];
        }
        return null;
    }
    #endregion
}
