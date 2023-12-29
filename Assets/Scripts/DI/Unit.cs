using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public class Drone : Player
    {
        public override void Move()
        {
            Debug.Log("Move drone");
        }
    }

    public class Unit : Player
    {
        public override void Move()
        {
            Debug.Log("Move unit");
        }
    }

    public class Orc : Player, IStatic
    {
        public override void Move()
        {
            Debug.Log("Orc unit");
        }
    }
}
