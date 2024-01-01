using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    [SerializeField] private PersonnageData data;
    private int hp, mana, ad, ap, pr, mr;

    // Start is called before the first frame update
    void Start()
    {
        hp = data.Hp; mana = data.Mana;
        ad = data.Ad; ap = data.Ap;
        pr = data.Pr; mr = data.Mr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
