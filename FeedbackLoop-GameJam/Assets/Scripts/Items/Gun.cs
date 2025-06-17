using UnityEngine;
using System.Collections;
public class Gun : MonoBehaviour
{
    public GameObject Target;
    public GameObject Barrel;
    public GameObject endOfBarrel;
    public GameObject Bullet;
    public LineRenderer Line;
    public LayerMask Layer;
    public Material stage1, stage2, stage3, stage4, stage5;
    float timer;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(GunShoot());
        StartCoroutine(Stages());
        Line.material = stage1;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, Layer);
        Debug.Log(hit.distance);
        float rayLength = hit.distance;

        float gunPosY = transform.position.y + rayLength- 0.5f;
        transform.parent.position = new Vector2(transform.position.x, gunPosY);
    }
    private void Update()
    {
        var dir = Target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        RaycastHit2D hit;
        hit = Physics2D.Raycast(Barrel.transform.position, Target.transform.position - Barrel.transform.position, Mathf.Infinity, Layer);
        Line.SetPosition(0, Barrel.transform.position);
        Line.SetPosition(1, hit.point);

        timer += Time.deltaTime;
        if(timer <0)
        {
            Line.startWidth = (0);
            Line.endWidth = (0);
        }
        else
        {
            Line.startWidth = (timer * 0.04f);
            Line.endWidth = (timer * 0.04f);
        }

    }
    IEnumerator GunShoot()
    {
        yield return new WaitForSeconds(4);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(endOfBarrel.transform.position, Target.transform.position - endOfBarrel.transform.position, Mathf.Infinity, Layer);
        Bullet.GetComponent<Bullet>().TargetPos = hit.point;
        Instantiate(Bullet, endOfBarrel.transform.position, transform.rotation, transform.parent = null);
        Line.material = stage1;
        timer = -1;
        StartCoroutine(GunShoot());
        StartCoroutine(Stages());
    }
    IEnumerator Stages()
    {
        yield return new WaitForSeconds(2);
        Line.material = stage2;
        yield return new WaitForSeconds(1f);
        Line.material = stage3;
        yield return new WaitForSeconds(0.4f);
        Line.material = stage4;
        yield return new WaitForSeconds(0.1f);
        Line.material = stage5;
        yield return new WaitForSeconds(0.1f);
        Line.material = stage4;
        yield return new WaitForSeconds(0.1f);
        Line.material = stage5;
        yield return new WaitForSeconds(0.1f);
        Line.material = stage4;
    }
}
