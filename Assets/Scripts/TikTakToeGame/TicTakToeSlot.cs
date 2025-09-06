using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TicTakToeSlot : MonoBehaviour
{
    private TMP_Text slotText;

    private void Start()
    {
        slotText = GetComponentInChildren<TMP_Text>();
        slotText.text = " ";
    }

    public string GetTextInSlot()
    {
        return slotText.text;
    }

    public void ChangeTextsInSlot(string player)
    {
        if (slotText.text == " ")
        {
            slotText.text = player;
        }
    }

}
