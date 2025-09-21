using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikTakToeController : MonoBehaviour
{
    public enum GameMode { TwoPlayer, SimpleAI, MiniMaxAi, AIvsAI } //game mode options
    [SerializeField] private GameMode _gameMode;

    private System.Action _aiTypeTurn;

    [Header("slots")]
    public TicTakToeSlot[] ticTakToeSlots;

    [Header("AIs")]
    [SerializeField] TicTakToeSimpleAI _simpleAI;
    [SerializeField] TicTakToeMinimaxAI _miniMaxAi;

    [Header("UI")]
    [SerializeField] TicTakToeUIUpdater _gameUI;


    private TicTakToeSlot[,] _ticTakToeBoard = new TicTakToeSlot[3, 3];

    private bool _isItXTurn = true;
    //private bool _isSimpleAiTurn = false;
    private string _winner = "";


    private void Start()
    {
        Create2DBoard();

        InitializeAiMode(_gameMode);

        if (_gameMode == GameMode.AIvsAI)
        {
            StartCoroutine(PlayAIvsAI());
        }
    }


    private void Create2DBoard() //move the slots from the 1d array to a 2d array
    {
        Debug.Log("allocating array to 2d array");
        int rowIndex = 0;
        int columIndex = 0;
        for (int i = 0; i < ticTakToeSlots.Length; i++)
        {
            _ticTakToeBoard[rowIndex, columIndex] = ticTakToeSlots[i];

            columIndex++;
            if (columIndex == 3)
            {
                columIndex = 0;
                rowIndex++;
            }
        }
        Debug.Log(_ticTakToeBoard.Length);
    }


    private void InitializeAiMode(GameMode gameMode)
    {
        if (gameMode == GameMode.SimpleAI)
        {
            _aiTypeTurn = SimpleAiTurn;
        }
        else if (gameMode == GameMode.MiniMaxAi)
        {
            _aiTypeTurn = MiniMaxAiTurn;
        }
    }

    private bool CheckWin()
    {
        return CheckRows() || CheckColumn() || CheckDiagonals();
    }

    private bool CheckRows()
    {
        for (int i = 0; i < 3; i++)
        {
            string firstInRow = GetSlotText(i, 0);
            if (firstInRow == " " || firstInRow == null)
                return false;

            if (GetSlotText(i, 1) == firstInRow && GetSlotText(i, 2) == firstInRow)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckColumn()
    {
        for (int i = 0; i < 3; i++)
        {
            string firstInColumn = GetSlotText(0, i);

            if (firstInColumn == " " || firstInColumn == null)
                return false;

            if (GetSlotText(1, i) == firstInColumn && GetSlotText(2, i) == firstInColumn)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckDiagonals()
    {
        int leftDiagonalMatch = 0;
        int rightDiagonalMatch = 0;

        string middleSlot = GetSlotText(1, 1);
        if (middleSlot == " " || middleSlot == null)
            return false;

        for (int i = 0; i < 3; i++)
        {
            if (GetSlotText(i, i) == middleSlot)
            {
                leftDiagonalMatch++;
            }

            if (GetSlotText(i, 2 - i) == middleSlot)
            {
                rightDiagonalMatch++;
            }
        }

        return leftDiagonalMatch == 3 || rightDiagonalMatch == 3;
    }

    private bool CheckTie()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (GetSlotText(i, j) == " ")
                {
                    return false;
                }
            }
        }
        return true;
    }

    // private void ToggleSimpleAi(bool onOrOff)
    // {
    //     _isSimpleAiTurn = onOrOff;
    // }


    public void OnSlotClicked(TicTakToeSlot slot) //connected to the buttons
    {
        if (slot.GetTextInSlot() != " " || _winner != "" && _gameMode != GameMode.AIvsAI) //if the slot isnt emptky and the winner is announced return
            return;

        if (_gameMode == GameMode.TwoPlayer || _gameMode != GameMode.TwoPlayer && _isItXTurn)
        {
            string player = _isItXTurn ? "X" : "O"; //if its xs turn the player is x and if not the player is o

            slot.ChangeTextsInSlot(player);
            Debug.Log("placed: " + player);

            if (CheckGameEnd(player))
                return;

            _isItXTurn = !_isItXTurn; //switch turns

            if (!_isItXTurn && _gameMode != GameMode.TwoPlayer && _winner == "" && !CheckTie()) //if play mode is not tow players then run ai play
            {
                _aiTypeTurn.Invoke();
            }
        }
    }


    private void SimpleAiTurn()
    {
        _simpleAI.MakeMove(ticTakToeSlots);
        //ToggleSimpleAi(false);

        if (CheckGameEnd("O"))
            return;

        _isItXTurn = true;
        Debug.Log("ai turn");
    }

    private void MiniMaxAiTurn()
    {
        Vector2Int bestMove = _miniMaxAi.GetBestMove(CreateMiniMaxArray(_ticTakToeBoard), "O");
        TicTakToeSlot chosenSlot = _ticTakToeBoard[bestMove.x, bestMove.y];
        StartCoroutine(MiniMaxPlay(Random.Range(0.2f, 2), chosenSlot));
    }

    private IEnumerator MiniMaxPlay(float delay, TicTakToeSlot chosenSlot)
    {
        yield return new WaitForSeconds(delay);

        chosenSlot.ChangeTextsInSlot("O");
        CheckGameEnd("O");


        _isItXTurn = true;
        Debug.Log("MINI MAX ai turn");
    }



    private string[,] CreateMiniMaxArray(TicTakToeSlot[,] ticTakToeBoard)
    {
        string[,] minMaxBoard = new string[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                minMaxBoard[i, j] = ticTakToeBoard[i, j].GetTextInSlot();
            }
        }
        return minMaxBoard;
    }

    private IEnumerator PlayAIvsAI()
    {

        yield return null; //wait before starting the ai dual

        while (_winner == "")
        {
            string currentPlayer = _isItXTurn ? "X" : "O";

            Debug.Log($"player - {currentPlayer} turn");

            Vector2Int bestMove = _miniMaxAi.GetBestMove(CreateMiniMaxArray(_ticTakToeBoard), currentPlayer);
            TicTakToeSlot chosenSlot = _ticTakToeBoard[bestMove.x, bestMove.y];

            yield return new WaitForSeconds(Random.Range(0.2f, 2));

            chosenSlot.ChangeTextsInSlot(currentPlayer);
            if (CheckGameEnd(currentPlayer))
            {
                break;
            }

            _isItXTurn = !_isItXTurn;

            if (_winner != "" || CheckTie())
            {
                yield break;
            }
        }
    }


    private bool CheckGameEnd(string player)
    {
        if (CheckWin())
        {
            _winner = player;
            Debug.Log($"the winner is {player}");
            _gameUI.UpdateWinStateText("Win", player);
            return true;
        }

        if (CheckTie())
        {
            _gameUI.UpdateWinStateText("Tie");

            Debug.Log("It's a tie");
            return true;
        }
        return false;
    }

    private string GetSlotText(int row, int col)
    {
        return _ticTakToeBoard[row, col].GetTextInSlot();
    }
}
