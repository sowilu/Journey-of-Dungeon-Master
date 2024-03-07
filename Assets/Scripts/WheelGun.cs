using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGun : Shootgun
{
    public override void Shoot()
    {
        var angle = 360 / 8 * Mathf.Deg2Rad;

        for (int i = 0; i < 8; i++)
        {
            var x = Mathf.Cos(angle * i);
            var y = Mathf.Sin(angle * i);

            var bullet = Instantiate(bulletPrefab, player.transform.position + new Vector3(x, y), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * bulletSpeed;
            Destroy(bullet, 2);
        }
    }
}
