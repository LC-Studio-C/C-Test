using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//
// ժҪ:����ű�ʵ�ֵ���ϵͳ��ͨ�����������ƽ�ɫ�ڿ�ͨ�������ƶ�.
//      ֻ��ѧϰ�����е���ϰ,������õĿ��Ʒ�ʽ
//
public class Nav_MouseClick_PlayerControl : MonoBehaviour
{
    //��ɫ������������ֶ�,�������ý�ɫ�ĵ�������������
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        //���ý�ɫ�ĵ�������������
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveControl();
    }

    private void MoveControl()
    {
        //�����ָ���λ�÷���һ������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //����������ײ��Ϣ
        RaycastHit hit;
        //����������
        if (Input.GetButtonDown("Fire1"))
        {
            //��������������ײ������ײ�����
            if (Physics.Raycast(ray, out hit))
            {
                //���õ���Ŀ�ĵ�Ϊ��ײ��
                navMeshAgent.SetDestination(hit.point);    
            }
        }
    }
}
