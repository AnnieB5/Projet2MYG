using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IHMController : MonoBehaviour
{
    [SerializeField] private TMP_Text motConvertiText;
    public TMP_Text debugInfo;
    [SerializeField] private TMP_Text msgActionText;
    [SerializeField] private GameObject msgErreurPanneau;
    [SerializeField] private TMP_Text msgErreurText;
    [SerializeField] private TMP_Text msgInfoText;
    [SerializeField] private GameObject[] penduTableauImages;
    public EventSystem eventSystem;
    [HideInInspector] public GameObject letterGOSelected;
    private Button letterButtonSelected;
    private GameObject rondImage;
    private GameObject croixImage;
    private GameController game; //Objet game pour gérer le traitement

    private bool debugOn = false;
    [HideInInspector] public int currentSprite;

    //Use for initialization -------------------------------------------------
    void Start()
    {
        //ajout pour Unity IHM
        game = GetComponent<GameController>();

        msgErreurPanneau.SetActive(false);
    }


    //Update is called once per frame ----------------------------------------
    void Update()
    {
        //only in debug mode
        if(debugOn){
            if(Input.GetKeyDown(KeyCode.E)){
                ShowErrorMessage("ERROR : ceci est un message de dev");
            }
        }
    }



    //methods needed from GameController -------------------------------------
    
    //change l'image du pendu et affiche celui de l'étape suivante
    public void ChangeImagePendu()
    {
        penduTableauImages[currentSprite].SetActive(false);
        currentSprite = currentSprite + 1;
        penduTableauImages[currentSprite].SetActive(true);
    }

    //initialise et réinitialisatise l'illustration du pendu
    public void ResetSpritePendu()
    {
        if(currentSprite != 0)
        {
            penduTableauImages[currentSprite].SetActive(false);
        }
        currentSprite = 0;
        penduTableauImages[currentSprite].SetActive(true);
    }

    //affiche un message d'erreur dans le panneau dédié (pour debug)
    public void ShowErrorMessage(string message)
    {
        msgErreurPanneau.SetActive(true);
        msgErreurText.text = message;
        StartCoroutine(WaitAndClose(msgErreurPanneau));
    }

    //attend un moment défini avant de fermer le panneau affichant les erreurs
    IEnumerator WaitAndClose(GameObject panelToClose)
    {
        yield return new WaitForSeconds(2f);
        panelToClose.SetActive(false);
    }

    //affiche un message d'information destiné au joueur
    public void ShowInfoMessage(string message)
    {
        msgInfoText.text = message; 
    }

    //affiche un message invitant à une action du joueur
    public void ShowActionMessage(string message)
    {
        msgActionText.text = message;
    }

    //affiche des debugs et des infos réservées aux développeurs
    public void ShowInfoDev(bool etat, string message)
    {
        debugInfo.gameObject.SetActive(etat);
        debugInfo.text = message;
    }

    //affiche le mot à deviner, en fonction des lettres trouvées
    public void ShowMotConverti(string message)
    {
        motConvertiText.text = message;
    }

    //affiche le calque "RondImage" du bouton dernièrement cliqué
    public void ShowRondImage()
    {
        rondImage = letterGOSelected.transform.Find("RondImage").gameObject;
        rondImage.SetActive(true);
    }

    //affiche le calque "CroixImage" du bouton dernièrement cliqué
    public void ShowCroixImage()
    {
        croixImage = letterGOSelected.transform.Find("CroixImage").gameObject;
        croixImage.SetActive(true);
    }


    //methods called by IHM --------------------------------------------------

    //appelé par le bouton Quitter, quitte le jeu
    public void QuitGame()
    {
        Application.Quit();
    }


    //appelé par le bouton Rejouer, relance le jeu
    public void RestartGame()
    {
        game.Restart();
    }

    //called by alphabet buttons, désactive le bouton et traite la lettre choisie
    public void OnClickTesterLettre(string letter)
    {
        //Debug.Log("Lettre affichée: " + letter);
        letterGOSelected = eventSystem.currentSelectedGameObject;
        letterButtonSelected = letterGOSelected.GetComponent<Button>();
        letterButtonSelected.interactable = false;
        game.CheckUserEnter(letter);  
    }

}


