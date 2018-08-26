using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Models;

namespace BlackJack.Services
{
    public class JogosServices
    {
        public Jogo NovoJogo()
        {
            var jogo = new Jogo();

            jogo.JogadorAdd(Carta(jogo));
            jogo.JogadorAdd(Carta(jogo));

            jogo.DealerAdd(Carta(jogo));
            jogo.DealerAdd(Carta(jogo));

            jogo.Status = QuemGanhou(jogo);

            return jogo;
        }

        public Jogo NovaCarta(Jogo jogo)
        {
            jogo.JogadorAdd(Carta(jogo));

            jogo.Status = JogadorGanhou(jogo);
            
            return jogo;
        }

        public Jogo EncerrarJogo(Jogo jogo)
        {
            var dealerPontos = Pontuacao(jogo.DealerList);
            if(dealerPontos <= 17)
            { //3
                jogo.DealerAdd(Carta(jogo));
                dealerPontos = Pontuacao(jogo.DealerList);
            }
            
            if(dealerPontos <= 17)
            { //4
                jogo.DealerAdd(Carta(jogo));
                dealerPontos = Pontuacao(jogo.DealerList);
            }

            if(dealerPontos <= 17)
            { //5
                jogo.DealerAdd(Carta(jogo));
            }

            jogo.Status = DealerGanhou(jogo);
            
            return jogo;
        }

        private List<string> Baralho
        {
            get
            {
                var naipe = new List<string>() { "A", "2","3","4","5","6","7","8","9","10", "J","Q","K"};

                naipe.AddRange(naipe); //2
                naipe.AddRange(naipe); //4

                return naipe;
            }
        }

        private List<string> BaralhoSobra(Jogo item)
        {
            var sobra = Baralho;

            item.JogadorList.ForEach(carta => sobra.Remove(carta));
            item.DealerList.ForEach(carta => sobra.Remove(carta));

            return sobra;
        }

        private string Carta(Jogo item)
        {
            var baralho = BaralhoSobra(item);

            var random = new Random();
            var carta = random.Next(1, baralho.Count);

            return baralho[carta];
        }

        private int Pontuacao(List<string> cartas)
        {
            var pontuacao = 0;

            foreach(var carta in cartas)
            {
                switch(carta)
                {
                    case "A":
                        if(cartas.Any(item => (new List<string>(){ "10","J","Q","K"}).Contains(item)))
                        { pontuacao += 11; }
                        else
                        { pontuacao += 1; }
                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "10":
                        pontuacao += int.Parse(carta);
                        break;
                    case "J":
                    case "Q":
                    case "K":
                        pontuacao +=10;
                        break;
                }
            }

            return pontuacao;
        }

        private string QuemGanhou(Jogo jogo)
        {
            var jogadorPontos = Pontuacao(jogo.JogadorList);
            var dealerPontos = Pontuacao(jogo.DealerList);

            if(jogadorPontos == 21)
            {
                if(dealerPontos == 21)
                {
                    return "Empate";
                }
                
                return "Jogador";
            }

            if(dealerPontos == 21)
            {
                return "Dealer";
            }

            return "";
        }

        private string JogadorGanhou(Jogo jogo)
        {
            var jogadorPontos = Pontuacao(jogo.JogadorList);
            if(jogadorPontos == 21)
            {
                return "Jogador";
            }
            if(jogadorPontos > 21)
            {
                return "Dealer";
            }

            return "";
        }

        private string DealerGanhou(Jogo jogo)
        {
            var jogadorPontos = Pontuacao(jogo.JogadorList);
            var dealerPontos = Pontuacao(jogo.DealerList);

            if(dealerPontos > 21 || jogadorPontos > dealerPontos)
            {
                return "Jogador";
            }
            if(dealerPontos > jogadorPontos)
            {
                return "Dealer";
            }

            return "Empate";
        }
    }
}