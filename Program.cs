using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Queen
{
    class Program
    {
        static void Main(string[] args)
        {
            var queen = new Queen(8);
            queen.Recursive(0);

            //ans: 92
        }
    }


    public class Queen
    {
        //Constructor
        public Queen(int n)
        {
            this.N = n;
            this.history = new List<int>();
            this.Result = new List<List<int>>();
            this.Valid = new Valid();
        }

        public Queen(int n, IValid valid)
        {
            this.N = n;
            this.history = new List<int>();
            this.Result = new List<List<int>>();
            this.Valid = valid;
        }

        public int N { get; set; }

        private List<int> history { get; set; }

        public List<List<int>> Result { get; set; }

        public IValid Valid { get; set; }

        public IDisplay Display { get; set; }

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

        private bool check(int layer, int cur)
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

    public interface IValid
    {
        bool Check(int layer, int cur, List<int> history);
    }

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

    public interface IDisplay
    {
        void Print();
    }


    public class Display : IDisplay
    {
        public void Print()
        {

        }
    }
}
