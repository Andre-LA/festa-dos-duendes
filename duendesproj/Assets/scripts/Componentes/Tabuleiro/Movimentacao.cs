﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Componentes.Tabuleiro
{
    public class Movimentacao : MonoBehaviour
    {
        //Achar casa
        public GerenciadorPartida _gerenPart;
        public Transform casaAtual;
        [HideInInspector]
        public int proximaCor;

        //Animar pulo
        [HideInInspector]
        public Queue<Vector3> fila = new Queue<Vector3>();
        private Vector3 animAtual;
        private float duracaoPulo = 0.25f;

        void Start()
        {
            animAtual = Vector3.zero;

            SetCasaAtual(casaAtual);
        }

        public void SetCasaAtual(Transform casa)
        {
            casaAtual = casa;
            fila.Enqueue(casa.position);
            if (animAtual == Vector3.zero)
            {
                animAtual = fila.Dequeue();
                StartCoroutine(Pulinho(animAtual, Time.time));
            }
        }

        public IEnumerator ProcuraCasa(int corDesejada)
        {
            bool achou = false;
            Transform casaTemp = casaAtual;
            int corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

            if (corTemp != 0 && corTemp == proximaCor)
            {
                achou = true;
                proximaCor = 0;
            }
            else
            {
                do
                {
                    casaTemp = casaTemp.GetComponent<CasaBase>().casaSeguinte[0];
                    corTemp = casaTemp.GetComponent<CasaBase>().tipoCasa;

                    SetCasaAtual(casaTemp);

                    if (corTemp == 0)
                    {
                        CasaBase _casaBase = casaTemp.GetComponent<CasaBase>();
                        if (_casaBase.casaSeguinte.Count > 1) //Se o conector tem multiplos caminhos
                        {
                            proximaCor = corDesejada; //Salva cor desejada
                            break;
                        }
                    }
                    else if (corTemp == corDesejada || corTemp == proximaCor)
                    {
                        achou = true;
                        proximaCor = 0;
                    }
                } while (!achou);
            }

            while (fila.Count > 0)
            {
                yield return new WaitForSeconds(0.1f);
            }

            _gerenPart.fimMov(achou);
        }

        public IEnumerator Pulinho(Vector3 destino, float tempoInicio)
        {
            Vector3 centro = (transform.position + destino) * 0.5F;
            centro -= Vector3.up;

            Vector3 inicioRel = transform.position - centro;
            Vector3 finalRel = destino - centro;

            float x = (Time.time - tempoInicio) / duracaoPulo;

            transform.position = Vector3.Slerp(inicioRel, finalRel, x);
            transform.position += centro;

            yield return new WaitForSeconds(0.02f);

            if (x <= 1)
                StartCoroutine(Pulinho(destino, tempoInicio));
            else
            {
                if (fila.Count > 0)
                {
                    animAtual = fila.Dequeue();
                    StartCoroutine(Pulinho(animAtual, Time.time));
                }
                else
                    animAtual = Vector3.zero;
            }
        }
    }
}