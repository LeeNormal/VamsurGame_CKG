using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    private Transform target; // 플레이어 Transform
    private PlayerExpManager playerExpManager; // 플레이어 경험치 매니저
    public float moveSpeed = 5f;
    private bool isAttracted = false;

    private void Update()
    {
        if (isAttracted && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    public void AttractToPlayer(Transform player)
    {
        target = player;
        isAttracted = true;
        playerExpManager = player.GetComponent<PlayerExpManager>(); // 처음 한 번만 찾기!
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerExpManager != null)
        {
            playerExpManager.GainExp(50);
            Destroy(gameObject);
        }
    }
}
