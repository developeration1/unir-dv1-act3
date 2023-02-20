using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shot;
    [SerializeField] float shotForce;
    [SerializeField] int ammo;
    [SerializeField] float fireRate;

    float nextBulletTime;

    private void Start()
    {
        GameManager.Instance.CheckPlayerAmmo(ammo);
    }

    private void OnEnable()
    {
        player.OnTriggerAction += OnShoot; 
    }

    private void OnDisable()
    {
        player.OnTriggerAction -= OnShoot;
    }

    private void OnShoot()
    {
        if (nextBulletTime > Time.time || ammo <= 0) return;
        Instantiate(shot, shootPoint.position, Quaternion.identity);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f));
        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit) ? hit.point : ray.GetPoint(1000);
        Vector3 direction = (targetPoint - shootPoint.position).normalized;
        GameObject shotBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        shotBullet.transform.forward = direction;
        shotBullet.GetComponent<Rigidbody>().AddForce(shotForce * direction, ForceMode.Impulse);
        nextBulletTime = Time.time + fireRate;
        ammo--;
        GameManager.Instance.CheckPlayerAmmo(ammo);
    }
}
