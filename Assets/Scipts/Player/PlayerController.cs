using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IController
{
    public float speed;
    private Vector3 movement;

    private Player _p;
    
    public PlayerController(Player p)
    {
        _p = p;
    }
    
    public void OnUpdate()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _p.OnMove(movement);
    }
}
