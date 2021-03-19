using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField]
    private float moveX, moveY;
    public Rigidbody2D rb;
    private float moveMag;
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

    }

    void FixedUpdate()
    {
        if(UIManager.UILock) { return; }
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveV = new Vector2(moveX, moveY);
        moveV = Vector2.ClampMagnitude(moveV, 1.0f);

        rb.MovePosition(transform.position + (moveV * moveSpeed * Time.fixedDeltaTime));
    }
}
