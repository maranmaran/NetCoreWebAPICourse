using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonAPI.BusinessLayer.Models
{
    public class BattleResult
    {
        public Guid? WinnerID { get; set; }
        public Guid? LoserID { get; set; }
        public bool Draw { get; set; }
        
        public BattleResult(Guid? winnerID, Guid? loserID, bool draw = false)
        {
            WinnerID = winnerID;
            LoserID = loserID;
            Draw = draw;
        }
    }
}
