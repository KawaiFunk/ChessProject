﻿namespace ChessLogic
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.NorthEast,
            Direction.East,
            Direction.SouthEast,
            Direction.South,
            Direction.SouthWest,
            Direction.West,
            Direction.NorthWest
        };
        public King(Player color)
        {
            Color = color;
        }

        private static bool IsUnmovedRook(Position pos, Board board)
        {
            if (board.IsEmpty(pos))
            {
                return false;
            }
            
            Piece piece = board[pos];
            return piece.Type == PieceType.Rook && !piece.HasMoved;
        }

        private static bool AllEmpty(IEnumerable<Position> postitions, Board board)
        {
            return postitions.All(pos => board.IsEmpty(pos));
        }

        private bool CanCastleKingSide(Position from, Board board)
        {
            if (HasMoved)
            {
                return false;
            }
            
            Position rookPos = new Position(from.Row, 7);
            Position[] betweenPositions = new Position[]
            {
                new (from.Row, 5),
                new (from.Row, 6)
            };

            return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
        }

        private bool CanCastleQueenSide(Position from, Board board)
        {
            if (HasMoved)
            {
                return false;
            }

            Position rookPos = new Position(from.Row, 0);
            Position[] betweenPositions = new Position[]
            {
                new (from.Row, 1),
                new (from.Row, 2),
                new (from.Row, 3)
            };

            return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;

                if (!Board.IsInside(to))
                {
                    continue;
                }

                if (board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Board board, Position pos)
        {
            foreach (Position to in MovePositions(pos, board))
            {
                yield return new NormalMove(pos, to);
            }

            if (CanCastleKingSide(pos, board))
            {
                yield return new Castle(MoveType.CastleKS, pos);
            }

            if (CanCastleQueenSide(pos, board))
            {
                yield return new Castle(MoveType.CastleQS, pos);
            }
        }

        public override bool CanCaptureOpponentKIng(Position from, Board board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }


    }
}
