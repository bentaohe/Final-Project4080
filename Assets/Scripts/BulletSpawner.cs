using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletSpawner : NetworkBehaviour
{
    public Rigidbody bullet;
    private float bulletSpeed = 20f;
    public NetworkVariable<int> BulletDamage = new NetworkVariable<int>(5);
    private int maxDamage = 20;
    public void IncreaseDamage()
    {
        if(BulletDamage.Value == 1)
        {
            BulletDamage.Value = 5;
        }else
        {
            BulletDamage.Value += 5;
        }if(BulletDamage.Value > maxDamage)
        {
            BulletDamage.Value = maxDamage;
        }
    }
    public bool IsAtMaxDamage()
    {
        return BulletDamage.Value == maxDamage;
    }
    [ServerRpc]
    public void FireServerRpc() {
        Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.velocity = transform.forward * bulletSpeed;
        newBullet.gameObject.GetComponent<NetworkObject>().Spawn();
        Destroy(newBullet.gameObject, 3);
        newBullet.GetComponent<Bullet>().Damage.Value = BulletDamage.Value;
    }
}
