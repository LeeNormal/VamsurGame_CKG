using UnityEngine;
using UnityEngine.UI; // 경험치 UI를 위한 Image 사용

public class PlayerExpManager : MonoBehaviour
{
    public int currentLevel = 1; // 시작 레벨
    public int currentExp = 0;        // 현재 누적 경험치
    public int maxExp = 40;           // 레벨업에 필요한 최대 경험치
    public Image expFillImage;         // 경험치 바 UI 이미지
    private float displayedExpRatio = 0f; // 부드럽게 표현할 현재 경험치 비율

    private void Update()
    {
        // 목표 경험치 비율 계산
        float targetRatio = (float)currentExp / maxExp;

        // UI를 부드럽게 채우기 위해 Lerp로 점진적으로 따라가게 함 (unscaledDeltaTime 사용)
        displayedExpRatio = Mathf.Lerp(displayedExpRatio, targetRatio, Time.unscaledDeltaTime * 5f);

        // 실제 경험치 바 채우기
        if (expFillImage != null)
        {
            expFillImage.fillAmount = displayedExpRatio;
        }
    }

    private void Start()
    {
        UpdateExpUI(); // 시작할 때 경험치 UI 초기화
    }

    // 경험치 획득 함수
    public void GainExp(int amount)
    {
        currentExp += amount;


        // 현재 경험치가 최대치 이상이면 레벨업 처리
        if (currentExp >= maxExp)
        {   
            currentExp = maxExp; // 일단 최대치를 넘지 않게 고정
            LevelUp();
        }

        UpdateExpUI(); // UI 갱신
    }

    // 경험치 UI를 즉시 갱신
    private void UpdateExpUI()
    {
        if (expFillImage != null)
        {
            expFillImage.fillAmount = (float)currentExp / maxExp;
        }
    }

    // 레벨업 처리
    private void LevelUp()
    {
        Debug.Log("레벨업!");

        currentExp = 0;
        currentLevel++;

        // 다음 레벨업까지 필요한 경험치 증가 공식
        maxExp = Mathf.FloorToInt(40 + 25f * Mathf.Pow(currentLevel - 1, 1.5f));
        Debug.Log("MaxExp : " + maxExp);

        UpdateExpUI();

        // 레벨업 UI 오픈
        FindObjectOfType<LevelUpUIManager>().OpenLevelUpUI();
    }
}
