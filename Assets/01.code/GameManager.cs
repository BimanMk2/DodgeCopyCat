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

    // UI Text ������Ʈ��
    
    public Text ClearText;
    public Text boxCounterText;

    private bool isGameover;
    private int collectedBoxes = 0;
    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        isGameover = false;

        // ���� ���� ��, ���� ����� Ŭ���� UI�� ����ϴ�.
        
        ClearText.gameObject.SetActive(false);

        // ��� �ڽ��� �����մϴ�.
        for (int i = 0; i < totalBoxesToCollect; i++)
        {
            SpawnBox();
        }

        // ���� ���� �� �ڽ� ī���� UI�� �ʱ�ȭ�մϴ�.
        UpdateBoxCounterText();
    }

    // �ڽ��� ������ ��ġ�� �����ϴ� �Լ�
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

    // �÷��̾ �ڽ��� �Ծ��� ��, Box ��ũ��Ʈ���� �� �Լ��� ȣ���մϴ�.
    public void CollectBox()
    {
        collectedBoxes++;
        UpdateBoxCounterText();

        if (collectedBoxes >= totalBoxesToCollect)
        {
            // ���� Ŭ���� ����
            ClearText.gameObject.SetActive(true);
            isGameover = true;

            // ��� �� ������Ʈ�� ã�� �ı��ϴ� �Լ� ȣ��
            DestroyAllEnemies();

        }
    }

    // ���� �ִ� ��� �� ������Ʈ�� �ı��ϴ� �Լ�
    private void DestroyAllEnemies()
    {
        // "Enemy" �±׸� ���� ��� ������Ʈ�� ã���ϴ�.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // ã�� ��� �� ������Ʈ�� ��ȸ�ϸ� �ı��մϴ�.
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    // �ڽ� ������ UI �ؽ�Ʈ�� ǥ���ϴ� �Լ�
    private void UpdateBoxCounterText()
    {
        if (boxCounterText != null)
        {
            // 'boxCounterText.text'�� ���ڸ� �Ҵ�
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

    // �ٸ� ��ũ��Ʈ(��: ���ΰ� �Ǵ� ��)���� ���� ���� �� �� �Լ��� ȣ���մϴ�.
    public void EndGame()
    {
        isGameover = true;
        // ���� ���� �� GameOverScene���� �̵�
        SceneManager.LoadScene("GameOverScene");

        // �ʿ��ϴٸ�, ���⿡ ������ ���߰ų� ���� �ٽ� �ε��ϴ� �ڵ带 �߰��� �� �ֽ��ϴ�.
    }
}