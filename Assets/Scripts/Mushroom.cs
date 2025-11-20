using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health;
    public int ammunition;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D polygonCollider2D;
    public int secondsUntilRespawn = 10;

    void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (polygonCollider2D == null)
            polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void Eat()
    {
        if (health > 0)
        {
            EventManager.TriggerEvent("UpdatePlayerHealth", health);
        }
        else if (ammunition > 0)
        {
            EventManager.TriggerEvent("UpdatePlayerAmmunition", ammunition);
        }
        Disable();
        // once consumed, enable after n seconds 
        Invoke("Enable", secondsUntilRespawn);
    }

    void Enable()
    {
        spriteRenderer.enabled = true;
        polygonCollider2D.enabled = true;
    }
    void Disable()
    {
        spriteRenderer.enabled = false;
        polygonCollider2D.enabled = false;
    }




}
