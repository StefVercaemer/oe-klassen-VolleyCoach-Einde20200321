using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volley.Lib.Entities
{
    public class Speler
    {
        private static Random random = new Random();

        const int minNummer = 1;
        const int maxNummer = 14;

        public Guid Id { get; set; }

        public string Naam { get; set; }

        public int Nummer { get; set; }

        public Plaatsen Plaats { get; set; }

        public DateTime InschrijvingsDatum { get; set; }

        public static int[] AantalPerPlaats { get; set; } = new int[3];
        
        public Speler() : this("John Doe", 0)
        {

        }

        public Speler(string naam, int nr, 
            Plaatsen positie = Plaatsen.Aanvaller, Guid? guid = null,
            DateTime? datum = null)
        {
            Naam = naam;
            Nummer = (nr <= 0) ? random.Next(minNummer, maxNummer + 1) : nr;
            Plaats = positie;
            VerhoogAantalPerPlaats();
            if (guid == null) Id = Guid.NewGuid();
            else Id = (Guid)guid;
            if (datum == null) InschrijvingsDatum = DateTime.Today;
            else InschrijvingsDatum = (DateTime)datum;
        }

        void VerhoogAantalPerPlaats()
        {
            int indexInArray = (int)Plaats;
            AantalPerPlaats[indexInArray]++;
        }

        public override string ToString()
        {
            string info;
            info = $"{Naam} (Nr. {Nummer})\n=> {Plaats}";
            return info;
        }
    }
}
