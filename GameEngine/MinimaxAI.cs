using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public static class MinimaxAI
    {
        const int INF = 1000;

        public static int Minimax(GameLogic game, int depth, int player, out Move bestMove)
        {
            bestMove = game.CurrentPlayer.GetValidMoves(game.Board)[0];

            if (depth == 0)
            {
                return getScore(game);
            }

            switch (game.GameStatus)
            {
                case GameLogic.eGameStatus.Draw:
                    return 0;
                case GameLogic.eGameStatus.Player2OWin:
                    return player * INF;
                case GameLogic.eGameStatus.Player1XWin:
                    return -player * INF;
            }

            int bestScore = player * INF;
            foreach(var move in game.NextPlayer.GetValidMoves(game.Board))
            {
                GameLogic copy = game.Copy();
                copy.ExecuteSingleTurn(move);
                int curScore = Minimax(copy, depth - 1, -player, out bestMove);
                if (curScore > bestScore)
                {
                    bestScore = curScore;
                    bestMove = move;
                }
            }

            return player * bestScore;
        }

        private static int getScore(GameLogic game)
        {
            return game.CurrentPlayer.Score - game.NextPlayer.Score;
        }
    }
}