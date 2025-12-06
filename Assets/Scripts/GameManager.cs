using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public int health;
    public int ammunition;

    public TMP_Text ammunitionText;
    public TMP_Text healthText;

    void OnEnable()
    {
        EventManager.StartListening("UpdatePlayerHealth", UpdatePlayerHealth);
        EventManager.StartListening("UpdatePlayerAmmunition", UpdatePlayerAmmunition);

        UpdatePlayerHealth(3);
        UpdatePlayerAmmunition(0);
    }
    void OnDisable()
    {
        EventManager.StopListening("UpdatePlayerHealth", UpdatePlayerHealth);
        EventManager.StopListening("UpdatePlayerAmmunition", UpdatePlayerAmmunition);
    }


    void UpdatePlayerHealth(object data)
    {
        health += (int)data;
        healthText.text = "Health: " + health;

        if (health <= 0)
        {
            Debug.Log("Player died");
            EventManager.TriggerEvent("PlayerDied");
        }
    }

    void UpdatePlayerAmmunition(object data)
    {
        ammunition += (int)data;
        ammunitionText.text = "Ammo: " + ammunition;
    }

}
