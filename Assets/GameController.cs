using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

public class GameController : MonoBehaviour
{
    int nbreEssais = 7;
    const int essais = 7; //constante pour le restart
    bool isWin = false;
    string motMystere = "";
    string motConverti = "";
    bool isDevOn = true;

    private IHMController ihm; //Objet ihm pour gérer les "vues"

    public delegate void ResetGame();
    public static event ResetGame ResetButtonState;
    

    //Use this for initialization
    void Start()
    {
        //ajout pour Unity GAME
        ihm = GetComponent<IHMController>();

        Restart();
    }

    //Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        //Initialisation et réinitialisation des variables
        isWin = false;
        nbreEssais = essais;

        //Initialisation et réinitialisation de l'affichage illustrative du pendu
        ihm.ResetSpritePendu();

        //Réinitialisation des boutons alphabet
        ResetButtonState?.Invoke();

        ihm.ShowActionMessage("Cliquez sur votre première lettre...");

        //génère un tableau de mots à deviner
        string[] banqueMots = new string[] { "ANTILOPE", "BISON", "CHEVAL", "DINDON", "ELEPHANT", "FAON", "GUEPARD", "HIBOU", "IGUANE" }; // première cellule => indice 0            

        //génère un nombre aléatoire entre 0 et la dernière cellule du tableau de mots à deviner
        System.Random rnd = new System.Random();
        int nombreMystereRandom = 0;
        nombreMystereRandom = rnd.Next(0, (banqueMots.Length - 1));

        //génère un mot choisi à partir du tableau de mots et du nombre aléatoire de sélection
        motMystere = banqueMots[nombreMystereRandom];

        //transformation et affichage du mot mystère en mode jeu (juste le nb de lettres donc, "_ _ _ _ ")
        int motLongueur = motMystere.Length;
        motConverti = string.Concat(Enumerable.Repeat("_ ", motLongueur));
        ihm.ShowMotConverti("Mot à deviner : " + motConverti);

        if (isDevOn == true)
        {
            ihm.ShowInfoDev(true, "Le mot mystère " + motMystere + " contient " + motLongueur + " caractères."); //debug
        }

        //affichage du nombre d'essais initial
        ihm.ShowInfoMessage("A chaque erreur, un essai vous sera retiré. Vous avez " + nbreEssais + " essai(s).");
    }

    public void CheckUserEnter(string lettreChoisie) //appelée quand clic user lettre alphabet
    {

        if (nbreEssais >= 1 && isWin == false) //boucle pour chaque tour/essai du jeu
        {
            string underscoreMot = "_";

            //comparaison de la lettre cliquée avec le mot à deviner
            bool contientRtatRecherche = motMystere.Contains(lettreChoisie);

            if (contientRtatRecherche == true)
            {
                //affiche le calque "RondImage" du bouton car la lettre choisie est dans le mot à deviner
                ihm.ShowRondImage();

                //recherche des positions des lettres trouvées sur ce tour, dans le mot mystère
                foreach (Match match in Regex.Matches(motMystere, lettreChoisie))
                {
                    //remplacement des lettres trouvées à ce tour dans le mot mystère à afficher
                    StringBuilder sb = new StringBuilder(motConverti);
                    sb[match.Index * 2] = Convert.ToChar(lettreChoisie);
                    motConverti = sb.ToString();
                }
            }

            else
            {
                //affiche le calque "CroixImage" du bouton car la lettre choisie n'est pas dans le mot à deviner
                ihm.ShowCroixImage();

                //change et passe l'affichage du pendu (illustration) à l'étape suivante
                ihm.ChangeImagePendu();

                //retrait d'une tentative
                nbreEssais--;
            }

            //affichage du mot à deviner, reconstitué partiellement ou pas en fonction des réponses    
            ihm.ShowMotConverti("Mot à deviner : " + motConverti);

            if (!motConverti.Contains(underscoreMot)) //cas où tout le mot mystère a été deviné, WIN
            {
                ihm.ShowInfoMessage("Félicitations, vous avez gagné !");
                isWin = true;
                ihm.ShowActionMessage("Voulez-vous rejouer, ou quitter ? Cliquez sur le bouton de votre choix.");
            }

            else
            {
                //affichage du nombre d'essais restants à l'issue du tour
                ihm.ShowInfoMessage("Il vous reste : " + nbreEssais + " essai(s).");

                if (nbreEssais == 0) //cas où le joueur a épuisé tous ses essais sans deviner le mot, GAME OVER
                {
                    ihm.ShowInfoMessage("Dommage, vous avez perdu !");
                    ihm.ShowActionMessage("Voulez-vous rejouer, ou quitter ? Cliquez sur le bouton de votre choix.");
                }

                else
                {
                   ihm.ShowActionMessage("Cliquez sur la lettre de votre choix."); 
                }

            }

        }

        else
        {
            ihm.ShowInfoDev(true, "sortie de la boucle des tours de cette partie de jeu"); //debug

            //demande à l'utilisateur s'il veut quitter
            ihm.ShowActionMessage("Voulez-vous rejouer, ou quitter ? Cliquez sur le bouton de votre choix.");
        }

    }

}
