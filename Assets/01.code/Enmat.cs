using UnityEngine;
using System.Collections; // �ڷ�ƾ ����� ���� ���ӽ����̽� �߰�

public class EnemyController : MonoBehaviour
{
    public Transform playerTarget;
    public float baseForce = 150f;
    public float maxForce = 300f;
    public float decelerationRate = 0.5f;
    public float forceIncreaseRate = 20f;

    // ���� �߰��� ������
    public float directionChangeInterval = 1.0f; // ������ �ٲٴ� �ֱ� (��)
    private Vector3 currentTargetDirection;

    private Rigidbody rb;
    private float currentForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the enemy object.");
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with 'Player' tag not found!");
        }

        currentForce = baseForce;

        // �ڷ�ƾ ����: �ֱ������� Ÿ�� ������ �����մϴ�.
        StartCoroutine(UpdateTargetDirection());
    }

    void FixedUpdate()
    {
        if (playerTarget == null)
        {
            return;
        }

        // ���� ���ΰ� ���� ���, currentTargetDirection ������ ����մϴ�.
        if (Vector3.Dot(rb.linearVelocity.normalized, currentTargetDirection) < 0.5f)
        {
            currentForce += forceIncreaseRate * Time.fixedDeltaTime;
            if (currentForce > maxForce)
            {
                currentForce = maxForce;
            }
        }
        else
        {
            currentForce = Mathf.Lerp(currentForce, baseForce, decelerationRate * Time.fixedDeltaTime);
        }

        rb.AddForce(currentTargetDirection * currentForce);

        if (rb.linearVelocity.magnitude > 8f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * 8f;
        }
    }

    // ���� �ð����� ��ǥ ������ �����ϴ� �ڷ�ƾ
    IEnumerator UpdateTargetDirection()
    {
        while (true)
        {
            if (playerTarget != null)
            {
                // ���ΰ��� ���ϴ� ���ο� ������ �����մϴ�.
                currentTargetDirection = (playerTarget.position - transform.position).normalized;
            }
            // directionChangeInterval�� ���� ��ٸ��ϴ�.
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.Die();
            }
        }
    }
}