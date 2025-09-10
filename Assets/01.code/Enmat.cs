using UnityEngine;
using System.Collections; // 코루틴 사용을 위한 네임스페이스 추가

public class EnemyController : MonoBehaviour
{
    public Transform playerTarget;
    public float baseForce = 150f;
    public float maxForce = 300f;
    public float decelerationRate = 0.5f;
    public float forceIncreaseRate = 20f;

    // 새로 추가된 변수들
    public float directionChangeInterval = 1.0f; // 방향을 바꾸는 주기 (초)
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

        // 코루틴 시작: 주기적으로 타겟 방향을 갱신합니다.
        StartCoroutine(UpdateTargetDirection());
    }

    void FixedUpdate()
    {
        if (playerTarget == null)
        {
            return;
        }

        // 이제 주인공 방향 대신, currentTargetDirection 변수를 사용합니다.
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

    // 일정 시간마다 목표 방향을 갱신하는 코루틴
    IEnumerator UpdateTargetDirection()
    {
        while (true)
        {
            if (playerTarget != null)
            {
                // 주인공을 향하는 새로운 방향을 설정합니다.
                currentTargetDirection = (playerTarget.position - transform.position).normalized;
            }
            // directionChangeInterval초 동안 기다립니다.
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