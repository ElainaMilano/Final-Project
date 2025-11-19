using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text ammunitionText;
    public TMP_Text healthText;

    void OnEnable()
    {
        EventManager.StartListening("IncreasePlayerHealth", IncreasePlayerHealth);
        EventManager.StartListening("IncreasePlayerAmmunition", IncreasePlayerAmmunition);
    }
    void OnDisable()
    {
        EventManager.StopListening("IncreasePlayerHealth", IncreasePlayerHealth);
        EventManager.StopListening("IncreasePlayerAmmunition", IncreasePlayerAmmunition);
    }


    void IncreasePlayerHealth(object data)
    {
        healthText.text = healthText.text + (int)data;
    }

    void IncreasePlayerAmmunition(object data)
    {
        ammunitionText.text = ammunitionText.text + (int)data;
    }

}
