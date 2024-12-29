using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidenitemTrigger : MonoBehaviour
{
    [SerializeField] public GameObject hiddenobject1;  // 苹果对象的引用
    [SerializeField] public GameObject hiddenobject2;
    [SerializeField] public GameObject hiddenobject3;
    [SerializeField] public GameObject hiddenobject4;

    private void Start()
    {
        // 初始时苹果对象不可见
        if (hiddenobject1 != null && hiddenobject2 != null && hiddenobject3 != null && hiddenobject4 != null)
        {
            hiddenobject1.SetActive(false);  // 隐藏苹果
            hiddenobject2.SetActive(false);
            hiddenobject3.SetActive(false);
            hiddenobject4.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判断是否是玩家碰到触发器
        if (other.CompareTag("Player"))
        {
            if (hiddenobject1 != null && hiddenobject2 != null && hiddenobject3 != null && hiddenobject4 != null)
            {
                hiddenobject1.SetActive(true);  // 显示苹果
                hiddenobject2.SetActive(true);
                hiddenobject3.SetActive(true);
                hiddenobject4.SetActive(true);
            }
        }
    }
}
