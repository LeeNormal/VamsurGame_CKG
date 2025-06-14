using UnityEngine;

public class OrbitingSword : MonoBehaviour
{
    private Transform target;
    private float rotateSpeed;
    private float damage;
    private float radius;
    private float currentAngle; 

    public void Initialize(Transform center, float speed, float dmg, float r, float initialAngle)
    {
        target = center;
        rotateSpeed = speed;
        damage = dmg;
        radius = r;
        currentAngle = initialAngle;
    }

    void Update()
    {
        if (target == null) return;

        currentAngle += rotateSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
        transform.position = target.position + offset;

        Vector3 dir = (transform.position - target.position).normalized;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 45f);
    }

    public void SetDamage(float dmg) => damage = dmg;
    public void SetSpeed(float speed) => rotateSpeed = speed;
}
