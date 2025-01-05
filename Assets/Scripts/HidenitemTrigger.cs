using System.Collections;
using UnityEngine;

public class HidenitemTrigger : MonoBehaviour
{
    [SerializeField] public GameObject hiddenobject1;
    [SerializeField] public GameObject hiddenobject2;
    [SerializeField] public GameObject hiddenobject3;
    [SerializeField] public GameObject hiddenobject4;

    private SpriteRenderer spriteRenderer1;
    private SpriteRenderer spriteRenderer2;
    private SpriteRenderer spriteRenderer3;
    private SpriteRenderer spriteRenderer4;

    private void Start()
    {
        // 获取物品的 SpriteRenderer 组件
        spriteRenderer1 = hiddenobject1.GetComponent<SpriteRenderer>();
        spriteRenderer2 = hiddenobject2.GetComponent<SpriteRenderer>();
        spriteRenderer3 = hiddenobject3.GetComponent<SpriteRenderer>();
        spriteRenderer4 = hiddenobject4.GetComponent<SpriteRenderer>();

        HideItems(); // 初始时隐藏物品
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowItems(); // 玩家碰到触发器时显示物品
            ShowHint();  // 显示物品的SpriteRenderer
        }
    }

    public void HideItems()
    {
        hiddenobject1.SetActive(false);
        hiddenobject2.SetActive(false);
        hiddenobject3.SetActive(false);
        hiddenobject4.SetActive(false);
    }

    public void ShowItems()
    {
        hiddenobject1.SetActive(true);
        hiddenobject2.SetActive(true);
        hiddenobject3.SetActive(true);
        hiddenobject4.SetActive(true);
    }

    private void ShowHint()
    {
        StartCoroutine(DisplayHintForDuration(0.3f));  // 显示物品1秒
    }

    private IEnumerator DisplayHintForDuration(float duration)
    {
        // 临时启用物品的 SpriteRenderer 组件
        if (spriteRenderer1 != null)
        {
            spriteRenderer1.enabled = true;  // 显示物品的Sprite
        }
        if (spriteRenderer2 != null)
        {
            spriteRenderer2.enabled = true;
        }
        if (spriteRenderer3 != null)
        {
            spriteRenderer3.enabled = true;
        }
        if (spriteRenderer4 != null)
        {
            spriteRenderer4.enabled = true;
        }

        // 等待指定时间后隐藏物品的 SpriteRenderer
        yield return new WaitForSeconds(duration);

        // 恢复隐藏物品的 SpriteRenderer
        if (spriteRenderer1 != null)
        {
            spriteRenderer1.enabled = false;  // 隐藏物品的Sprite
        }
        if (spriteRenderer2 != null)
        {
            spriteRenderer2.enabled = false;
        }
        if (spriteRenderer3 != null)
        {
            spriteRenderer3.enabled = false;
        }
        if (spriteRenderer4 != null)
        {
            spriteRenderer4.enabled = false;
        }
    }
}
