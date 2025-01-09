using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Transform player; // 플레이어를 드래그 앤 드롭으로 연결
    private NavMeshAgent agent;

    [Header("Monster Settings")]
    public float speed = 3.5f; // 괴물의 속도 조절 변수
    public float stoppingDistance = 0.5f; // 플레이어와의 최소 거리

    [Header("Audio Settings")]
    public AudioClip footstepSound; // 발자국 소리
    public AudioClip attackSound;   // 공격 소리
    public float footstepInterval = 0.5f; // 발자국 소리 간격 (초)
    private AudioSource audioSource;
    private float footstepTimer = 0f;

    void Start()
    {
        // NavMeshAgent 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player is not assigned! Please drag and drop the player object in the Inspector.");
        }

        // 초기 속도와 스탑핑 거리 설정
        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;

        // AudioSource 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어 위치를 목적지로 설정
            agent.SetDestination(player.position);

            // 발자국 소리 재생
            if (IsMoving())
            {
                footstepTimer += Time.deltaTime;
                if (footstepTimer >= footstepInterval)
                {
                    PlayFootstepSound();
                    footstepTimer = 0f;
                }
            }
        }
    }

    private bool IsMoving()
    {
        // NavMeshAgent의 속도로 이동 여부 확인
        return agent.velocity.magnitude > 0.1f;
    }

    private void PlayFootstepSound()
    {
        if (footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌 시 공격 소리 재생
        if (other.transform == player)
        {
            Debug.Log("Monster has caught the player!");
            PlayAttackSound();
        }
    }
}
