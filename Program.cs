using System;
using System.Collections.Generic;

namespace N_Queen
{
    class Program
    {
        static void Main(string[] args)
        {
            var queen = new Queen(8);
            queen.Recursive(0);
            queen.Print();
            //ans: 92

            // 可自行實作碰撞檢核與打印邏輯的建構涵式
            //int n = 8;
            //var queen2 = new Queen(n, new Valid(), new Display(n));
        }
    }

    /// <summary>
    /// 棋子物件
    /// </summary>
    public class Queen
    {
        /// <summary>
        /// Constructor 建立N*N棋盤
        /// </summary>
        /// <param name="n"></param>
        public Queen(int n)
        {
            this.N = n;
            this.history = new List<int>();
            this.Result = new List<List<int>>();
            this.Valid = new Valid();
            this.Display = new Display(n);
        }

        /// <summary>
        /// Constructor 建立N*N棋盤，並且自訂檢核碰撞與打印方法
        /// </summary>
        /// <param name="n"></param>
        /// <param name="valid"></param>
        /// <param name="display"></param>
        public Queen(int n, IValid valid, IDisplay display)
        {
            this.N = n;
            this.history = new List<int>();
            this.Result = new List<List<int>>();
            this.Valid = valid;
            this.Display = display;
        }

        /// <summary>
        /// N*N
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// 最後結果
        /// </summary>
        public List<List<int>> Result { get; set; }

        /// <summary>
        /// 檢驗碰撞的方法
        /// </summary>
        public IValid Valid { get; set; }

        /// <summary>
        /// 打印的方法
        /// </summary>
        public IDisplay Display { get; set; }

        /// <summary>
        /// 每層棋子排放的紀錄
        /// </summary>
        private List<int> history { get; set; }

        /// <summary>
        /// 遞迴求解涵式
        /// </summary>
        /// <param name="layer"></param>
        public void Recursive(int layer)
        {         
            for (int i = 0; i < this.N; i++)
            {
                if (this.Valid.Check(layer, i, history))
                {
                    history.Add(i);
                    Recursive(layer + 1);
                    if (layer == this.N - 1)
                    {
                        Result.Add(new List<int>(history));
                    }
                    history.RemoveAt(history.Count - 1);
                }
            }
        }

        /// <summary>
        /// 打印涵式
        /// </summary>
        public void Print()
        {
            this.Display.Print(this.Result);
        }
    }

    /// <summary>
    /// 檢核碰撞介面
    /// </summary>
    public interface IValid
    {
        bool Check(int layer, int cur, List<int> history);
    }

    /// <summary>
    /// 碰撞邏輯實作(如需變換碰撞方式，可繼承IValid另外實作)
    /// </summary>
    public class Valid : IValid
    {
        public bool Check(int layer, int cur, List<int> history)
        {
            for (int i = 0; i < layer; i++)
            {
                int data = history[i];
                if (cur == data)
                    return false;
                else if ((i + data) == (layer + cur))
                    return false;
                else if ((i - data) == (layer - cur))
                    return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 打印介面
    /// </summary>
    public interface IDisplay
    {
        int N { get; set; }
        void Print(List<List<int>> data);
    }

    /// <summary>
    /// 打印邏輯實作(如需變換打印方式，可繼承IDisplay另外實作)
    /// </summary>
    public class Display : IDisplay
    {
        public Display(int n)
        {
            this.N = n;
        }

        public int N { get; set; }

        /// <summary>
        /// 模擬試卷上的顯示方法(換行、文字等)
        /// </summary>
        /// <param name="data"></param>
        public void Print(List<List<int>> data)
        {
            Console.WriteLine();
            for (int s = 0; s < data.Count; s++)
            {
                Console.WriteLine($"// Solution {s+1}");
                for (int i = 0; i < this.N; i++)
                {
                    int j;
                    for (j = 0; j < data[s][i]; j++)
                    {
                        Console.Write(".");
                    }
                    Console.Write("Q");
                    for (j = data[s][i] + 1; j < this.N; j++)
                    {
                        Console.Write(".");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
