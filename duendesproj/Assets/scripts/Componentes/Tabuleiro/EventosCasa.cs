﻿using System.Reflection;
using UnityEngine;

namespace Componentes.Tabuleiro
{
    public enum Eventos
    { Nada, Pote, Elementos, PowerUp, Acontecimento, Minijogo, Portal }

    public class EventosCasa : MonoBehaviour
    {
        public string nomeEvento;
        public int aux;

        public void ativarCasa()
        {
            MethodInfo metodo = GetType().GetMethod(nomeEvento);
            try
            {
                metodo.Invoke(this, null);
            }
            catch { }
        }

        public void Pote()
        {
            Debug.Log("Preso no pote");
        }

        public void Elementos()
        {
            Debug.Log("Água, fogo, terra ou ar");
        }

        public void PowerUp()
        {
            Debug.Log("PowerUp aleatório");
        }

        public void Acontecimento()
        {
            Debug.Log("Acontecimento aleatório");
        }

        public void Minijogo()
        {
            Gerenciadores.GerenciadorGeral.TransitarParaMJ((Identificadores.CenaID)aux);
        }

        public void Portal()
        {
            Debug.Log("Teleportar");
        }
    }
}