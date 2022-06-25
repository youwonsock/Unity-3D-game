using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief namespace 간략한 설명
 * @details namespace 자세한 설명
 */
namespace TestNamespace
{
    /**
     * @brief class 간략한 설명
     * @details class 자세한 설명
     * @author writer name(코드 작성자 이름)
     * @date 2022-06-24
     * @version 1.0
     */
    public class TestDoxygen : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /**
         * @brief 메서드 간략 설명
         * @details 메서드 상세 설명
         * @param[in] a <- 매개변수 1
         * @param[in] b <- 매개변수 2
         * @param[out] c <- 만약 포인터등이 전달되거나 반환값이 있으면 표시
         * @return void <- 함수가 void니까
         * @exception 예외처리를 설명(이 함수에는 예외가 없습니다.)
         * @throws throws하는 객체나 변수등을 설명
         * 
         * @note 참고 설명
         * @todo 할 일 설명
         * @pre 미리 호출해야 할 사항
         * @bug 버그 설명
         * @warning 주의 사항 작성
         * @see 참고할 함수 또는 페이지 (페이지나 함수 이름을 작성하면 자동 링크)
         * 
         * @code 중요코드를 설명할때 시작 지점을 가리킵니다.
         * @endcode 중요코드를 설명할때 종료 지점을 가리킵니다.
         * 
         */
        public void TestFunc(int a, int b)
        {
            Debug.Log("test doxegen");
        }
    }
}