using UnityEngine;
using UnityEngine.SceneManagement;


public class ReplayGameMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        // 'R' 키가 눌렸는지 확인합니다.
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 현재 활성화된 씬의 이름을 가져옵니다.
            Scene currentScene = SceneManager.GetActiveScene();

            // 현재 씬을 다시 로드하여 게임을 재시작합니다.
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
