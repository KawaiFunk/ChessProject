﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        private readonly Direction forward;
        public Pawn(Player color)
        {
            Color = color;

            if (color == Player.White)
            {
                forward = Direction.North;
            }
            else
            {
                forward = Direction.South;
            }
        }
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmpty(pos);
        }

        private bool CanCaptureAt(Position pos, Board board)
        {
            if (!Board.IsInside(pos) || board.IsEmpty(pos))
            {
                return false;
            }

            return board[pos].Color != Color;
        }

        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            Position oneMovePosition = from + forward;

            if (CanMoveTo(oneMovePosition, board))
            {
                yield return new NormalMove(from, oneMovePosition);

                Position twoMovesPosition = oneMovePosition + forward;

                if (!HasMoved && CanMoveTo(twoMovesPosition, board))
                {
                    yield return new NormalMove(from, twoMovesPosition);
                }
            }
        }

        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
            {
                Position to = from + forward + dir;

                if (CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }

        public override IEnumerable<Move> GetMoves(Board board, Position pos)
        {
            return ForwardMoves(pos, board).Concat(DiagonalMoves(pos, board));
        }
    }
}
