using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KringeEnKruise.Properties
{
    class Player
    {
        public String Name   { get; set; }
        public int Score     { get; set; }
        public string Symbol { get; private set; }

        public bool isTurn  { get; set; }
        public bool Started { get; set; }

        public Player()
        {
            Name = "";
            Symbol = "";
            Score = 0;
            isTurn = false;
        }

        public Player(String Name, String Symb, int score)
        {
            this.Name = Name;
            Score = score;
            Symbol = Symb;
            isTurn = false;
        }

        public override string ToString()
        {
            return Score + " - " + Name + " (" + Symbol + ")";
        }
    }


}
