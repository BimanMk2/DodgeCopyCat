using UnityEngine;
using UnityEngine.SceneManagement;


public class ReplayGameMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        // 'R' Ű�� ���ȴ��� Ȯ���մϴ�.
        if (Input.GetKeyDown(KeyCode.R))
        {
            // ���� Ȱ��ȭ�� ���� �̸��� �����ɴϴ�.
            Scene currentScene = SceneManager.GetActiveScene();

            // ���� ���� �ٽ� �ε��Ͽ� ������ ������մϴ�.
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
