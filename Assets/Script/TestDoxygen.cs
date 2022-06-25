using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief namespace ������ ����
 * @details namespace �ڼ��� ����
 */
namespace TestNamespace
{
    /**
     * @brief class ������ ����
     * @details class �ڼ��� ����
     * @author writer name(�ڵ� �ۼ��� �̸�)
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
         * @brief �޼��� ���� ����
         * @details �޼��� �� ����
         * @param[in] a <- �Ű����� 1
         * @param[in] b <- �Ű����� 2
         * @param[out] c <- ���� �����͵��� ���޵ǰų� ��ȯ���� ������ ǥ��
         * @return void <- �Լ��� void�ϱ�
         * @exception ����ó���� ����(�� �Լ����� ���ܰ� �����ϴ�.)
         * @throws throws�ϴ� ��ü�� �������� ����
         * 
         * @note ���� ����
         * @todo �� �� ����
         * @pre �̸� ȣ���ؾ� �� ����
         * @bug ���� ����
         * @warning ���� ���� �ۼ�
         * @see ������ �Լ� �Ǵ� ������ (�������� �Լ� �̸��� �ۼ��ϸ� �ڵ� ��ũ)
         * 
         * @code �߿��ڵ带 �����Ҷ� ���� ������ ����ŵ�ϴ�.
         * @endcode �߿��ڵ带 �����Ҷ� ���� ������ ����ŵ�ϴ�.
         * 
         */
        public void TestFunc(int a, int b)
        {
            Debug.Log("test doxegen");
        }
    }
}