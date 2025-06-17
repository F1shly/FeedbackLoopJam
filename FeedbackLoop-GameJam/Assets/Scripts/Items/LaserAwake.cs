using UnityEngine;
using System.Collections;

public class LaserAwake : MonoBehaviour
{
    public bool horizontal;

    public Transform laserBeam;
    public Transform laserEnd;
    public Transform laserStart;
    public LayerMask layer;

    private void Awake()
    {
        if (!horizontal)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, layer);
            Debug.Log(hit.distance);
            float rayLength = hit.distance;

            float beamYPos = transform.position.y + (rayLength / 2) - 0.25f;
            laserBeam.position = new Vector2(transform.position.x, beamYPos);
            laserBeam.localScale = new Vector2(laserBeam.localScale.x, rayLength + 0.5f);

            float endYPos = transform.position.y + rayLength;
            laserEnd.position = new Vector2(transform.position.x, endYPos);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, layer);
            RaycastHit2D fullHit = Physics2D.Raycast(hit.point + new Vector2(0.5f, 0), Vector2.right, Mathf.Infinity, layer);
            float rayLength = fullHit.distance + 0.5f;

            float beamXPos = hit.point.x + (rayLength / 2);
            laserBeam.position = new Vector2(beamXPos, transform.position.y + 0.25f);
            laserBeam.localScale = new Vector2(rayLength, laserBeam.localScale.y);

            float startPosX = hit.point.x;
            laserStart.position = new Vector2(startPosX, transform.position.y + 0.25f);

            float endPosX = fullHit.point.x;
            laserEnd.position = new Vector2(endPosX, transform.position.y + 0.25f);
        }

        StartCoroutine(LaserToggle());
    }
    public void ResetBeam()
    {
        if (!horizontal)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, layer);
            Debug.Log(hit.distance);
            float rayLength = hit.distance;

            float beamYPos = transform.position.y + (rayLength / 2) - 0.25f;
            laserBeam.position = new Vector2(transform.position.x, beamYPos);
            laserBeam.localScale = new Vector2(laserBeam.localScale.x, rayLength + 0.5f);

            float endYPos = transform.position.y + rayLength;
            laserEnd.position = new Vector2(transform.position.x, endYPos);
        }
        else
        {
            RaycastHit2D fullHit = Physics2D.Raycast(new Vector2(laserEnd.position.x, laserEnd.position.y) - new Vector2(0.5f, 0), Vector2.left, Mathf.Infinity, layer);
            float rayLength = fullHit.distance + 0.5f;

            float beamXPos = laserEnd.position.x - (rayLength / 2);
            laserBeam.position = new Vector2(beamXPos, transform.position.y + 0.25f);
            laserBeam.localScale = new Vector2(rayLength, laserBeam.localScale.y);

            float endPosX = fullHit.point.x;
            laserStart.position = new Vector2(endPosX, transform.position.y + 0.25f);
        }
    }
    IEnumerator LaserToggle()
    {
        yield return new WaitForSeconds(2);
        laserBeam.gameObject.SetActive(!laserBeam.gameObject.activeInHierarchy);
        StartCoroutine(LaserToggle());
    }
}
