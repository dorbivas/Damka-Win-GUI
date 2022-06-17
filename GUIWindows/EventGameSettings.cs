namespace GUIWindows
{
    using System;
    using GameEngine;

    public class EventGameSettings : EventArgs
    {
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly Board.eBoardSizes r_BoardSize;
        private readonly Player.ePlayerType r_OpponentType;
        private Player.ePlayerNumber m_CurrentPlayerNumber;
        private string m_CurrentPlayer;
        private string m_NextPlayer;
        private int m_Player1Score = 0;
        private int m_Player2Score = 0;

        public EventGameSettings(string i_Player1Name, string i_Player2Name, Board.eBoardSizes i_BoardSize, Player.ePlayerType i_Player2Type)
        {
            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_BoardSize = i_BoardSize;
            r_OpponentType = i_Player2Type;
            m_CurrentPlayer = r_Player1Name;
            m_NextPlayer = r_Player2Name;
            m_CurrentPlayerNumber = Player.ePlayerNumber.PlayerOneX;
        }

        public Board.eBoardSizes BoardSize
        {
            get => r_BoardSize;
        }

        public string Player1Name
        {
            get => r_Player1Name;
        }

        public string Player2Name
        {
            get => r_Player2Name;
        }

        public void SetPlayers(string i_CurrentPlayer)
        {
            m_CurrentPlayer = i_CurrentPlayer;
            m_NextPlayer = i_CurrentPlayer == r_Player2Name ? r_Player1Name : r_Player2Name;
        }

        public Player.ePlayerType Player2Type
        {
            get => r_OpponentType;
        }

        public Player.ePlayerNumber CurrentPlayerNumber
        {
            get => m_CurrentPlayerNumber;
            set => m_CurrentPlayerNumber = value;
        }

        public string CurrentPlayer
        {
            get => m_CurrentPlayer;
            set => m_CurrentPlayer = value;
        }

        public string NextPlayer
        {
            get => m_NextPlayer;
            set => m_NextPlayer = value;
        }
        
        public int Player2Score 
        { 
            get => m_Player2Score; 
            set => m_Player2Score = value; 
        }
        
        public int Player1Score 
        { 
            get => m_Player1Score; 
            set => m_Player1Score = value; 
        }
    }
}