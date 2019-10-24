﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Componentes.Tabuleiro
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public List<GameObject> ordemJogadores;
        public Text textoPartida;
        [HideInInspector]
        public Movimentacao jogadorAtual;
        public EscolheRota _escolheRota;
        private int rodada = 1, turno = 0;

        private void Awake()
        {
            jogadorAtual = ordemJogadores[0].GetComponent<Movimentacao>();
            _escolheRota.EstadoCanvasRota(false);
        }

        public void NovaRodada()
        {
            turno++;
            if (turno == ordemJogadores.Count)
            {
                turno = 0;
                rodada++;
            }

            jogadorAtual = ordemJogadores[turno].GetComponent<Movimentacao>();

            textoPartida.text = "Jogador: " + (turno + 1) + "\nRodada: " + rodada;
        }

        public void MoverJogador(int casa)
        {
            if (jogadorAtual.ProcuraCasa(casa))
                NovaRodada();
            else
                _escolheRota.EstadoCanvasRota(true);
        }
    }
}
