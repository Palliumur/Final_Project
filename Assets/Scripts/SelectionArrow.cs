using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentIndex;
    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
        {
            ChangePosition(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
        {
            ChangePosition(-1);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }
    }

    private void ChangePosition(int index)
    {
        currentIndex += index;
        if (currentIndex < 0)
        {
            currentIndex = options.Length - 1;
        }
        else if (currentIndex > options.Length - 1)
        {
            currentIndex = 0;
        }
        rect.position = new Vector3(rect.position.x, options[currentIndex].position.y, 0);
    }
    private void Interact()
    {
        options[currentIndex].GetComponent<Button>().onClick.Invoke();
    }
}
