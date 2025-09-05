using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TikTakToeManager : MonoBehaviour
{
    [SerializeField] TikTakToeRow[] tikTakToeRows;

    private bool isItXTurn = true;
    private string winner = "";

    public void OnSlotClicked(TicTakToeSlot slot)
    {
        if (slot.GetTextInSlot() != " " || winner != "") //if the slot isnt emptky and the winner is announced return
        { return; }

        string player = isItXTurn ? "X" : "O"; //if its xs turn the player is x and if not the player is o
        slot.ChangeTextsInSlot(player);

        if (CheckWin())
        {
            Debug.Log($"the winner is {player}");
        }

        if (CheckTie())
        {
            Debug.Log("It's a tie");
        }

        isItXTurn = !isItXTurn;
    }

    private bool CheckWin()
    {
        return CheckRows() || CheckDiagonal() || CheckColums();
    }

    private bool CheckTie()
    {
        if (winner != "") //if theres a winner return false
        {
            return false;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (GetSlotText(i, j) == " ") //check all slot text if there is still an empty slot then it's not a tie
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool CheckRows()
    {
        for (int i = 0; i < 3; i++)
        {
            string a = GetSlotText(i, 0);
            string b = GetSlotText(i, 1);
            string c = GetSlotText(i, 2);

            if (a == b && b == c && a != " ")
            {
                winner = a;

                return true;
            }
        }
        return false;
    }

    private bool CheckColums()
    {
        for (int i = 0; i < 3; i++)
        {
            string a = GetSlotText(0, i);
            string b = GetSlotText(1, i);
            string c = GetSlotText(2, i);

            if (a == b && b == c && a != " ")
            {
                winner = a;
                return true;
            }
        }
        return false;
    }

    private bool CheckDiagonal()
    {
        string middleSlot = GetSlotText(1, 1);
        if (GetSlotText(0, 0) == middleSlot && GetSlotText(2, 2) == middleSlot && middleSlot != " ")
        {
            winner = GetSlotText(0, 0);
            return true;
        }

        if (GetSlotText(0, 2) == middleSlot && middleSlot == GetSlotText(2, 0) && middleSlot != " ")
        {
            winner = GetSlotText(0, 2);
            return true;
        }

        return false;

    }

    private string GetSlotText(int row, int col)
    {
        return tikTakToeRows[row].rows[col].GetComponent<TicTakToeSlot>().GetTextInSlot();
    }




}
