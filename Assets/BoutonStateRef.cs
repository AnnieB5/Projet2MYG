using UnityEngine;
using UnityEngine.UI;

public class BoutonStateRef : MonoBehaviour
{
    [SerializeField] private GameObject croixImage;
    [SerializeField] private GameObject rondImage;
    [SerializeField] private Button button;
    
    void OnEnable()
    {
        GameController.ResetButtonState += ResetButtons; 
    }

    void OnDisable()
    {
        GameController.ResetButtonState -= ResetButtons;
    }
    public void ResetButtons() //réinitialise tous les boutons
    {
        croixImage.SetActive(false);
        rondImage.SetActive(false);
        button.interactable = true;
        //Debug.Log("ResetButtonsDeux");
    }
}
