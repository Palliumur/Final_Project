using UnityEngine;

public class Apple : MonoBehaviour
{
    private PlayerRespawn playerRespawn;
    private void Start()
    {
        playerRespawn = PlayerRespawn.instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRespawn.hasEatenApple = true;
            playerRespawn.StartAppleTimer();
            Destroy(gameObject);
        }
    }
}
