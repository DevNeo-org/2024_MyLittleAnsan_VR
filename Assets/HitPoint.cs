using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour // Hitbox가 Hit되는 지점 스크립트
{
    public void Deactivate(Material mat)
    {
        StartCoroutine(DeactivateFinalpoint(mat));
    }
    IEnumerator DeactivateFinalpoint(Material mat)
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        transform.GetChild(0).GetComponent<MeshRenderer>().material = mat; // SubObjectColorFalse
        gameObject.SetActive(false);
    }
}
