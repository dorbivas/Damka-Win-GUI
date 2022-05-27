using System;
using System.Collections.Generic;

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
    }
}
