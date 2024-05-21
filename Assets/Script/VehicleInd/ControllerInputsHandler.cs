using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ControllerInputsHandler : MonoBehaviour // ��ġ �׷� ��ũ��Ʈ
{
    [SerializeField] private ControllerRef controllerRef;
    [SerializeField] private GrabInteractor grabInteractor;
    [SerializeField] private OVRControllerHelper controllerHelper;
    [SerializeField] private GameObject wrench; // �¿� ��ġ �и��ؼ� ����
    [SerializeField] private bool isLeft;
    private GrabInteractable grabInteractable;
    private HitboxSpawner hitboxSpawner;
    
    private void Start()
    {
        hitboxSpawner = FindAnyObjectByType<HitboxSpawner>();
    }
    private void Update()
    {
        if (!grabInteractor.HasSelectedInteractable) // �׷����� �ʾ��� ���
        {
            grabInteractable = null;
            return;
        }
        if (grabInteractable == null) // �׷����� ���(��ġ)
        {
            if (Time.timeScale == 0) { return; } // �Ͻ����� ������ ��� �׷� ��Ȱ��
            wrench.gameObject.SetActive(true);
            hitboxSpawner.PickUp(isLeft);
            controllerHelper.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand; // ��Ʈ�ѷ� �׷��� ��Ȱ��ȭ
            grabInteractable = grabInteractor.SelectedInteractable;
            Destroy(grabInteractor.transform.parent.gameObject);
            Destroy(grabInteractable.gameObject);
            Destroy(gameObject);
        }
    }
}
