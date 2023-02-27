using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Projet2
{
    class Program
    {
        static int nbreEssais = 6;

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
        static void Main(string[] args)
        {
            Start();
            while (isApplicationOn == true)
            {
                Update();
            }
        }

        static void Start()
        {
            nbreEssais = essais;
        }

        static void Update()
        {
            if (isError == false)
            {
                showTitle();

                //génère un tableau de mots à deviner
                string[] banqueMots = new string[] { "ANTILOPE", "BISON", "CHEVAL", "DINDON", "ELEPHANT", "FAON" }; // première cellule => indice 0
                /*
                Console.WriteLine(" nbre de cellules du tableau: "+banqueMots.Length);
                Console.WriteLine(" on lit la première cellule : "+banqueMots[0]);
                Console.WriteLine(" on lit la dernière cellule : "+banqueMots[banqueMots.Length - 1]);
                */

                //génère un nombre aléatoire entre 0 et la dernière cellule du tableau de mots à deviner
                Random rnd = new Random();
                int nombreMystereRandom = 0;
                nombreMystereRandom = rnd.Next(0, (banqueMots.Length - 1));

                //génère un mot choisi à partir du tableau de mots et du nombre aléatoire de sélection
                motMystere = banqueMots[nombreMystereRandom];

                if (isDevOn == true)
                {
                    //affichage du mot mystère (debug)
                    showInfoDev("Le mot mystère est " + motMystere);//debug
                }

                //transformation et affichage du mot mystère en mode jeu (juste le nb de lettres donc, "_ _ _ _ ")
                int motLongueur = motMystere.Length;
                if (isDevOn == true)
                {
                    showInfoDev("Le mot mystère " + motMystere + " contient " + motLongueur + " caractères."); //debug
                }
                //ENLEVER COMM motConverti = string.Concat(Enumerable.Repeat("_ ", motLongueur));
                showInfoMessage("Mot à deviner : " + motConverti);

                //affichage du nombre d'essais initial
                showInfoMessage("A chaque erreur, un essai vous sera retiré. Vous avez " + Program.nbreEssais + " essais.");
            }



            while (Program.nbreEssais >= 1 && isWin == false) //boucle pour chaque tour/essai du jeu
            {
                bool isOkInputUser = false;
                bool isAlreadyChoosen = false;
                string lettreChoisie = "";
                string underscoreMot = "_";

                while (isOkInputUser == false)
                {
                    //demande une lettre à l'utilisateur
                    showActionMessage("Entrez votre lettre ...", ref stringRecu);
                    //Console.WriteLine("Entrez votre lettre ...");
                    //string stringRecu = Console.ReadLine(); //met en pause le programme et attend un input
                    lettreChoisie = stringRecu;

                    //vérification bonne entrée utilisateur (1 seul caractère, qui est 1 lettre)
                    if (string.IsNullOrEmpty(lettreChoisie))
                    {
                        showErrorMessage("Tapez une lettre avant d'appuyer sur la touche Entrée !");
                    }

                    else if (lettreChoisie.Length > 1)
                    {
                        showErrorMessage("Entrez qu'un seul caractère, une lettre !");
                    }

                    else if (Char.IsLetter(lettreChoisie, 0) == false)
                    {
                        showErrorMessage("Entrez une lettre, pas autre chose svp (numéros et caractères spéciaux)");
                    }

                    else
                    {
                        isOkInputUser = true;
                    }
                }

                //DEBUG
                //Console.WriteLine("sortie de la boucle isOkInputUser");

                //passage en majuscule de la lettre choisie si nécessaire
                if (Char.IsLower(lettreChoisie, 0))
                {
                    lettreChoisie = lettreChoisie.ToUpper();

                    //DEBUG
                    //Console.WriteLine("Passage en majuscule : "+ lettreChoisie);
                }

                //affichage console
                showInfoMessage("Vous avez choisi : " + lettreChoisie);

                //vérification du cas où la lettre choisie a déjà été jouée
                if (lettresJouees.Contains(lettreChoisie) && lettresJouees != "")
                {
                    showInfoMessage("Vous avez déjà utilisé cette lettre, vous perdez un essai inutilement.");
                    isAlreadyChoosen = true;

                    //retrait d'une tentative
                    Program.nbreEssais--;
                }

                //comparaison de la lettre entrée et mis en majuscule, avec le mot à deviner
                bool contientRtatRecherche = motMystere.Contains(lettreChoisie);

                if (isAlreadyChoosen == false)
                {
                    if (contientRtatRecherche == true)
                    {
                        showInfoMessage("Bien joué, cette lettre faisait partie du mot.");

                        //recherche des positions des lettres trouvées sur ce tour, du mot mystère
                        foreach (Match match in Regex.Matches(motMystere, lettreChoisie))
                        {
                            //DEBUG
                            //Console.WriteLine("On trouve la lettre '{0}' à la position {1} (attention, l'index commence à 0!).", match.Value, match.Index);

                            //remplacement des lettres trouvées à ce tour dans le mot mystère à afficher
                            StringBuilder sb = new StringBuilder(motConverti);
                            sb[match.Index * 2] = Convert.ToChar(lettreChoisie);
                            motConverti = sb.ToString();
                        }
                    }

                    else
                    {
                        showInfoMessage("Dommage, le mot ne contient pas cette lettre !");

                        //retrait d'une tentative
                        Program.nbreEssais--;
                    }
                }

                //affichage du mot à deviner, reconstitué partiellement ou pas en fonction des réponses    
                showMotConverti("Mot à deviner : " + motConverti);

                if (!motConverti.Contains(underscoreMot))
                {
                    showInfoMessage("Félicitations, vous avez gagné !");
                    isWin = true;
                }

                else
                {
                    //affichage du nombre d'essais restants à l'issue du tour
                    showInfoMessage("Il vous reste : " + Program.nbreEssais + " essai(s).");

                    //affichage des lettres déjà jouées
                    if (lettresJouees != "")
                    {
                        lettresJouees = lettresJouees + ", " + lettreChoisie;
                    }

                    else
                    {
                        lettresJouees = lettreChoisie;
                    }

                    showLettresJouees("Lettres déjà jouées : " + lettresJouees);
                }

            }

            showInfoDev("sortie de la boucle des tours de cette partie de jeu"); //debug

            //demande à l'utilisateur s'il veut quitter
            showActionMessage("Voulez-vous rejouer, ou quitter ? (Tapez 1, ou 0.)", ref choixRecu);
            //Console.WriteLine("Voulez-vous rejouer, ou quitter ? (Tapez 1, ou 0.)");
            //string choixRecu = Console.ReadLine(); //met en pause le programme et attenb un input
            restart(choixRecu);
        }

        static void restart(string choixRecu)
        {
            if (choixRecu == "0")
            {
                isApplicationOn = false;
            }

            else if (choixRecu == "1")
            {
                isWin = false;
                nbreEssais = essais;
                isError = false;
                lettresJouees = "";
            }
            else
            {
                showErrorMessage("Entrez 0 ou 1, rien d'autre svp !");
                isError = true;
            }
        }

        /************* gestion des "vues" ************/

        static void showTitle()
        {
            //affiche le titre du jeu
            Console.WriteLine("****************************");
            Console.WriteLine("******* Jeu du pendu *******");
            Console.WriteLine("****************************");
        }

        static void showErrorMessage(string message)
        {
            Console.WriteLine("ERREUR : " + message);
        }

        static void showActionMessage(string message, ref string variableToSet)
        {
            Console.WriteLine(message);
            variableToSet = Console.ReadLine();
        }

        static void showInfoMessage(string message)
        {
            Console.WriteLine(message);
        }

        static void showInfoDev(string message)
        {
            Console.WriteLine(message);
        }

        static void showMotConverti(string message)
        {
            Console.WriteLine(message);
        }

        static void showLettresJouees(string message)
        {
            Console.WriteLine(message);
        }
        /********************* FIN *********************/
    }
}
