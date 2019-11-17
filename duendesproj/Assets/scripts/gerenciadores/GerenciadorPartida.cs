﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Componentes.Jogador;
using Componentes.Tabuleiro;
using Identificadores;

namespace Gerenciadores
{
    public class GerenciadorPartida : MonoBehaviour
    {
        public Transform paiConectores;
        public GameObject jogadorPrefab;
        public Text textoPartida;
        [HideInInspector]
        public EscolheRota _escolheRota;
        public static Movimentacao MovAtual { get; set; }
        public static Inventario InvAtual { get; set; }

        private List<Transform> ordemJogadores = new List<Transform>();
        private int rodada = 1, turno = 0;

        private void Start()
        {
            for (int i = 0; i < GerenciadorGeral.qtdJogadores; i++)
            {
                Transform jogador = Instantiate(jogadorPrefab).transform;
                ordemJogadores.Add(jogador.transform);

                Transform casa = paiConectores.GetChild(i);
                jogador.transform.position = casa.position;
                jogador.GetComponent<Movimentacao>().casaAtual = casa;
            }

            MovAtual = ordemJogadores[0].GetComponent<Movimentacao>();
            InvAtual = ordemJogadores[0].GetComponent<Inventario>();

            _escolheRota.estadoUIRota(false);
            _escolheRota.estadoUICarta(true);
        }

        public void NovaRodada()
        {
            turno++;
            if (turno == ordemJogadores.Count)
            {
                turno = 0;
                rodada++;
            }

            MovAtual = ordemJogadores[turno].GetComponent<Movimentacao>();
            InvAtual = ordemJogadores[turno].GetComponent<Inventario>();

            textoPartida.text = "Jogador: " + (turno + 1) + "\nRodada: " + rodada;

            if (InvAtual.itens.Contains(Itens.Garrafa))
            {
                InvAtual.itens.Remove(Itens.Garrafa);
                //Transform garrafa = InvAtual.transform.GetChild(1);
                //Destroy(garrafa.gameObject);

                NovaRodada();
            }
            else
                _escolheRota.estadoUICarta(true);
        }

        public void MoverJogador(int casa)
        {
            _escolheRota.estadoUICarta(false);
            StartCoroutine(MovAtual.ProcuraCasa((TiposCasa)casa));
        }

        public void fimMov(bool casaEncontrada)
        {
            if (casaEncontrada)
            {
                Transform casaJogador = MovAtual.GetComponent<Movimentacao>().casaAtual;
                EventosCasa _eventCasa = casaJogador.GetComponent<EventosCasa>();
                _eventCasa.ativarCasa();

                NovaRodada();
            }
            else
            {
                _escolheRota.paraFrente = MovAtual.paraFrente;
                _escolheRota.estadoUIRota(true);
            }
        }

        public Transform ObterJogadorAtivo()
        {
            return ordemJogadores[turno];
        }
    }
}
