using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform slotPanel;
    public TextMeshProUGUI message;

    private PlayerController controller;
    private PlayerCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;

        CharacterManager.Instance.Player.addItem += AddItem;
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].Inventory = this;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        ItemSlot Slot = GetEmptySlot(data);

        if (Slot != null)
        {
            Slot.item = data;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        message.text = "인벤토리에 자리가 없습니다.";
        CharacterManager.Instance.Player.itemData = null;
    }

    ItemSlot GetEmptySlot(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public ItemType ItemScan(int index)
    {
        ItemType type = slots[index].item.type;
        return type;
    }
}
