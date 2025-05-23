
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
                Debug.LogWarning("알 수 없는 아이템 타입입니다.");
                break;
        }
    }

    private void Apple()
    {
        StartCoroutine(ItemMessage("\"즉시 체력을 30만큼 회복했습니다.\""));

        CharacterManager.Instance.Player.condition.Heal(30);
    }

    private void Banana()
    {
        StartCoroutine(ItemMessage("\"잠시 점프 충전 속도가 증가했습니다.\""));

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

        yield return new WaitForSeconds(sec);  // 0.25초 유지

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
