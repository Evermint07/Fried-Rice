using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCollider : MonoBehaviour
{
    public GameObject frogObject; // Unity Inspector에서 설정
    private Skeleton frog;            // Frog 스크립트 참조
    private Animator frogAnimator; // Animator 컴포넌트 참조

    // Start is called before the first frame update
    void Start()
    {
        // frogObject가 null인지 확인
        if (frogObject == null)
        {
            Debug.LogError("frogObject가 Unity Inspector에서 설정되지 않았습니다!");
            return;
        }

        // Frog 컴포넌트를 가져오기
        frog = frogObject.GetComponent<Skeleton>();
        if (frog == null)
        {
            Debug.LogError("frogObject에 Frog 컴포넌트가 없습니다!");
        }

        // Animator 컴포넌트를 가져오기
        frogAnimator = frogObject.GetComponent<Animator>();
        if (frogAnimator == null)
        {
            Debug.LogError("frogObject에 Animator 컴포넌트가 없습니다!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 필요 시 추가 로직 작성
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // frog 또는 frogAnimator가 null이면 실행 중단
        if (frog == null || frogAnimator == null)
        {
            Debug.LogWarning("frog 또는 frogAnimator가 null 상태입니다.");
            return;
        }

        // 충돌한 객체가 "Ground" 태그를 가진 경우
        if (other.gameObject.tag == "Ground")
        {
            // Frog 상태 변경
            frog.current2_state = true;

            // Animator 상태 변경
            frogAnimator.SetBool("isJumping", false);

            // Debug 메시지 출력 (필요 시 활성화)
            // Debug.Log("Collision detected with Ground!");
        }
    }

}