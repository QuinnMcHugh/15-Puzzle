using System;
using System.Collections.Generic;

namespace Puzzle
{
    public class Puzzle
    {
        /* Puzzle constants */
        public const int BLANK = 0;
        public const int ROWS = 4, COLS = 4;
        public const int DEFAULT_MOVES_TO_SCRAMBLE = 100;

        /* Tiles representing the puzzle */
        private int[,] tiles;
        /* Quick access to where the blank tile is */
        Tuple<int, int> blankLocation;

        /* Random number generator for tile scrambling */
        private Random rnd;

        public Puzzle(){
            /* Create a new solved puzzle representation */
            tiles = new int[ROWS, COLS];
            for (int r = 0; r < ROWS; r++){
                for (int c = 0; c < COLS; c++){
                    tiles[r, c] = 1 + r * 4 + c;
                }
            }

			// Set last tile to be the blank
			blankLocation = Tuple.Create(ROWS - 1, COLS - 1);
            tiles[blankLocation.Item1, blankLocation.Item2] = BLANK;

            rnd = new Random();
        }

        public List<Tuple<int, int>> GetPossibleMoves(){
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();

            if (blankLocation.Item1 > 0){ // row is greater than 0
                possibleMoves.Add(Tuple.Create(-1, 0));
            }
            if (blankLocation.Item1 < ROWS - 1){ // row is less than max
                possibleMoves.Add(Tuple.Create(1, 0));
            }
            if (blankLocation.Item2 > 0){ // column is greater than 0
                possibleMoves.Add(Tuple.Create(0, -1));
            }
            if (blankLocation.Item2 < COLS - 1){ // column is less than max
                possibleMoves.Add(Tuple.Create(0, 1));
            }

            return possibleMoves;
        }

        private void ExecuteMove(Tuple<int, int> move){
            // Swap blank tile with one inhabiting the one indicated by 'move'
            int temp = tiles[move.Item1 + blankLocation.Item1, move.Item2 + blankLocation.Item2];
            tiles[move.Item1 + blankLocation.Item1, move.Item2 + blankLocation.Item2] = BLANK;
            tiles[blankLocation.Item1, blankLocation.Item2] = temp;
            blankLocation = Tuple.Create(move.Item1 + blankLocation.Item1, move.Item2 + blankLocation.Item2);
        }

        private void moveBlankOnce(){
            // Get all the moves possible
            List<Tuple<int, int>> moves = GetPossibleMoves();

            // Randomly choose one of those moves
            Tuple<int, int> chosen = moves[rnd.Next(0, moves.Count)];

            ExecuteMove(chosen);
        }

        public void Scramble(int moves = Puzzle.DEFAULT_MOVES_TO_SCRAMBLE){
            for (int i = 0; i < moves; i++){
                moveBlankOnce();
            }
        }

        public override String ToString(){
            String str = "";

            for (int r = 0; r < ROWS; r++){
                for (int c = 0; c < COLS; c++){
                    if (tiles[r, c] == BLANK){
                        str += "   ";
                    }
                    else {
                        str += " " + String.Format("{0,-2}", tiles[r, c]);
                    }

                    if (c != COLS - 1){
                        str += "|";
                    }
                }
                str += "\n";
                if (r != ROWS - 1){
                    str += "---|---|---|---\n";
                }
            }

            return str;
        }
    }
}
