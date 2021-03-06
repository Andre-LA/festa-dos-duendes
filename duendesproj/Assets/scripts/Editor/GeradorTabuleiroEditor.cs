﻿using UnityEngine;
using UnityEditor;

namespace Componentes.Tabuleiro
{
    [CustomEditor(typeof(GeradorTabuleiro))]
    public class GeradorTabuleiroEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GeradorTabuleiro gt = (GeradorTabuleiro)target;

            if (GUILayout.Button("Gerar Tabuleiro"))
            {
                Conector[] conectores = gt.paiConectores.GetComponentsInChildren<Conector>();
                for (int i = 0; i < conectores.Length; i++)
                    EditorUtility.SetDirty(conectores[i]);

                gt.GerarCasas();
            }

            if (GUILayout.Button("Atualizar Casas"))
            {
                CasaBase[] casas = gt.paiConectores.GetComponentsInChildren<CasaBase>();
                for (int i = 0; i < casas.Length; i++)
                    EditorUtility.SetDirty(casas[i]);

                gt.AtualizaCasas();
            }
        }
    }
}
