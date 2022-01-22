using System;
using System.Collections.Generic;
using System.Threading;
using ColorShapeLinks.Common;
using ColorShapeLinks.Common.AI;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = System.Random;

public class NewAI : AbstractThinker
{
   private List<FutureMove> possibleMoves;
   private List<FutureMove> nonLosingMoves;
   private Random random;

   public override void Setup(string str)
   {
      possibleMoves = new List<FutureMove>();
      nonLosingMoves = new List<FutureMove>();
      random = new Random();
   }

   public override FutureMove Think(Board board, CancellationToken ct)
   {
      var colorOfOurAI = board.Turn;
      
      possibleMoves.Clear();
      nonLosingMoves.Clear();

      for (var col = 0; col < Cols; col++)
      {
         //Skip when Column is full;
         if (board.IsColumnFull(col)) continue;
         
         for (var iShp = 0; iShp < 2; iShp++)
         {
            var shape = (PShape) iShp;
            
            //Skip when no more of these pieces
            if (board.PieceCount(colorOfOurAI, shape) == 0) continue;
            
            //Move is possible, add it to possible moves list 
            possibleMoves.Add(new FutureMove(col, shape));
            
            //Test
            board.DoMove(shape, col);
            
            //Winner?
            var winner = board.CheckWinner();
            
            //Undo the move
            board.UndoMove();
            
            //Ai Winner?
            if (winner.ToPColor() == colorOfOurAI)
            {
               //Return this move instantly
               return new FutureMove(col, shape);
            }
            //Otherwise, is our opponent the winner?
            else if (winner.ToPColor() != colorOfOurAI.Other())
            {
               //if it's not, add this move to the non-losing moves list
               nonLosingMoves.Add(new FutureMove(col, shape));
            }
         }
      }
      
      // Do we have any moves on the non-losing move list?
      if (nonLosingMoves.Count > 0)
      {
         // If so, return a random one
         return nonLosingMoves[random.Next(nonLosingMoves.Count)];
      }
      
      //Return any valid moves
      return possibleMoves[random.Next(possibleMoves.Count)];
   }
}
