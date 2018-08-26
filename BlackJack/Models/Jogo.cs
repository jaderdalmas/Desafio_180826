using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string JogadorCartas { get; set; }
        public string DealerCartas { get; set; }

        public string Status { get; set; }

        public Jogo()
        {
            JogadorCartas = "";
            DealerCartas = "";
            Status = "";
        }

        public void JogadorAdd(string carta)
        {
            JogadorCartas += (string.IsNullOrWhiteSpace(JogadorCartas) ? "" : "|") + carta;
        }

        public List<string> JogadorList
        {
            get
            {
                return JogadorCartas.Split("|").ToList();
            }
        }

        public void DealerAdd(string carta)
        {
            DealerCartas += (string.IsNullOrWhiteSpace(DealerCartas) ? "" : "|") + carta;
        }

        public List<string> DealerList
        {
            get
            {
                return DealerCartas.Split("|").ToList();
            }
        }
    }
}