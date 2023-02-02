using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CData
{
    [SerializeField] public string name;
    public int age;
    public string gender;
    public Personality personality;
    public Life life;
    public Skills skills;

    [Serializable]
    public class Personality
    {
        public int introverted;
        public int extroverted;
        public int creative;
        public int logical;
        public int caring;
        public int assertive;
    }

    [Serializable]
    public class Life
    {
        public string education;
        public string career;
        public string goals;
        public string hobbies;
    }

    [Serializable]
    public class Skills
    {
        public int intelligence;
        public int wisdom;
        public int dexterity;
        public int strength;
        public int constitution;
        public int charisma;
    }
}
