using UnityEngine;

public class Box : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �±װ� "Player"���� Ȯ���մϴ�.
        if (other.gameObject.CompareTag("Player"))
        {
            // GameManager�� ã�� Box ȹ���� �˸��ϴ�.
            GameManager gameManager = Object.FindAnyObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.CollectBox();
            }

            // �� Box ������Ʈ�� �ı��մϴ�.
            Destroy(gameObject);
        }
    }
}