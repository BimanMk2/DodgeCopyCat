using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boxPrefab;
    public int totalBoxesToCollect = 5;
    public GameObject plane;

    // UI Text 컴포넌트들
    
    public Text ClearText;
    public Text boxCounterText;

    private bool isGameover;
    private int collectedBoxes = 0;
    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        isGameover = false;

        // 게임 시작 시, 게임 종료와 클리어 UI는 숨깁니다.
        
        ClearText.gameObject.SetActive(false);

        // 모든 박스를 생성합니다.
        for (int i = 0; i < totalBoxesToCollect; i++)
        {
            SpawnBox();
        }

        // 게임 시작 시 박스 카운터 UI를 초기화합니다.
        UpdateBoxCounterText();
    }

    // 박스를 랜덤한 위치에 생성하는 함수
    void SpawnBox()
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            return;
        }

        Vector3 spawnPosition;
        bool positionFound = false;

        while (!positionFound)
        {
            float x = Random.Range(-planeRenderer.bounds.extents.x, planeRenderer.bounds.extents.x);
            float z = Random.Range(-planeRenderer.bounds.extents.z, planeRenderer.bounds.extents.z);
            spawnPosition = new Vector3(x, 0.5f, z);

            bool isOverlapping = false;
            foreach (Vector3 pos in usedPositions)
            {
                if (Vector3.Distance(pos, spawnPosition) < 2f)
                {
                    isOverlapping = true;
                    break;
                }
            }

            if (!isOverlapping)
            {
                usedPositions.Add(spawnPosition);
                Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
                positionFound = true;
            }
        }
    }

    // 플레이어가 박스를 먹었을 때, Box 스크립트에서 이 함수를 호출합니다.
    public void CollectBox()
    {
        collectedBoxes++;
        UpdateBoxCounterText();

        if (collectedBoxes >= totalBoxesToCollect)
        {
            // 게임 클리어 로직
            ClearText.gameObject.SetActive(true);
            isGameover = true;

            // 모든 적 오브젝트를 찾아 파괴하는 함수 호출
            DestroyAllEnemies();

        }
    }

    // 씬에 있는 모든 적 오브젝트를 파괴하는 함수
    private void DestroyAllEnemies()
    {
        // "Enemy" 태그를 가진 모든 오브젝트를 찾습니다.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 찾은 모든 적 오브젝트를 순회하며 파괴합니다.
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    // 박스 개수를 UI 텍스트에 표시하는 함수
    private void UpdateBoxCounterText()
    {
        if (boxCounterText != null)
        {
            // 'boxCounterText.text'에 숫자만 할당
            boxCounterText.text = $"{collectedBoxes} / {totalBoxesToCollect}";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");

        }

    }

    // 다른 스크립트(예: 주인공 또는 적)에서 게임 오버 시 이 함수를 호출합니다.
    public void EndGame()
    {
        isGameover = true;
        // 게임 오버 시 GameOverScene으로 이동
        SceneManager.LoadScene("GameOverScene");

        // 필요하다면, 여기에 게임을 멈추거나 씬을 다시 로드하는 코드를 추가할 수 있습니다.
    }
}