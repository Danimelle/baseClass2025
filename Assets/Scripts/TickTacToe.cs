using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TickTacToe : MonoBehaviour
{
    private string[,] tickTacToeBoard = new string[3, 3];

    // void Start()
    // {
    //     PopulateBoard();
    //     Debug.Log(PrintBoard());
    //     //DisplayCharacters();
    // }

    private void PopulateBoard()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                tickTacToeBoard[row, col] = (row + col) % 2 == 0 ? "X" : "O";
            }
        }
    }

    private string PrintBoard()
    {
        string board = "";
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                board += tickTacToeBoard[row, col] + " ";
            }
            board += "\n";
        }
        return board;
    }

    //public string inputString = "hello, world";

    // void DisplayCharacters()
    // {
    //     foreach (char item in inputString)
    //     {
    //         Debug.Log(item);
    //     }
    // }



    //ADDITIONAL FROM CLASS

    [SerializeField] Button[] row = new Button[3];


    Button[,] board;

    private void Start()
    {
        int rowIndex = 0;
        int columIndex = 0;

        for (int i = 0; i < row.Length; i++)
        {
            columIndex++;
            if (columIndex == 2)
            {
                columIndex = 0;
                rowIndex++;
            }
            board[rowIndex, columIndex] = row[i];
        }
    }



}
