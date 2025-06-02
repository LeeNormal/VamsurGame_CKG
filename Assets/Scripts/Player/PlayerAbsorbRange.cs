using UnityEngine;

public class PlayerAbsorbRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExpOrb"))
        {
            ExpOrb orb = other.GetComponent<ExpOrb>();
            if (orb != null)
            {
                orb.AttractToPlayer(transform.parent); // 플레이어 Transform 전달
            }
        }
    }
}
