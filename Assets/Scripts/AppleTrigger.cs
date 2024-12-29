using UnityEngine;

public class AppleTrigger : MonoBehaviour
{
    [SerializeField] public GameObject apple;  // 苹果对象的引用

    private void Start()
    {
        // 初始时苹果对象不可见
        if (apple != null)
        {
            apple.SetActive(false);  // 隐藏苹果
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判断是否是玩家碰到触发器
        if (other.CompareTag("Player"))
        {
            if (apple != null)
            {
                apple.SetActive(true);  // 显示苹果
            }
        }
    }
}
