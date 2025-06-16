using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("설정")]
    public float _charMS = 5f;
    public Transform visual; // Visual 자식 오브젝트 연결

    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _inputDir;
    public bool isAction = false;
    public bool canMove = true;

    private float _lastMoveX = 1f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (visual == null)
        {
            Debug.LogError("Visual 객체가 연결되지 않았습니다.");
            return;
        }

        _anim = visual.GetComponent<Animator>();
        _spriteRenderer = visual.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        WeaponManager weaponManager = GetComponentInChildren<WeaponManager>();

        if (weaponManager != null && weaponManager.CanAddWeapon())
        {
            GameObject defaultWeapon = weaponManager.availableWeaponPrefabs[0];
            if (defaultWeapon != null)
            {
                weaponManager.AddWeapon(defaultWeapon);
                Debug.Log($"{defaultWeapon.name} 기본 무기 장착 완료!");
            }
        }
    }

    void Update()
    {
        if (isAction || !canMove) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _inputDir = new Vector2(h, v).normalized;

        UpdateAnimationAndDirection(h, v);
    }

    void FixedUpdate()
    {
        if (isAction || _inputDir == Vector2.zero)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _rb.MovePosition(_rb.position + _inputDir * (_charMS * Time.fixedDeltaTime));
    }

    void UpdateAnimationAndDirection(float horizontal, float vertical)
    {
        bool isMovingHorizontally = Mathf.Abs(horizontal) > 0.1f;
        bool isMovingVertically = Mathf.Abs(vertical) > 0.1f;

        if (isMovingHorizontally)
        {
            _anim.SetFloat("Horizontal", horizontal);
            _spriteRenderer.flipX = false;  // 이동 중엔 반전 안함
            _lastMoveX = horizontal;
        }
        else if (isMovingVertically)
        {
            _anim.SetFloat("Horizontal", _lastMoveX);
            _spriteRenderer.flipX = false;
        }
        else
        {
            _anim.SetFloat("Horizontal", 0f);
            _spriteRenderer.flipX = _lastMoveX < 0;
        }
    }

    public Vector2 GetInputDirection()
    {
        return _inputDir;
    }
}
