using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image fillImage;
    [HideInInspector]
    public PlayerData playerData;  // 외부에서 연결됨
    public Vector3 offset = new Vector3(0, 0.7f, 0); // 플레이어 머리 위

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;

        // playerData가 이미 연결돼 있으면 즉시 구독
        if (playerData != null)
        {
            Bind(playerData);
        }
    }

    //private void LateUpdate()
    //{
    //    if (playerData != null)
    //    {
    //        transform.position = playerData.transform.position + offset;
    //        transform.forward = mainCam.transform.forward; // 카메라 고정
    //    }
    //}

    public void UpdateBar(float fillAmount)
    {
        if (fillImage != null)
            fillImage.fillAmount = fillAmount;
    }

    public void Bind(PlayerData data)
    {
        playerData = data;
        playerData.OnHpChanged += UpdateBar;
        UpdateBar(playerData.currentHp / playerData.maxHp);
    }
}
