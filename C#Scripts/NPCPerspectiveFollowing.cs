using UnityEngine;

public class NPCPerspectiveFollowing : MonoBehaviour
{
    [Header("目标设置")]
    [Tooltip("自动设置玩家为目标")]
    public Transform targer;
    public string PlayerTag = "Player";
    [Header("旋转设置")]
    public float rotationSpeed = 5f;
    public bool lockVerticalRotation = true;
    public float detectionRadius = 5f;
    public float returnRotationSpeed = 3f;
    private Quaternion originRotation;
    private bool isPlayerInRange;
    [Header("滞后阈值")]
    public float hysteresis = 0.5f;
    void Start()
    {
        originRotation = transform.rotation;

        if (targer == null)
        {
            GameObject player = GameObject.FindWithTag(PlayerTag);
            if (player != null)
            {
                targer = player.transform;
            }
            else
            {
                Debug.Log("无目标");
            }
        }
    }

    void Update()
    {
        if (targer != null)
        {
            float distance = Vector3.Distance(targer.position, transform.position);
            if (distance < detectionRadius - hysteresis)
            {
                isPlayerInRange = true;
            }
            else if (distance > detectionRadius + hysteresis)
            {
                isPlayerInRange = false;
            }

            if (isPlayerInRange)
            {
                LookAtPlayer();
            }
            else
            {
                ReturnOriginRotation();
            }
        }
    }

    public void LookAtPlayer()
    {
        Vector3 direction = targer.position - transform.position;

        if (lockVerticalRotation)
        {
            direction.y = 0;
        }

        Quaternion targerRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.deltaTime);
    }

    public void ReturnOriginRotation()
    {
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            originRotation,
            returnRotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,detectionRadius);

        Gizmos.color = isPlayerInRange ? Color.blue : Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
