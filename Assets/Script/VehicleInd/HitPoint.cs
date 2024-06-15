using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour // Hitbox�� Hit�Ǵ� ���� ��ũ��Ʈ
{
    public void Deactivate(Material mat)
    {
        StartCoroutine(DeactivateFinalpoint(mat));
    }
    IEnumerator DeactivateFinalpoint(Material mat)
    {
        yield return new WaitForSeconds(1f); // 1�� ���
        transform.GetChild(0).GetComponent<MeshRenderer>().material = mat; // SubObjectColorFalse
        gameObject.SetActive(false);
    }
}
