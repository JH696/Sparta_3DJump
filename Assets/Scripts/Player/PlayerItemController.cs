
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class PlayerItemController : MonoBehaviour
{
    public UIInventory uIInventory;
    public TextMeshProUGUI itemMessage;

    public void UseItemSlot_1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && uIInventory.slots[0].item != null)
        {
            ItemType type = uIInventory.slots[0].item.type;

            UseItem(type);

            uIInventory.slots[0].item = null;
            uIInventory.UpdateUI(); ;
        }
    }

    public void UseItemSlot_2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && uIInventory.slots[1].item != null)
        {
            ItemType type = uIInventory.slots[1].item.type;

            UseItem(type);

            uIInventory.slots[1].item = null;
            uIInventory.UpdateUI();
        }
    }

    public void UseItemSlot_3(InputAction.CallbackContext context)
    {
            if (context.phase == InputActionPhase.Started && uIInventory.slots[2].item != null)
        {
            ItemType type = uIInventory.slots[2].item.type;

            UseItem(type);
            uIInventory.slots[2].item = null;
            uIInventory.UpdateUI();
        }
    }

    private void UseItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Apple:
                Apple();
                break;
            case ItemType.Banana:
                Banana();
                break;
            case ItemType.CupCake:
                CupCake();
                break;
            default:
                Debug.LogWarning("�� �� ���� ������ Ÿ���Դϴ�.");
                break;
        }
    }

    private void Apple()
    {
        StartCoroutine(ItemMessage("\"��� ü���� 30��ŭ ȸ���߽��ϴ�.\""));

        CharacterManager.Instance.Player.condition.Heal(30);
    }

    private void Banana()
    {
        StartCoroutine(ItemMessage("\"��� ���� ���� �ӵ��� �����߽��ϴ�.\""));

        StartCoroutine(FastCharge(5));
    }

    private void CupCake()
    {
        StartCoroutine(ItemMessage("\"Sugar Rush ?\""));

        CharacterManager.Instance.Player.controller.JumpHigher(600);
    }

    IEnumerator FastCharge(float sec)
    {
        CharacterManager.Instance.Player.controller.chargingSpeed += 0.5f;

        yield return new WaitForSeconds(sec);  // 0.25�� ����

        CharacterManager.Instance.Player.controller.chargingSpeed -= 0.5f;
    }

    IEnumerator ItemMessage(string message)
    {

        Debug.Log(message);
        itemMessage.text = message;
        itemMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        itemMessage.gameObject.SetActive(false);
    }
}
