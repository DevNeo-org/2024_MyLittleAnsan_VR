# 추후 수정 예정(~06.15)
# My Little Ansan 개발자 기술문서

My Little Ansan은 타이틀, 도시 디자인, 체험 3종, 총 5개의 씬으로 구성된다.
개발자 기술문서는 크게 5 단락으로 되어있으며, 주요 씬 4개(자동차, 섬유, 전자, 도시)와 공통 기능을 다룬 다이얼로그

1. [자동차 산업 씬](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%EA%B8%B0%EC%88%A0%EB%AC%B8%EC%84%9C_%EA%B0%9C%EB%B0%9C%EC%9E%90.md#%EC%9E%90%EB%8F%99%EC%B0%A8-%EC%82%B0%EC%97%85-%EC%94%AC)
2. [섬유 산업 씬](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%EA%B8%B0%EC%88%A0%EB%AC%B8%EC%84%9C_%EA%B0%9C%EB%B0%9C%EC%9E%90.md#%EC%84%AC%EC%9C%A0-%EC%82%B0%EC%97%85-%EC%94%AC)
3. [전자 산업 씬](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%EA%B8%B0%EC%88%A0%EB%AC%B8%EC%84%9C_%EA%B0%9C%EB%B0%9C%EC%9E%90.md#%EC%A0%84%EC%9E%90-%EC%82%B0%EC%97%85-%EC%94%AC)
4. [도시 디자인 씬]([https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents#%EB%8F%84%EC%8B%9C-%EB%94%94%EC%9E%90%EC%9D%B8-%EC%94%AC](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%EA%B8%B0%EC%88%A0%EB%AC%B8%EC%84%9C_%EA%B0%9C%EB%B0%9C%EC%9E%90.md#%EB%8F%84%EC%8B%9C-%EB%94%94%EC%9E%90%EC%9D%B8-%EC%94%AC))
5. [다이얼로그와 기타 상황](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%EA%B8%B0%EC%88%A0%EB%AC%B8%EC%84%9C_%EA%B0%9C%EB%B0%9C%EC%9E%90.md#%EB%8B%A4%EC%9D%B4%EC%96%BC%EB%A1%9C%EA%B7%B8%EC%99%80-%EA%B8%B0%ED%83%80-%EC%83%81%ED%99%A9)

---

## 자동차 산업 씬 

AutomobIndScene.unity 씬에서 사용되는 스크립트의 기능들을 정의한다

---
### ControllerInputsHandler

자동차 산업 본체험 시작 전 렌치 Grab 체크용 스크립트

- Fields:
  - [SerializeField] private ControllerRef controllerRef: 컨트롤러 참조 변수
  - [SerializeField] private GrabInteractor grabInteractor: Grab 인터렉터 참조 변수
  - [SerializeField] private OVRControllerHelper controllerHelper: OVR 컨트롤러 헬퍼 참조 변수
  - [SerializeField] private GameObject wrench: 좌우 렌치 분리해서 적용
  - [SerializeField] private bool isLeft: 좌우 렌치 구분 변수

- Methods:
  - private void HandleGrab(): 오브젝트 잡기를 처리하는 함수

---
### HitboxMovement

자동차 산업 씬에서 히트박스의 이동을 처리하는 클래스

- Fields:
  - [SerializeField] float force: 이동속도
  - [SerializeField] private Material[] materials: 히트박스의 재질 배열

- Methods:
  - private void Start(): 초기 설정 함수
  - private void Update(): 매 프레임마다 호출되는 함수
  - public void TurnOff(): 히트박스를 비활성화하는 함수
  - public void SetFinalPoint(int num): 최종 도달 위치를 설정하는 함수
  - public void StopMovement(): 히트박스의 이동을 멈추는 함수

---
### HitboxSpawner

자동차 산업 씬에서 히트박스를 생성하는 클래스

- Fields:
  - [SerializeField] GameObject hitboxPrefab: 히트박스 프리팹
  - [SerializeField] Transform[] points: 히트박스의 시작 위치 배열
  - [SerializeField] GameObject finalPointObject: 최종 위치 부모 오브젝트
  - public Transform[] finalPoints: 최종 위치 배열
  - private float beat = 2f: 히트박스 소환 주기
  - private float timer = 0: 히트박스 소환 타이머
  - private VehicleManager vehicleManager: VehicleManager 참조 변수
  - private GameObject timerText: 타이머 텍스트 오브젝트
  - public bool leftWrenchOn = false: 왼쪽 렌치 활성화 여부
  - public bool rightWrenchOn = false: 오른쪽 렌치 활성화 여부

- Methods:
  - public void PickUp(bool isLeft): 렌치를 집었는지 여부를 설정하는 함수
  - private void PlaySound(): 히트박스 소환 소리를 재생하는 함수

---
### HitPoint

자동차 산업 씬에서 히트박스가 Hit되는 지점 스크립트

- Methods:
  - public void Deactivate(Material mat): 히트박스를 비활성화하는 함수
  - private IEnumerator DeactivateFinalpoint(Material mat): 히트박스를 비활성화하는 코루틴 함수

---
### MainCarObject

자동차 산업 씬에서 메인 자동차 오브젝트를 제어하는 클래스

- Fields:
  - [SerializeField] int scoreIndex: 점수 인덱스
  - private VehicleManager vehicleManager: VehicleManager 참조 변수
  - private int score = 0: 현재 점수
  - private Animation anim: 애니메이션 컴포넌트
  - private AnimationState[] animStates: 애니메이션 상태 배열

- Methods:

---
### VehicleHitbox

자동차 산업 씬에서 히트박스를 제어하는 클래스

- Fields:
  - [SerializeField] private GameObject correctEffectPrefab: 점수 획득 성공 이펙트
  - [SerializeField] private GameObject wrongEffectPrefab: 점수 획득 실패 이펙트
  - public bool isCorrect = false: 점수 획득 가능 여부
  - private GameObject instateEffectObj: 이펙트 오브젝트
  - private VehicleManager vehicleManager: VehicleManager 참조 변수
  - private Transform finalPoint: 최종 도달 위치

- Methods:
  - public int ObjectHit(): 히트박스가 히트되었을 때 호출되는 함수
  - private void CorrectEffectPlay(): 점수 획득 성공 이펙트를 재생하는 함수
  - private void WrongEffectPlay(): 점수 획득 실패 이펙트를 재생하는 함수
  - public void SetVehicleManager(VehicleManager temp): VehicleManager 설정 함수

---
### VehicleManager

자동차 산업 씬에서 차량 관리를 처리하는 클래스

- Fields:
  - [SerializeField] GameObject menu: 일시정지 메뉴 오브젝트
  - [SerializeField] GameObject resultMenu: 결과창 오브젝트
  - [SerializeField] GameObject timerUI: 타이머 및 점수 UI 오브젝트
  - [SerializeField] GameObject leftRayController: 왼쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] GameObject rightRayController: 오른쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] private OVRControllerHelper controllerHelperLeft: 왼쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] private OVRControllerHelper controllerHelperRight: 오른쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] private GameObject leftWrench: 왼쪽 렌치 오브젝트
  - [SerializeField] private GameObject rightWrench: 오른쪽 렌치 오브젝트
  - [SerializeField] TextMeshPro scoreText: 점수 텍스트 컴포넌트
  - [SerializeField] GameObject wrenchObjects: 렌치 부모 오브젝트
  - [SerializeField] GameObject celebrate: 클리어 축포 오브젝트
  - private Timer timer: Timer 참조 변수
  - private HitboxSpawner spawner: HitboxSpawner 참조 변수
  - private int score: 현재 점수
  - private bool isMenuOn = false: 일시정지 메뉴 활성화 여부
  - private bool gameEnd = false: 게임 종료 여부
  - private bool isDialogEnd = false: 다이얼로그 종료 여부
  - private DataManager dataManager: DataManager 참조 변수

- Methods:
  - public void ScorePlus(): 점수를 증가시키는 함수
  - public int GetScore(): 현재 점수를 반환하는 함수
  - public void LoadTitle(): 메인 메뉴 씬을 로드하는 함수
  - public void LoadCityScene(): 도시 디자인 씬을 로드하는 함수
  - public void RestartScene(): 현재 씬을 재시작하는 함수
  - public void EndGame(): 게임을 종료하는 함수
  - public void CloseMenu(): 메뉴를 닫는 함수
  - private void OpenMenu(): 메뉴를 여는 함수
  - private void GameClear(): 게임 클리어를 처리하는 함수
  - public bool IsRayControllerOn(): Ray 컨트롤러가 활성화

되어 있는지 확인하는 함수
  - public void DialogEnd(): 다이얼로그 종료를 처리하는 함수
  - public void PlayConfetti(): 클리어 축포를 실행하는 함수

---
### Wrench

자동차 산업 씬에서 렌치를 제어하는 클래스

- Fields:
  - public OVRInput.Controller controller: 좌우 개별적인 컨트롤러 할당 변수

- Methods:
  - private void OnTriggerEnter(Collider other): 충돌 시 호출되는 함수
  - private IEnumerator TriggerHaptics(): 렌치로 오브젝트 히트 시 진동을 처리하는 코루틴 함수
  - private IEnumerator StartTriggerHaptics(): 시작 트리거로 렌치를 집었을 시 진동을 처리하는 코루틴 함수

---

## 섬유 산업 씬 

TextileIndScene.unity 씬에서 사용되는 스크립트의 기능들을 정의한다.

---
### Ball

섬유 산업 씬에서 페인트볼의 기능을 담당하는 클래스

- Fields:
  - [SerializeField] int penSize: 색칠할 크기
  - [SerializeField] GameObject particleManage: 파티클 매니저 오브젝트
  - [SerializeField] Gun gun: Gun 클래스 참조 변수
  - [SerializeField] Material[] colorMaterial: 색칠할 색상 Material 배열
  - [SerializeField] private int paintSizeRange: 색칠 범위 크기
  - [SerializeField] private int paintSize: 색칠 크기
  - private Whiteboard _whiteboard: 화이트보드 참조 변수
  - private Rigidbody rigid: 리지드바디 컴포넌트
  - private RaycastHit _touch: 레이캐스트 히트 정보
  - private Vector2 _touchPos: 터치 위치
  - private ParticleManager particleManager: 파티클 매니저 참조 변수
  - private Color[] _color: 색상 배열
  - public int colorCode: 색상 코드
  - public float ballSpeed: 페인트볼 속도

- Methods:
  - public void ChangeColorCode(int code): 색상 코드를 변경하는 함수

---
### Clothes

섬유 산업 씬에서 옷의 기능을 담당하는 클래스

- Fields:
  - private Animator anim: 애니메이터 컴포넌트

- Methods:
  - void Start(): 초기 설정 함수
  - public void PlayAnim(): 게임 종료 애니메이션을 재생하는 함수
  - private void PauseGame(): 게임을 일시정지하는 함수

---
### Gun

섬유 산업 씬에서 페인트 총의 기능을 담당하는 클래스

- Fields:
  - [SerializeField] ParticleSystem[] _bucketPop: 색 변경 파티클
  - [SerializeField] GameObject gunObject: 페인트 총 오브젝트
  - [SerializeField] Material[] gunMaterials: 페인트 총 색상 Material 배열
  - [SerializeField] Material[] ballMaterial: 페인트볼 색상 Material 배열
  - [SerializeField] private GameObject board: 색칠되는 보드 오브젝트
  - [SerializeField] private GameObject ball: 발사되는 페인트볼 프리팹
  - [SerializeField] private float vibSize: 발사 진동 크기
  - [SerializeField] private GameObject changeSound: 색 변경 사운드 오브젝트
  - private Ball ballCS: Ball 클래스 참조 변수
  - private Renderer ballRenderer: 페인트볼 렌더러 컴포넌트
  - private Renderer gunRenderer: 페인트 총 렌더러 컴포넌트
  - private float nextShoot: 다음 발사 시간
  - private TextileManager textileManager: TextileManager 참조 변수
  - private bool isStart: 게임 시작 여부
  - private bool isEnd: 게임 종료 여부
  - public float ballSpeed: 페인트볼 발사 속도
  - public float shootRate: 발사 가능 간격
  - public int colorCode: 색상 코드
  - public OVRInput.Controller controller: 컨트롤러 참조 변수

- Methods:
  - private void Shoot(): 페인트볼을 발사하는 함수
  - public void ChangeColor(int code): 색상을 변경하는 함수
  - private void OnTriggerEnter(Collider other): 색 변경 페인트통과 충돌 시 호출되는 함수
  - IEnumerator TriggerHaptics(): 색 변경 진동을 처리하는 코루틴 함수
  - IEnumerator ShootTriggerHaptics(): 발사 진동을 처리하는 함수
  - public void StartGame(): 게임을 시작하는 함수
  - public void EndGame(): 게임을 종료하는 함수

---
### ParticleManager

섬유 산업 씬에서 파티클을 관리하는 클래스

- Fields:
  - [SerializeField] private float generateTime: 파티클 생성 간격
  - public GameObject[] particleList: 파티클 리스트
  - private float nextGenerate: 다음 파티클 생성 시간
  - private ParticleSystem particle: 파티클 시스템

- Methods:
  - public void PlayParticle(int particleID, Vector3 particlePos): 파티클을 재생하는 함수

---
### TextileManager

섬유 산업 씬에서 전체적인 관리 기능을 담당하는 클래스

- Fields:
  - [SerializeField] GameObject menu: 일시정지 메뉴 오브젝트
  - [SerializeField] GameObject resultMenu: 결과창 오브젝트
  - [SerializeField] TextMeshPro timeText: 타이머 텍스트
  - [SerializeField] GameObject leftRayController: 왼쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] GameObject rightRayController: 오른쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] private OVRControllerHelper controllerHelperLeft: 왼쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] private OVRControllerHelper controllerHelperRight: 오른쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] GameObject paintGun: 페인트 총 모델 프리팹
  - [SerializeField] GameObject line: 조준선 오브젝트
  - [SerializeField] private GameObject closeButton: 메뉴 닫기 버튼
  - [SerializeField] private GameObject timerUI: 타이머 UI 오브젝트
  - [SerializeField] ParticleSystem shine: 색칠 테두리 효과
  - [SerializeField] private GameObject[] celebrates: 게임 종료 파티클
  - [SerializeField] private GameObject[] buckets: 색 변경 페인트통
  - Timer timer: Timer 클래스 참조 변수
  - DataManager dataManager: DataManager 참조 변수
  - Gun gun: Gun 클래스 참조 변수
  - Clothes clothes: Clothes 클래스 참조 변수
  - private bool isMenuOn: 메뉴 열림 여부
  - private bool gameEnd: 게임 종료 여부
  - private bool isMenualClosed: 메뉴얼 닫힘 여부

- Methods:
  - public void LoadTitle(): 타이틀 씬을 로드하는 함수
  - public void LoadCityScene(): 도시 디자인 씬을 로드하는 함수
  - public void RestartScene(): 현재 씬을 재시작하는 함수
  - public void EndGame(): 게임을 종료하는 함수
  - public void CloseMenu(): 메뉴를 닫는 함수
  - private void OpenMenu(): 메뉴를 여는 함수
  - private void OpenResultMenu(): 결과창을 여는 함수
  - public bool IsMenuOn(): 메뉴가 열려있는지 여부를 반환하는 함수
  - public void EndManual(): 메뉴얼을 닫는 함수

---
### Whiteboard

섬유 산업 씬에서 화이트보드의 기능을 담당하는 클래스

- Fields:
  - public Texture2D texture: 텍스처
  - public Vector2 textureSize: 텍스처 크기

- Methods:

---

## 전자 산업 씬 

Electronic.unity 씬에서 사용되는 스크립트의 기능들을 정의한다

---
### Timer

전자 산업 씬에서 시간을 관리하는 타이머 클래스

- Fields:
  - float timeInSeconds = 60f: 초기 시간 설정
  - public TextMeshPro timeText: 타이머 텍스트 컴포넌트
  - private bool gameStart = false: 게임 시작 여부
  - private bool timeOver = false: 시간 종료 여부

- Methods:
  - public void StartGame(): 게임을 시작하는 함수
  - public void TimeOver(): 시간을 종료하는 함수
  - public bool GetBool(): 시간 종료 여부를 반환하는 함수

---
### Circlehit

전자 산업 씬에서 히트박스를 담당하는 클래스

- Fields:
  - private ElecMenuManagement menuManagement: ElecMenuManagement 클래스 참조 변수

- Methods:
  - private void Start(): 초기 설정 함수
  - public int ObjectHit(): 히트박스가 히트되었을 때 호출되는 함수

---
### ElecManagement

전자 산업 씬에서 전반적인 관리를 담당하는 클래스

- Fields:
  - public GameObject blinkingCirclePrefab: 깜빡이는 원 프리팹
  - public float blinkInterval = 3f: 깜빡이는 간격
  - private float rowposition = -0.19f: 행 위치
  - private float columnposition = 0.18f: 열 위치
  - [SerializeField] private GameObject[] circles: 원 배열
  - private Timer timer: Timer 클래스 참조 변수
  - private float time = 3f: 시간
  - int randomIndex = -1: 랜덤 인덱스
  - int prerandomIndex = -2: 이전 랜덤 인덱스
  - bool startgame = false: 게임 시작 여부
  - [SerializeField] private GameObject leftSolder: 왼쪽 납땜 오브젝트
  - [SerializeField] private GameObject rightSolder: 오른쪽 납땜 오브젝트

- Methods:
  - public void StartGame(): 게임을 시작하는 함수

---
### Soldering

전자 산업 씬에서 납땜 작업을 담당하는 클래스

- Fields:
  - public OVRInput.Controller controller: 컨트롤러 참조 변수
  - public GameObject particlePrefab: 파티클 프리팹
  - private Vector3 particleScale = new Vector3(0.1f, 0.05f, 0.1f): 파티클 스케일
  - private float enterTime = 0f: 입장 시간
  - public AudioSource effectsound: 효과음 오디오 소스

- Methods:

---
### ElecMenuManagement

전자 산업 씬에서 메뉴를 관리하는 클래스

- Fields:
  - [SerializeField] GameObject menu: 일시정지 메뉴 오브젝트
  - [SerializeField] GameObject resultMenu: 결과창 오브젝트
  - [SerializeField] GameObject timerUI: 타이머 및 점수 UI 오브젝트
  - [SerializeField] GameObject leftRayController: 왼쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] GameObject rightRayController: 오른쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] private OVRControllerHelper controllerHelperLeft: 왼쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] private OVRControllerHelper controllerHelperRight: 오른쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] private GameObject leftSolder: 왼쪽 납땜 오브젝트
  - [SerializeField] private GameObject rightSolder: 오른쪽 납땜 오브젝트
  - [SerializeField] TextMeshPro scoreText: 점수 텍스트 컴포넌트
  - public GameObject celebratePrefab: 축하 파티클 프리팹
  - public AudioSource celebratesound: 축하 소리 오디오 소스
  - Timer timer: Timer 클래스 참조 변수
  - private int score: 현재 점수
  - private bool isMenuOn = false: 메뉴 열림 여부
  - private bool gameEnd = false: 게임 종료 여부
  - DataManager datamanager: DataManager 클래스 참조 변수
  - bool startgame = false: 게임 시작 여부

- Methods:
  - private IEnumerator DelayedResultMenu(): 2초 대기 후 결과 메뉴를 여는 코루틴 함수
  - public void ScorePlus(): 점수를 증가시키는 함수
  - public int GetScore(): 현재 점수를 반환하는 함수
  - public void LoadTitle(): 타이틀 씬을 로드하는 함수
  - public void LoadCityScene(): 도시 디자인 씬을 로드하는 함수
  - public void RestartScene(): 현재 씬을 재시작하는 함수
  - public void EndGame(): 게임을 종료하는 함수
  - public void CloseMenu(): 메뉴를 닫는 함수
  - private void OpenMenu(): 메뉴를 여는 함수
  - public void StartGame(): 게임을 시작하는 함수

---

## 도시 디자인 씬 

CityDesign.unity 씬에서 사용되는 스크립트의 기능들을 정의한다.

---
### AudioManager

오디오 재생을 관리하는 클래스

- Fields:
  - [SerializeField] AudioSource[] audioArray: 오디오 소스 배열

- Methods:
  - public void PlaySound(int n): 특정 오디오 클립을 재생하는 메서드

---
### BuildPosition

빌딩 오브젝트가 특정 위치에 도달했을 때 건물을 생성하는 클래스

- Fields:
  - public GameObject[] builidngPrefabs: 빌딩 프리팹 배열
  - public bool isBuildComplete: 빌딩 완료 여부
  - GameObject selctEffect: 선택 효과 오브젝트
  - public GameObject gameManager: 게임 매니저 오브젝트
  - public GameObject dataManager: 데이터 매니저 오브젝트
  - public GameObject dialogManager: 다이얼로그 매니저 오브젝트
  - public ParticleSystem buildEffect: 빌딩 이펙트 파티클 시스템
  - public GameObject buildEffectPrefab: 빌딩 이펙트 프리팹
  - GameObject buildEffectObject: 빌딩 이펙트 오브젝트
  - public OVRInput.Controller controller: 컨트롤러 참조
  - string[] buildingSamples: 빌딩 샘플 이름 배열
  - string[] SampleNames: 빌딩 샘플 오브젝트 이름 배열
  - string[] areas: 구역 이름 배열

- Methods:
  - public bool returnBuildComplete(): 빌딩 완료 여부 반환 함수
  - void playBuildEffect(): 빌딩 이펙트를 재생하는 함수
  - IEnumerator TriggerHaptics(): 햅틱 피드백을 처리하는 코루틴 함수

---
### MenuController

일시정지 메뉴와 씬 전환을 관리하는 클래스

- Fields:
  - [SerializeField] GameObject menu: 일시정지 메뉴 오브젝트
  - public GameObject gameManager: 게임 매니저 오브젝트
  - [SerializeField] GameObject leftRayController: 왼쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] GameObject rightRayController: 오른쪽 Ray 컨트롤러 오브젝트
  - [SerializeField] private OVRControllerHelper controllerHelperLeft: 왼쪽 OVR 컨트롤러 헬퍼
  - [SerializeField] private OVRControllerHelper controllerHelperRight: 오른쪽 OVR 컨트롤러 헬퍼
  - private bool isMenuOn: 메뉴 활성화 여부

- Methods:
  - public void RestartScene(): 씬을 재시작하는 함수
  - public void EndGame(): 게임을 종료하는 함수
  - public void LoadTitle(): 타이틀 씬을 로드하는 함수
  - private void OpenMenu(): 메뉴를 여는 함수
  - public void CloseMenu(): 메뉴를 닫는 함수

---
### Lobby

로비 씬에서 빌딩 샘플을 관리하고 씬 전환을 처리하는 클래스

- Fields:
  - public GameObject[] samplePrefabs: 건물 모형 프리팹 배열
  - public GameObject[] buildingPrefabs: 건물 프리팹 배열
  - public GameObject[] buttonObjects: 씬 전환 버튼 오브젝트 배열
  - public GameObject[] AreaObjects: 건설 구역 오브젝트 배열
  - public GameObject dataManager: 데이터 매니저 오브젝트
  - public bool token: 건설 토큰
  - public string[] samples: 빌딩 샘플 이름 배열
  - public string[] sampleClones: 빌딩 샘플 클론 이름 배열
  - public string[] areas: 구역 이름 배열
  - public bool[] isClear: 씬 클리어 여부 배열
  - public bool[] sampleDestroyed: 빌딩 샘플 사용 여부 배열
  - public int[] areaBuild: 구역 건설 상태 배열
  - public ParticleSystem completeEffect: 게임 클리어 이펙트 파티클 시스템
  - public GameObject completeEffectPrefab: 게임 클리어 이펙트 프리팹
  - public GameObject DialogManager: 다이얼로그 매니저 오브젝트
  - GameObject completeEffectObject: 게임 클리어 이펙트 오브젝트
  - GameObject buildingPrefab: 현재 선택된 빌딩 프리팹
  - GameObject area: 현재 선택된 구역 오브젝트
  - bool isDialog: 다이얼로그 활성화 여부
  - public bool isOnArea: 구역 내 위치 여부

- Methods:
  - void makePrefab(): 건물 모형 프리팹을 생성하는 함수
  - public void playCompleteEffect(): 게임 클리어 이펙트를 재생하는 함수
  - public void SetIsOnArea(bool torf): 구역 내 위치 여부 설정 함수
  - public bool GetIsOnArea(): 구역 내 위치 여부 반환 함수

---
### DataManager

저장된 데이터를 관리하는 클래스

- Fields:
  - private bool[] isClear: 씬 클리어 여부 배열
  - private int[] areaState: 구역 건설 상태 배열
  - private bool[] sampleDestroyed: 빌딩 샘플 사용 여부 배열

- Methods:
  - public void DataClear(): 모든 데이터를 초기화하는 함수
  - public bool GetClear(int index): 씬 클리어 여부 반환 함수
  - public int GetAreaState(int index): 구역 건설 상태 반환 함수
  - public bool GetSampleDestroyed(int index): 빌딩 샘플 사용 여부 반환 함수
  - public void SetGameClear(): 게임 클리어 상태를 설정하는 함수

---

## 다이얼로그와 기타 상황 

다이얼로그와 기타 상황에서 사용되는 스크립트의 기능들을 정의한다.

---
### DialogParser

텍스트 파일을 파싱하여 다이얼로그 데이터를 생성하는 클래스

- Methods:
  - public Dialogue[] Parse(string fileName): 주어진 파일명을 통해 대사 리스트를 파싱하는 메서드

---
### BackGroundMusic

백그라운드 음악을 재생하는 클래스

- Fields:
  - [SerializeField] AudioSource[] musicSources: 음악 소스 배열

- Methods:
  - public void PlayMusic(int index): 특정 음악을 재생하는 메서드
  - public void StopMusic(): 음악을 중지하는 메서드
  - public void PauseMusic(): 음악을 일시 정지하는 메서드
  - public void ResumeMusic(): 음악을 다시 재생하는 메서드

---
### DatabaseManager

데이터베이스를 관리하는 클래스

- Fields:
  - private Dictionary<int, Dialogue> dialogueDatabase: 다이얼로그 데이터베이스

- Methods:
  - public void LoadDatabase(): 데이터베이스를 로드하는 메서드
  - public Dialogue GetDialogue(int id): 특정 ID의 다이얼로그를 반환하는 메서드

---
### Dialog

다이얼로그 데이터를 저장하는 클래스

- Fields:
  - public int id: 다이얼로그 ID
  - public string[] contexts: 다이얼로그 문장 배열

---
### DialogManager

다이얼로그를 관리하는 클래스

- Fields:
  - public static DatabaseManager instance: DatabaseManager 인스턴스
  - Dictionary<int, Dialogue> dialogueDic: 다이얼로그 사전
  - public static bool isFinish: 다이얼로그 완료 여부
  - [SerializeField] DialogueEvent dialogue: 다이얼로그 이벤트
  - [SerializeField] GameObject dialogUI: 다이얼로그 UI 오브젝트
  - [SerializeField] TextMeshProUGUI dialogText: 다이얼로그 텍스트 UI
  - [SerializeField] float textDelay: 텍스트 출력 딜레이
  - [SerializeField] GameObject ManualUI: 안내 UI
  - [SerializeField] GameObject buildManualUI: 빌드 안내 UI
  - [SerializeField] GameObject nextButton: 다음 버튼
  - [SerializeField] GameObject leftRayController: 왼쪽 Ray 컨트롤러
  - [SerializeField] GameObject rightRayController: 오른쪽 Ray 컨트롤러
  - Dialogue[] dialogues: 다이얼로그 배열
  - bool isNext: 다음 대사 여부
  - int lineCount: 대화 카운트
  - int contextCount: 대사 카운트
  - public GameObject dialog: 다이얼로그 오브젝트
  - bool isDialogue: 다이얼로그 활성화 여부
  - bool skip: 스킵 여부
  - private bool gamestart: 게임 시작 여부

- Methods:
  - public Dialogue[] GetDialogue(int id): 특정 ID의 다이얼로그를 반환하는 메서드
  - void EndDialogue(): 다이얼로그를 종료하는 메서드
  - public void ShowDialogue(Dialogue[] p_dialogues): 다이얼로그를 보여주는 메서드
  - IEnumerator TypeWriter(): 텍스트를 출력하는 코루틴 함수
  - public void skipLine(): 다음 대사로 넘기는 메서드
  - public bool SendOnDialog(): 다이얼로그 활성화 여부를 반환하는 메서드
  - public void CloseManualUI(): 안내 UI를 닫는 메서드
  - public void ShowSelectButtonUI(): 선택 버튼 UI를 보여주는 메서드
  - public void SetClearUI(): 클리어 UI를 설정하는 메서드
  - public bool SendStart(): 게임 시작 여부를 반환하는 메서드
