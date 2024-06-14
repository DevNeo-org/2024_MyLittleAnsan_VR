# 2024_MyLittleAnsan_VR
# 🎯프로젝트 소개
> My Little Ansan은 안산산업역사박물관에서의 전시 경험을 VR을 통해 증강하기 위해 설계된 프로젝트입니다.
### 🎯개발 배경
> 안산 산업 역사박물관에 방문하여 조사해 본 결과 안산의 산업 발전역사에 대해서는 글과 사진으로만 소개되어 있어 어린이 방문객들이 이해하기에 `어렵고 지루할 수 있다` 는 문제점을 발견했습니다.   
> VR 게임 내에서 안산시의 `도시디자이너` 가 되어 시의 산업 발전 과정을 더욱 생생하고 재미있게 느낄 수 있는 게임을 기획하게 되었습니다.
### 🎯개발 환경
> 타겟층 안산산업역사박물관에 방문한 초등학교 저학년 어린이   
> 엔진: Unity 2022.3.23f1   
> 언어: C#   
> 사용장비: Meta Quest 2   
> VCS: Github   
> Tool: `Meta XR All-In-One SDK`(유니티 VR 개발), `XR Interaction Toolkit`(유니티 VR 시뮬레이션), `Unity ProBuilder`(오브젝트 모델링)

# 👨‍👦‍👦팀 소개
|이름|직무|담당개발|
|------|---|---|
|김나형|PM 및 개발자|도시건설기능, 씬 공유 데이터 관리|
|박준선|개발자|전자산업체험, 사용자 테스트|
|김종하|개발자|자동차산업체험, 메뉴UI|
|전우진|개발자|섬유산업체험, 에셋 디자인|

# 📌목차
- [설치 및 실행 방법](#설치-및-실행-방법)
- [프로젝트 구조](#프로젝트-구조)
- [문서](#문서)
- [사용자 매뉴얼](#사용자-매뉴얼)
- [외부 에셋](#외부-에셋)
- [기타](#기타)
      
# ⚙설치 및 실행 방법
1. 레포지토리 클론 `https://github.com/DevNeo-org/2024_MyLittleAnsan_VR.git`
2. Unity 버전 `2022.3.23f1`에서 프로젝트 실행
3. 메타 퀘스트:  `File` - `Build Settings` - `Android` - `Build`
4. 생성된 APK파일을 __메타 퀘스트 디벨로퍼 허브__ 를 통해 퀘스트 기기에 설치   
   `https://developer.oculus.com/meta-quest-developer-hub/`
5. 퀘스트 앱 목록 중 `알수 없는 출처`에서 탐색 후 실행

# 📁프로젝트 구조
```
📂Assets // 추후 수정 예정
 ┣ 📂Audio
 ┣ 📂Image
 ┣ 📂License Free Assets
 ┣ 📂Oculus
 ┣ 📂Plugins
 ┣ 📂Prefab
 ┣ 📂Resources
 ┣ 📂Samples
 ┣ 📂Scenes
 ┃ ┣ 📂AutomobIndScene // 담당: 김종하
 ┃ ┣ 📂CityDesign // 담당: 김나형
 ┃ ┣ 📂Electronic // 담당: 박준선
 ┃ ┣ 📂TextileIndScene // 담당: 전우진
 ┃ ┗ 📂Title // 담당: 김종하
 ┣ 📂Script
 ┣ 📂Settings
 ┣ 📂Skybox
 ┣ 📂TextMesh Pro
 ┣ 📂Tree 1_Textures
 ┣ 📂Tree 1_Textures
 ┣ 📂Tree_Textures
 ┣ 📂UnityFactorySceneHDRP
 ┣ 📂XR
 ┗ 📂XRI
📂Documents
📂Packages
📂ProjectSettings
```
# 📄문서
> 🔗[동작시나리오](https://drive.google.com/file/d/1-t_zbweEWNYh9zIoSPi7PHAw1hgHfJox/view?usp=sharing)
> 
> 🔗[개발 일정](https://github.com/orgs/DevNeo-org/projects/4)
> 
> 🔗[전체 플로우차트](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%ED%94%8C%EB%A1%9C%EC%9A%B0%EC%B0%A8%ED%8A%B8.png)
> 
> 🔗[개별 플로우차트](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%ED%94%8C%EB%A1%9C%EC%9A%B0%EC%B0%A8%ED%8A%B8.png)
> 
> 🔗[클래스 다이어그램](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_UML.png)
> 
> 🔗[기술문서 (개발자)](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_기술문서_개발자.md)
>
# 🧒사용자 매뉴얼
# 📥외부 에셋
- 타이틀 로고   
https://assetstore.unity.com/packages/2d/gui/icons/20-logo-templates-with-customizable-psd-vector-sources-174999

- 다이얼로그 및 메뉴 UI   
https://assetstore.unity.com/packages/2d/gui/extra-clean-ui-138812

- 효과음(건설 및 UI)   
https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116

- 도시디자인씬 환경   
https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552

- 메인 건물   
https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899

- 건설 및 클리어 효과 파티클   
https://assetstore.unity.com/packages/vfx/particles/hyper-casual-fx-200333

- 박수 효과음   
https://soundeffect-lab.info/

- 메인 자동차   
Assets/License Free Assets/Stylized Vehicles Pack Free   
https://assetstore.unity.com/packages/3d/vehicles/land/stylized-vehicles-pack-free-150318

- 렌치   
Assets/License Free Assets/Monqo Pipe Wrench   
https://assetstore.unity.com/packages/3d/props/tools/free-pipe-wrench-187070

- 환경 배치용 자동차   
Assets/License Free Assets/Azerilo   
https://assetstore.unity.com/packages/3d/vehicles/land/hd-low-poly-racing-car-no-1201-118603

- 자동차 부품   
Assets/License Free Assets/Junk Car Parts   
https://assetstore.unity.com/packages/3d/props/junkyard-car-parts-119065

- 스카이박스   
Assets/Skybox   
https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-sky-61217

- 자동차 산업 BGM   
Assets/Audio   
`"Too Cool" Kevin MacLeod (incompetech.com)`   
`Licensed under Creative Commons: By Attribution 4.0 License`   
`http://creativecommons.org/licenses/by/4.0/`

- 타이틀, 도시 디자인 BGM
Assets/Audio  
`"Happy Bee" Kevin MacLeod (incompetech.com)`   
`Licensed under Creative Commons: By Attribution 4.0 License`   
`http://creativecommons.org/licenses/by/4.0/`







# 📕기타
- **한양대학교 ERICA 캠퍼스 컴퓨터학부 가상및증강현실프로그래밍 수업의 IC-PBL 팀 프로젝트입니다.*

