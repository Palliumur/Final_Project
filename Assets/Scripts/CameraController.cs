using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float moveSpeed = 4f;  // 相机移动速度
    [SerializeField] private float zoomSpeed = 0.3f;  // 相机缩放速度
    [SerializeField] private Transform firstTarget; // 第一个目标点
    [SerializeField] private Transform secondTarget; // 第二个目标点
    [SerializeField] private float initialZoomSize = 5f; // 初始相机大小
    [SerializeField] private float finalZoomSize = 3f; // 最终相机大小

    [SerializeField] private PlayerMovement player; // 引用 PlayerMovement 脚本

    private bool isInIntro = true;  // 标志位，用于判断是否在开场动画阶段

    private void Start()
    {
        Camera.main.orthographicSize = initialZoomSize; // 设置初始相机大小

        // 禁用玩家移动
        player.movable = false;

        // 开始开场动画
        StartCoroutine(MoveAndZoomSequence());
    }

    private void Update()
    {
        if (isInIntro)
        {
            // 开场动画阶段，相机移动到指定位置
            return;  // 不执行后续的房间间移动
        }

        // 正常的房间间移动
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY + 1.59f, transform.position.z), ref velocity, speed);
    }

    private IEnumerator MoveAndZoomSequence()
    {
        // 第一阶段：移动到第一个目标点
        yield return MoveToPosition(firstTarget.position);

        // 第二阶段：移动到第二个目标点并缩小相机
        yield return MoveAndZoomToPosition(secondTarget.position, finalZoomSize);

        // 开场动画结束，允许玩家移动
        isInIntro = false;
        player.movable = true;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, moveSpeed);
            yield return null; // 等待下一帧
        }
        transform.position = targetPosition; // 确保精确到达目标位置
    }

    private IEnumerator MoveAndZoomToPosition(Vector3 targetPosition, float targetZoomSize)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f || Mathf.Abs(Camera.main.orthographicSize - targetZoomSize) > 0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, moveSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoomSize, Time.deltaTime * zoomSpeed);
            yield return null; // 等待下一帧
        }
        transform.position = targetPosition; // 精确调整位置
        Camera.main.orthographicSize = targetZoomSize; // 精确调整缩放大小
    }

    public void MovetoNextRoom(Transform _nextRoom)
    {
        currentPosX = _nextRoom.position.x;
        currentPosY = _nextRoom.position.y;
        Debug.Log(currentPosX);
    }
}
