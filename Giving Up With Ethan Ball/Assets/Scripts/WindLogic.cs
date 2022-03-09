using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLogic : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float WindForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.WindPush(-WindForce);
    }
}
