using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                  // 따라갈 대상
    public float followSpeed = 5f;            // 카메라 최대 따라가는 속도
    public Vector3 offset = new Vector3(0, 0, -10); // Z축 고정

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
        float distance = Vector3.Distance(transform.position, targetPos);

        // 거리에 따라 속도 보정
        float speed = Mathf.Max(followSpeed, distance * 5f); // 멀면 빠르게, 가까우면 followSpeed

        // 일정 속도로 따라가기 (프레임 독립적)
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
    }
}
