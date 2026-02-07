using DG.Tweening;
using UnityEngine;

public class Destiny : SkillBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float dashDistance;
    [SerializeField] float dashDuration;
    [SerializeField] LayerMask wallLayer;
    

    public override void Execute()
    {
        StartDash();
        Debug.Log("Enemy damage scripti yazýlacak");
        Debug.Log("SO inheritance yapýlacak ve deðerler oradan çekilecek");
        // animasyon oynar enemyler iþaretlenir animasyon bitince iþaretlenen enemyler karaktere çekilir ve dmg atýlýr
        // Opsiyonel -- animasyon oynar karakterin çarptýgý enemyler havaya savrulur        
    }

    public override void Stop()
    {
        //throw new System.NotImplementedException();
    }

    void StartDash()
    {
        rb.useGravity = false;
        rb.isKinematic = true;

        Vector3 direction = transform.forward;
        Vector3 targetPos = transform.position + (direction * dashDistance);
        Vector3 rayOrigin = transform.position + Vector3.up;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, direction, out hit, dashDistance, wallLayer))
        {
            targetPos = hit.point - direction;
            targetPos.y = transform.position.y;
        }

        transform.DOMove(targetPos, dashDuration)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() =>
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * dashDistance);
    }
}
