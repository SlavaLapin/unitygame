using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float rotationSpeed = 150.0f;
    public float movementSpeed = 3.0f;
    public float maxEnergy = 100.0f;
    private float energy;
    void Update()
    {
        if (!isLocalPlayer)  return;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);


        if (Input.GetButton("Submit"))
        {
            if (energy > 0) { CmdFire(); energy -= 5; }
        }
        else energy+=5;
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        energy = maxEnergy;

    }

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

}
