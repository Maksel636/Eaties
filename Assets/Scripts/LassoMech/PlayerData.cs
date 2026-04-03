using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LassoMech
{
    [System.Serializable]
    public class PlayerData
    {
        public Transform Player;
        public Transform Indicatororigin;
        public Color PlayerColor;

        public float Rotation;
        public int Direction;
        public float EscapeSteps;
    }
}

