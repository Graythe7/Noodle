using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite cloneSprite;
    public Animator animator;

    void Start()
    {
        GameManager.instance.AddPlayer(this);
        GameManager.instance.CheckLose(transform.position);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.Lost)
        {
            return;
        }
        Vector3 newPos = default;
        Vector3 selfPos = transform.position;
        bool flag = false;
        if (Input.GetKeyDown(KeyCode.A))
        {
            newPos = new Vector3(selfPos.x - 1, selfPos.y);
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            newPos = new Vector3(selfPos.x, selfPos.y + 1);
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            newPos = new Vector3(selfPos.x, selfPos.y - 1);
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            newPos = new Vector3(selfPos.x + 1, selfPos.y);
            flag = true;
        }

        if (flag && GameManager.instance.IsEmpty(newPos))
        {
            var o = Instantiate(this);
            o._spriteRenderer.sprite = cloneSprite;
            o.animator.enabled = false;
            o.transform.position = newPos;
        }
    }
}