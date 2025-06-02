using UnityEngine;

public class InfiniteMapManager : MonoBehaviour
{
    // 플레이어 Transform - 플레이어 위치를 추적하기 위해 필요
    public Transform player;

    // 타일 프리팹 - 맵을 구성하는 기본 단위 (예: 잔디 타일 등)
    public GameObject tilePrefab;

    // 3x3 그리드 크기 설정
    public int gridSize = 3;

    // 타일 하나의 크기 (유니티 월드 유닛 기준)
    public float tileSize = 10f;

    // 현재 활성화된 타일들을 담는 2차원 배열
    private GameObject[,] tiles;

    // 현재 중심 타일의 배열 인덱스 (항상 (1,1)을 중심으로 설정)
    private Vector2Int currentCenterIndex;

    void Start()
    {
        // 타일 배열 초기화
        tiles = new GameObject[gridSize, gridSize];

        // 3x3 타일을 초기 위치에 생성
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // (1,1) 위치를 중심으로 타일 배치
                Vector3 pos = new Vector3((x - 1) * tileSize, (y - 1) * tileSize, 0f);
                tiles[x, y] = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }

        // 중심 타일 인덱스 초기화 (3x3 기준 중앙은 (1,1))
        currentCenterIndex = new Vector2Int(1, 1);
    }

    void Update()
    {
        Vector3 playerPos = player.position;

        // 현재 중심 타일의 월드 좌표를 가져옴
        Vector3 centerTilePos = tiles[currentCenterIndex.x, currentCenterIndex.y].transform.position;

        // 플레이어가 중심 타일로부터 반 타일 이상 이동했는지 체크
        if (Vector3.Distance(playerPos, centerTilePos) > tileSize * 0.5f)
        {
            // 이동 거리 초과 시 타일 재배치
            RecenterTiles();
        }
    }

    void RecenterTiles()
    {
        Vector3 playerPos = player.position;

        // 플레이어 위치를 기준으로 새로운 중심 타일 좌표 계산
        int centerX = Mathf.RoundToInt(playerPos.x / tileSize);
        int centerY = Mathf.RoundToInt(playerPos.y / tileSize);

        // 3x3 그리드 내 모든 타일을 재배치
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                int tileX = centerX + x - 1; // (중앙을 기준으로 -1 ~ +1 이동)
                int tileY = centerY + y - 1;

                // 새 위치 계산 및 적용
                Vector3 newPos = new Vector3(tileX * tileSize, tileY * tileSize, 0f);
                tiles[x, y].transform.position = newPos;
            }
        }

        // 중심 인덱스는 항상 1,1로 유지
        currentCenterIndex = new Vector2Int(1, 1);
    }
}
