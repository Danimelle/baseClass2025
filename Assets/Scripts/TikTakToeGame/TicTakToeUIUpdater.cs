using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TicTakToeUIUpdater : MonoBehaviour
{
    [SerializeField] GameObject _winStateObject;
    private TMP_Text _stateText;


    private void Start()
    {
        _stateText = _winStateObject.GetComponent<TMP_Text>();
        _winStateObject.SetActive(false);
    }



    public void UpdateWinStateText(string state, string player = null)
    {
        _winStateObject.SetActive(true);
        switch (state)
        {
            case "Win":
                _stateText.text = PlayerWinText(player);
                break;
            case "Tie":
                _stateText.text = "it's a tie";
                break;
        }
    }

    private string PlayerWinText(string player)
    {
        return $"player {player} won";
    }
}
