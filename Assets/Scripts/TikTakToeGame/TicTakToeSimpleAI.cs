using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TicTakToeSimpleAI : MonoBehaviour
{
    //[SerializeField] TikTakToeController tikTakToeController;

    private string aiPlayer = "O";



    public void MakeMove(TicTakToeSlot[] slots)
    {
        //tikTakToeController.ToggleSimpleAi(false);

        TicTakToeSlot chosenSlot = slots
        .Where(slot => slot.GetTextInSlot() == " ")
        .OrderBy(_ => Random.value)
        .FirstOrDefault();

        if (chosenSlot != null)
        {
            StartCoroutine(DelayedMove(Random.Range(0.2f, 2), chosenSlot));
        }
    }

    private IEnumerator DelayedMove(float delay, TicTakToeSlot chosenSlot)
    {
        yield return new WaitForSeconds(delay);

        chosenSlot.ChangeTextsInSlot(aiPlayer);

        //tikTakToeController.AIPlay(chosenSlot, aiPlayer);

    }
}
