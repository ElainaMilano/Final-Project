using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health;
    public int ammunition;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D polygonCollider2D;

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
            EventManager.TriggerEvent("IncreasePlayerHealth", health);
        }
        else if (ammunition > 0)
        {
            EventManager.TriggerEvent("IncreasePlayerAmmunition", ammunition);
        }
        Disable();
        // once consumed, reappear after 1 minute 
        Invoke("Enable", 10);
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
