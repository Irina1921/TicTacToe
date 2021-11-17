using System;
using System.Text;

namespace Tic_tac_toe
{
    class Program
    {
        private static TicTacToe g = new TicTacToe();

        public enum Winner
        {
            Crosses,
            Zeros,
            Draw,
            UnFinished
        }
        public enum State
        {
            Cross,
            Zero,
            Unset
        }

        public class TicTacToe
        {
            private readonly State[] board = new State[9];

            public int MovesCounter { get; private set; }

            public TicTacToe()
            {
                for (int i = 0; i < 9; i++)
                {
                    board[i] = State.Unset;
                }
            }

            public void MakeMove (int index)
            {
                board[index - 1] = MovesCounter % 2 == 0 ? State.Cross : State.Zero;

                MovesCounter++;
            }

            public State GetState(int index) //возвращает состоянием клетки по индексу
            {
                return board[index - 1];
            }

            public Winner GetWinner()
            {
                return GetWinner(1, 4, 7, 2, 5, 8, 3, 6, 9, 
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 
                    1, 5, 9, 3, 5, 7);
            }

            private Winner GetWinner(params int[] indexes)
            {
                for (int i = 0; i < 24; i += 3)
                {
                    bool same = AreSame(indexes[i], indexes[i + 1], indexes[i + 2]);
                    if (same)
                    {
                        State state = GetState(indexes[i]);
                        if(state != State.Unset)
                        {
                            return state == State.Cross ? Winner.Crosses : Winner.Zeros;
                        }
                    }
                }
                if (MovesCounter < 9)
                    return Winner.UnFinished;
                return Winner.Draw;
            }
            private bool AreSame(int a, int b, int c)
            {
                return GetState(a) == GetState(b) && GetState(a) == GetState(c);
            }
                
        }

        static string Print()
        {
            Console.Clear();
            var sb = new StringBuilder();
            sb.AppendLine("   _________________ ");
            for (int i = 1; i <= 7; i += 3)
            {
                sb.AppendLine("  |     |     |     |")
                  .AppendLine(
                    $"  |  {PrintChar(i)}  |  {PrintChar(i + 1)}  |  {PrintChar(i + 2)}  |")
                  .AppendLine("  |_____|_____|_____|  ");
            }
            return sb.ToString();
        }

        static string PrintChar(int index)
        {
            State state = g.GetState(index);
            if (state == State.Unset)
                return index.ToString();
        return state == State.Cross ? "X" : "O";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Print());
            int count = 0;

            while(g.GetWinner() == Winner.UnFinished)
            {
                if (count % 2 == 0) Console.WriteLine("Ход крестиков: ");
                else Console.WriteLine("Ход ноликов: ");
                int index = int.Parse(Console.ReadLine());
                g.MakeMove(index);

                count += 1;

                Console.WriteLine();
                Console.WriteLine(Print());

            }
            if (g.GetWinner() == Winner.Crosses) Console.WriteLine("Победили крестики!! Поздравляем!");
            else Console.WriteLine("Победили нолики!! Поздравляем!!");
            Console.ReadLine();
        }
    }
}
