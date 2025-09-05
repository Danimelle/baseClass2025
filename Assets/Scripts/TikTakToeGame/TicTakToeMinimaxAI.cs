using System.Collections;
using UnityEngine;

public class TicTakToeMinimaxAI : MonoBehaviour
{


    public Vector2Int GetBestMove(string[,] minimaxBoard, string aiPlayer)
    {

        int bestScore = int.MinValue;
        Vector2Int bestMove = new Vector2Int(-1, -1);

        string secondPlayer = aiPlayer == "O" ? "X" : "O";

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (minimaxBoard[i, j] == " ")
                {
                    minimaxBoard[i, j] = aiPlayer;
                    int score = MiniMax(minimaxBoard, false, aiPlayer);
                    minimaxBoard[i, j] = " ";

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2Int(i, j);
                    }
                }
            }
        }

        return bestMove;
    }

    private int MiniMax(string[,] miniMaxBoard, bool isMaxing, string aiPlayer)
    {
        int? score = EvaluateBoard(miniMaxBoard, aiPlayer);
        if (score.HasValue)
            return score.Value;

        string secondPlayer = aiPlayer == "O" ? "X" : "O";


        if (isMaxing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (miniMaxBoard[i, j] == " ")
                    {
                        miniMaxBoard[i, j] = aiPlayer;
                        int result = MiniMax(miniMaxBoard, false, aiPlayer);
                        miniMaxBoard[i, j] = " ";
                        bestScore = Mathf.Max(bestScore, result);
                    }
                }
            }

            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (miniMaxBoard[i, j] == " ")
                    {
                        miniMaxBoard[i, j] = secondPlayer;
                        int result = MiniMax(miniMaxBoard, true, aiPlayer);
                        miniMaxBoard[i, j] = " ";
                        bestScore = Mathf.Min(bestScore, result);
                    }
                }
            }
            return bestScore;
        }
    }

    private int? EvaluateBoard(string[,] miniMaxBoard, string aiPlayer)
    {
        bool emptySlotFound = false;
        for (int i = 0; i < 3; i++)
        {
            //check rows
            if (miniMaxBoard[i, 0] != " " && miniMaxBoard[i, 0] == miniMaxBoard[i, 1] && miniMaxBoard[i, 1] == miniMaxBoard[i, 2])
            {
                return miniMaxBoard[i, 0] == aiPlayer ? 10 : -10;
            }

            //check colums
            if (miniMaxBoard[0, i] != " " && miniMaxBoard[0, i] == miniMaxBoard[1, i] && miniMaxBoard[1, i] == miniMaxBoard[2, i])
            {
                return miniMaxBoard[0, i] == aiPlayer ? 10 : -10;
            }

            //check tie
            for (int j = 0; j < 3; j++)
            {
                if (miniMaxBoard[i, j] == " ")
                {
                    emptySlotFound = true;
                }
            }
        }

        //check diagonal
        if (miniMaxBoard[1, 1] != " ")
        {
            string middleSlot = miniMaxBoard[1, 1];
            if (miniMaxBoard[0, 0] == middleSlot && middleSlot == miniMaxBoard[2, 2])
            {
                return middleSlot == aiPlayer ? 10 : -10;
            }

            if (miniMaxBoard[0, 2] == middleSlot && middleSlot == miniMaxBoard[2, 0])
            {
                return middleSlot == aiPlayer ? 10 : -10;
            }
        }

        if (!emptySlotFound)
            return 0;

        return null;
    }

}
