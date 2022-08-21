using System.Collections;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public Transform exitPoint;
    private float timeElapsed = 0;
    public AudioSource gunShot;
    public Camera camera;
    public Animator anim;
    public ParticleSystem flash;

    private int nbBullet = 30;
    private bool isReloading = false;
    void Update()
    {
        if (!PlayerInfo.playerDead && Input.GetMouseButton(0))
        {
            GameObject bullet = BulletPool.sharedInstance.getPooledObject();
            if (bullet != null && timeElapsed > 100)
            {
                nbBullet--;
                if (nbBullet > 0)
                {
                    bullet.transform.position = exitPoint.position;
                    RaycastHit hitInfo;
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hitInfo, 200f, LayerMask.NameToLayer("Player")))
                    {
                        if (hitInfo.transform.tag.Equals("Enemy"))
                        {
                            hitInfo.transform.GetComponent<EnemyScript>().removeHealth();
                        }
                    }
                    Vector3 direction = ray.direction;
                    bullet.SetActive(true);
                    bullet.transform.rotation = exitPoint.rotation;
                    bullet.GetComponent<Rigidbody>().velocity = transform.forward * 200;
                    timeElapsed = 0;
                    gunShot.Play();
                    flash.Play();
                }
                else if (!isReloading)
                {
                    isReloading = true;
                    StartCoroutine(Reload());
                }
            }
        }
        timeElapsed += Time.deltaTime * 1000;
    }
    
    IEnumerator Reload()
    {
        anim.SetTrigger("reload");
        yield return new WaitForSeconds(2.26f);
        nbBullet = 30;
        isReloading = false;
    }
}
