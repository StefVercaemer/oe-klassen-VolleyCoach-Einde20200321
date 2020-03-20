using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volley.Lib.Entities;

namespace Volley.Lib.Services
{
    public class WedstrijdBeheer
    {
        public Ploeg[] DeelnemendePloegen;

        public WedstrijdBeheer(Ploeg[] ploegen)
        {
            if (ploegen.Length == 2) DeelnemendePloegen = ploegen;
            else throw new Exception("Geef twee deelnemende ploegen");
        }
    }
}
