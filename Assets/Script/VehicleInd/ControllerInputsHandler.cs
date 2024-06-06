using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ControllerInputsHandler : MonoBehaviour // 자동차 산업 본체험 시작 전 렌치 Grab 체크용 스크립트
{
    [SerializeField] private ControllerRef controllerRef;
    [SerializeField] private GrabInteractor grabInteractor;
    [SerializeField] private OVRControllerHelper controllerHelper;
    [SerializeField] private GameObject wrench; // 좌우 렌치 분리해서 적용
    [SerializeField] private bool isLeft;
    private GrabInteractable grabInteractable;
    private HitboxSpawner hitboxSpawner;
    private VehicleManager vehicleManager;
    private void Start()
    {
        hitboxSpawner = FindAnyObjectByType<HitboxSpawner>();
        vehicleManager = FindAnyObjectByType<VehicleManager>();
    }
    private void Update()
    {
        if (!grabInteractor.HasSelectedInteractable) // 그랩하지 않았을 경우
        {
            grabInteractable = null;
            return;
        }
        if (grabInteractable == null) // 그랩했을 경우(렌치)
        {
            if (vehicleManager.IsRayControllerOn()) { return; } // 레이 컨트롤러 켜져 있을 경우 그랩 비활성화
            wrench.gameObject.SetActive(true);
            hitboxSpawner.PickUp(isLeft);
            controllerHelper.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand; // 컨트롤러 그래픽 비활성화
            grabInteractable = grabInteractor.SelectedInteractable;
            // 씬을 재시작하지 않는 이상 다시 그랩할 이유는 없으므로 관련 오브젝트 Destroy 처리
            Destroy(grabInteractor.transform.parent.gameObject);
            Destroy(grabInteractable.gameObject);
            Destroy(gameObject);
        }
    }
}
