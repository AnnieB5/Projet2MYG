using UnityEngine;
using UnityEngine.UI;

public class BoutonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource bonneReponse;
    [SerializeField] private AudioSource mauvaiseReponse;
    

    //joue le son "BonneReponse" suite au bouton dernièrement cliqué
    public void PlayBonneReponse()
    {
        bonneReponse.Play();
    }

    //joue le son "MauvaiseReponse" suite au bouton dernièrement cliqué
    public void PlayMauvaiseReponse()
    {
        mauvaiseReponse.Play();
    }
}
