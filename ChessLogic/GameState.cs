namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;

        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(Board, pos);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move)
        {
            Board.SetPawnSkipPosition(CurrentPlayer, null);
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }

        public IEnumerable<Move> AllLegalMoves(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(Board, pos);
            });

            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        /*private void CheckForGameOver()
        {
            if (!AllLegalMoves(CurrentPlayer).Any())
            {
                if (Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
        }*/

        private void CheckForGameOver()
        {
            // Check if the current player is in check after the opponent's move
            if (Board.IsInCheck(CurrentPlayer))
            {
                // Check if the current player has no legal moves (checkmate scenario)
                if (!AllLegalMoves(CurrentPlayer).Any())
                {
                    Result = Result.Win(CurrentPlayer.Opponent()); // Opponent wins (checkmate)
                }
            }
            else
            {
                // If the current player is not in check, check if they have no legal moves (stalemate scenario)
                if (!AllLegalMoves(CurrentPlayer).Any())
                {
                    Result = Result.Draw(EndReason.Stalemate); // No legal moves, it's a stalemate
                }
            }
        }


        public bool IsGameOver()
        {
            return Result != null;
        }
    }
}
