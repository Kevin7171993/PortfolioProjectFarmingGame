using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField]
    private float moveX, moveY;
    public Rigidbody2D rb;
    public Animator anim;
    private float moveMag;
    private Vector2Int direction;
    [SerializeField]
    Vector3 moveV;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gPlayer = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //if(moveX == 0.0f && moveY == 0.0f)
        //{
        //    direction = Vector2Int.zero;
        //    return;
        //}
        if (moveX > 0.0f) //right
        {
            direction.x = 1;
            direction.y = 0;
        }
        else if (moveX < 0.0f) //left
        {
            direction.x = -1;
            direction.y = 0;
        }

        if (moveY > 0.0f) //up
        {
            direction.y = 1;
            direction.x = 0;
        }
        else if (moveY < 0.0f) //down
        {
            direction.y = -1;
            direction.x = 0;
        }
        anim.SetFloat("dirX", direction.x);
        anim.SetFloat("dirY", direction.y);
    }

    void FixedUpdate()
    {
        if (moveV.sqrMagnitude < 0.001f)
        {
            if (direction.x == 1)
            {
                anim.Play("Player_Idle_Right");
            }
            else if (direction.x == -1)
            {
                anim.Play("Player_Idle_Left");
            }
            else if (direction.y == 1)
            {
                anim.Play("Player_Idle_Up");
            }
            else if (direction.y == -1)
            {
                anim.Play("Player_Idle");
            }
        }
        anim.SetFloat("Horizontal", moveX);
        anim.SetFloat("Vertical", moveY);
        anim.SetFloat("speed", moveV.sqrMagnitude);

        if (UIManager.UILock) { return; }
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveV = new Vector2(moveX, moveY);
        moveV = Vector2.ClampMagnitude(moveV, 1.0f);

        rb.MovePosition(transform.position + (moveV * moveSpeed * Time.fixedDeltaTime));
    }
}
