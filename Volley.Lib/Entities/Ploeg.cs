using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volley.Lib.Entities
{
    public class Ploeg
    {
        public List<Speler> Spelers { get; set; }

        public string Naam { get; set; }

        public Ploeg(string naam)
        {
            Naam = naam;
            MaakPloeg();
        }

        void MaakPloeg()
        {
            Speler jan = new Speler();
            jan.Id = Guid.NewGuid();
            jan.Naam = "Jan";
            jan.Plaats = Plaatsen.Passeur;

            //Met object initializer
            Speler piet = new Speler
            {
                Id = Guid.NewGuid(),
                Naam = "Piet",
                Nummer = 2,
                Plaats = Plaatsen.Aanvaller
            };

            Speler frank = new Speler("Frank", 3, Plaatsen.Libero);

            Speler koen = new Speler("Koen", 4, guid: Guid.NewGuid());

            Spelers = new List<Speler> { jan, piet, frank, koen };
        }


        public void Verwijder(Speler teVerwijderen)
        {
            if (teVerwijderen != null && BehoortSpelerToPloeg(teVerwijderen))
            {
                Spelers.Remove(teVerwijderen);
            }
            else throw new Exception
            ("Geef een geldige speler door om te verwijderen");
        }

        public void SlaOp(Speler opTeSlaan)
        {
            if (opTeSlaan == null) throw new Exception("Geef een geldige speler door");
            else if (!BehoortSpelerToPloeg(opTeSlaan)) Spelers.Add(opTeSlaan);
            else
            {
                int indexSpeler = GeefIndexInPloeg(opTeSlaan);
                Spelers[indexSpeler] = opTeSlaan;
            }
        }

        int GeefIndexInPloeg(Speler teChecken)
        {
            int index = -1;
            for (int i = 0; i < Spelers.Count; i++)
            {
                if (Spelers[i].Id == teChecken.Id)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }


        bool BehoortSpelerToPloeg(Speler teChecken)
        {
            bool gevonden = false;
            foreach (Speler speler in Spelers)
            {
                if (speler.Id == teChecken.Id)
                {
                    gevonden = true;
                    break;
                }
            }
            return gevonden;
        }

        public override string ToString()
        {
            return $"{Naam} ({Spelers.Count})";
        }
    }
}
