using System;
using System.Collections.Generic;
using System.Threading;

namespace GameEngine
{
    public class ComputerAI
    {
        private readonly Random r_Random = new Random();

        public Move ComputerMove(List<Move> i_NormalMovesList, List<Move> i_SkippingMovesList)
        {
            Move selectedMove = null;
            int index;

            if (i_SkippingMovesList.Count > 0)
            {
                index = r_Random.Next(i_SkippingMovesList.Count - 1);
                selectedMove = i_SkippingMovesList[index];
            }
            else if (i_NormalMovesList.Count > 0)
            {
                index = r_Random.Next(i_NormalMovesList.Count - 1);
                selectedMove = i_NormalMovesList[index];
            }

            return selectedMove;
        }

          //public Move MiniMax(List<Move> i_NormalMovesList, List<Move> i_SkippingMovesList)
        //{
            
        //}

        //public Move Minimax (Board board, int depth, bool isMaximizing, List<Move> i_NormalMovesList, List<Move> i_SkippingMovesList)
        //{
        //    if (depth == 0)
        //    {
        //        return null;
        //    }

        //    if (isMaximizing)
        //    {
        //        Move bestMove = null;
        //        int bestScore = int.MinValue;

        //        foreach (Move move in i_SkippingMovesList)
        //        {
        //            Board newBoard = board.Clone();
        //            newBoard.MakeMove(move);
        //            int score = Minimax(newBoard, depth - 1, false, i_NormalMovesList, i_SkippingMovesList).Score;
        //            if (score > bestScore)
        //            {
        //                bestScore = score;
        //                bestMove = move;
        //            }
        //        }

        //        foreach (Move move in i_NormalMovesList)
        //        {
        //            Board newBoard = board.Clone();
        //            newBoard.MakeMove(move);
        //            int score = Minimax(newBoard, depth - 1, false, i_NormalMovesList, i_SkippingMovesList).Score;
        //            if (score > bestScore)
        //            {
        //                bestScore = score;
        //                bestMove = move;
        //            }
        //        }

        //        bestMove.Score = bestScore;
        //        return bestMove;
        //    }
        //    else
        //    {
        //        Move bestMove = null;
        //        int bestScore = int.MaxValue;

        //        foreach (Move move in i_SkippingMovesList)
        //        {
        //            Board newBoard = board.Clone();
        //            newBoard.MakeMove(move);
        //            int score = Minimax(newBoard, depth - 1, true, i_NormalMovesList, i_SkippingMovesList).Score;
        //            if (score < bestScore)
        //            {
        //                bestScore = score;
        //                bestMove = move;
        //            }
        //        }

        //        foreach (Move move in i_NormalMovesList)
        //        {
        //            Board newBoard = board.Clone();
        //            newBoard.MakeMove(move);
        //            int score = Minimax(newBoard, depth - 1, true, i_NormalMovesList, i_SkippingMovesList).Score;
        //            if (score < bestScore)
        //            {
        //                bestScore = score;
        //                bestMove = move;
        //            }
        //        }

        //        bestMove.Score = bestScore;
        //        return bestMove;
        //    }
        //}

        //Move MakeMove(Board board, List<Move> i_NormalMovesList, List<Move> i_SkippingMovesList)
        //{
        //    Move bestMove = Minimax(board, 2, true, i_NormalMovesList, i_SkippingMovesList);
        //    return bestMove;
        //}
    }
}
