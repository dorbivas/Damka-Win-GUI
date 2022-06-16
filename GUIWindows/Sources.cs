﻿namespace GUIWindows
{
    using System.IO;
    class Sources
    {
        private static readonly string sr_WhitePieceImage = "WhitePiece.png";
        private static readonly string sr_WhiteKingImage = "WhiteKing.png";
        private static readonly string sr_BlackPieceImage = "BlackPieceG.png";
        private static readonly string sr_BlackKingImage = "‏‏BlackKing.png";
        private static readonly string sr_NullCelllImage = "‏‏NullCell.png";
        private static readonly string sr_EmptyCellImage = "EmptyCell.png";
        private static readonly string sr_PiecePressedImage = "PiecePressed.png";
        private static readonly string sr_SourcesPath;

        static Sources()
        {
            sr_SourcesPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources");
        }

        public static string WhitePiece
        {
            get => sr_WhitePieceImage;
        }

        public static string WhiteKingPiece
        {
            get => sr_WhiteKingImage;
        }

        public static string BlackPiece
        {
            get => sr_BlackPieceImage;
        }

        public static string BlackKingPawn
        {
            get => sr_BlackKingImage;
        }

        public static string PiecePressedImage
        {
            get => sr_PiecePressedImage;
        }

        public static string Disabled
        {
            get => sr_NullCelllImage;
        }

        public static string Empty
        {
            get => sr_EmptyCellImage;
        }

        public static string SourcesPath
        {
            get => sr_SourcesPath;
        }
    }
}