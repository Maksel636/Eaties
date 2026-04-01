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
        public Transform player;
        public Transform indicatororigin;

        public float rotation;
        public int direction;
        public float escapeSteps;
    }
}

