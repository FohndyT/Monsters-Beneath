// Jeremy Legault

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Animations : MonoBehaviour
{
    Animator anim;
    Player player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = transform.parent.GetComponent<Player>();
    }
    private void Update()
    {
        if (player.health <= 0)
            anim.SetTrigger("dies");
    }
    void OnMove(InputValue value)
    { anim.SetTrigger("moving"); }
    void OnDash()
    { anim.SetTrigger("catpose"); }
    void OnJump(InputValue value)
    { anim.SetTrigger("jump"); }
    void OnAttack(InputValue value)
    { anim.SetTrigger("attack"); }
    void OnItem(InputValue value)
    { anim.SetTrigger("throw"); }
}
