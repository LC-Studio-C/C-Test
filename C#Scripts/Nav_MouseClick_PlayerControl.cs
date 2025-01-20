using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//
// 摘要:这个脚本实现导航系统下通过点击地面控制角色在可通过区域移动.
//      只是学习过程中的练习,不是最好的控制方式
//
public class Nav_MouseClick_PlayerControl : MonoBehaviour
{
    //角色导航网格代理字段,负责引用角色的导航网格代理组件
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        //引用角色的导航网格代理组件
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveControl();
    }

    private void MoveControl()
    {
        //从鼠标指向的位置发出一条射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //保存射线碰撞信息
        RaycastHit hit;
        //按下鼠标左键
        if (Input.GetButtonDown("Fire1"))
        {
            //如果射出的射线碰撞到有碰撞箱对象
            if (Physics.Raycast(ray, out hit))
            {
                //设置导航目的地为碰撞点
                navMeshAgent.SetDestination(hit.point);    
            }
        }
    }
}
