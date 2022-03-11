using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LP.FDG.Units
{
    [CreateAssetMenu(fileName = "New Unit" , menuName = "New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum UnitType
        {
            Infantry,
            Car,
            Plane
        };

        public bool isPlayerUnit;

        public UnitType Type;

        public string Name;

        public GameObject HumanPrefab;
        public GameObject MachinePrefab;

        public int cost;
        public int attack;
        public int health;
        public int armor;


    }
}
