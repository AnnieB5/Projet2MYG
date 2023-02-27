using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    /*static int nbreEssais = 6;

    //constante pour le restart
    const int essais = 6;

    static bool isWin = false;

    static bool isApplicationOn = true;

    static bool isError = false;

    static string motMystere = "";

    static string lettresJouees = ""; //ATTENTION, la valeur "" est utilisée dans une condition if, la changer si on change aussi la valeur initiale de cette variable !

    static string motConverti = "";

    static bool isDevOn = false;

    static string stringRecu = "";

    static string choixRecu = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */

    public void OnClickAfficherLettre(string letter)
    {
        Debug.Log("Lettre affichée: " + letter);
    }

}
