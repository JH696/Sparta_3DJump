using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChargeText : MonoBehaviour
{
    public TextMeshProUGUI chargeText;

    private void Update()
    {
        SetChargeText();
    }

    private void SetChargeText()
    {
        if (!CharacterManager.Instance.Player.controller.isCharging)
        {
            chargeText.gameObject.SetActive(false);
            return;
        }
        else
        {
            float chargerPower = CharacterManager.Instance.Player.controller.chargedPower;

            chargeText.gameObject.SetActive(true);
            chargeText.text = $"[ÃæÀü] {chargerPower * 50:N0} / 100%";
        }
    }
}
