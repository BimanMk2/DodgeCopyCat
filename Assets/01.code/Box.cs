using UnityEngine;

public class Box : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체의 태그가 "Player"인지 확인합니다.
        if (other.gameObject.CompareTag("Player"))
        {
            // GameManager를 찾아 Box 획득을 알립니다.
            GameManager gameManager = Object.FindAnyObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.CollectBox();
            }

            // 이 Box 오브젝트를 파괴합니다.
            Destroy(gameObject);
        }
    }
}