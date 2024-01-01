using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Personnage] ", menuName = "Data/Personnage")]
public class PersonnageData : ScriptableObject
{
    private int hp, mana, ap, ad, mr, pr;

    public int Hp { get => hp; }
    public int Mana { get => mana; }
    public int Ap { get => ap; }
    public int Ad { get => ad; }
    public int Mr { get => mr; }
    public int Pr { get => pr; }
}
