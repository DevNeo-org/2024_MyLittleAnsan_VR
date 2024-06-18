# 2024_MyLittleAnsan_VR
# 🎯프로젝트 소개
> My Little Ansan은 안산산업역사박물관에서의 전시 경험을 VR을 통해 증강하기 위해 설계된 프로젝트입니다.
### 🎯개발 배경
> 안산 산업 역사박물관에 방문하여 조사해 본 결과 안산의 산업 발전역사에 대해서는 글과 사진으로만 소개되어 있어 어린이 방문객들이 이해하기에 `어렵고 지루할 수 있다` 는 문제점을 발견했습니다.   
> VR 게임 내에서 안산시의 `도시디자이너` 가 되어 시의 산업 발전 과정을 더욱 생생하고 재미있게 느낄 수 있는 게임을 기획하게 되었습니다.
### 🎯개발 환경
> - 타겟층: 안산산업역사박물관에 방문한 `초등학교 저학년` 어린이   
> - 엔진: `Unity 2022.3.23f1`   
> - 언어: `C#`   
> - 사용장비: `Meta Quest 2`   
> - Tool: `Meta XR All-In-One SDK`(유니티 VR 개발), `XR Interaction Toolkit`(유니티 VR 시뮬레이션), `Unity ProBuilder`(오브젝트 모델링)

# 👨‍👦‍👦팀 소개
|이름|직무|담당개발|
|------|---|---|
|김나형|PM 및 개발자|도시건설기능, 씬 공유 데이터 관리|
|박준선|개발자|전자산업체험, 사용자 테스트|
|김종하|개발자|자동차산업체험, 메뉴UI|
|전우진|개발자|섬유산업체험, 에셋 디자인|

# 📌목차
- [프로젝트 구조](#프로젝트-구조)
- [설치 및 실행 방법](#설치-및-실행-방법)
- [사용자 매뉴얼](#사용자-매뉴얼)
- [문서](#문서)
- [외부 에셋](#외부-에셋)
- [기타](#기타)

# 📁프로젝트 구조
```
📂Assets 
 ┣ 📂Audio
 ┣ 📂Image
 ┣ 📂License Free Assets
 ┣ 📂Oculus
 ┣ 📂Plugins
 ┣ 📂Prefab
 ┣ 📂Resources
 ┃ ┣📂Skybox
 ┣ 📂Samples
 ┣ 📂Scenes
 ┃ ┣ 📂AutomobIndScene // 담당: 김종하
 ┃ ┣ 📂CityDesign // 담당: 김나형
 ┃ ┣ 📂Electronic // 담당: 박준선
 ┃ ┣ 📂TextileIndScene // 담당: 전우진
 ┃ ┗ 📂Title // 담당: 김종하
 ┣ 📂Script
 ┣ 📂Settings
 ┣ 📂TextMesh Pro
 ┣ 📂XR
 ┗ 📂XRI
📂Documents
📂Packages
📂ProjectSettings
```
      
# ⚙설치 및 실행 방법
1. 레포지토리 클론 `https://github.com/DevNeo-org/2024_MyLittleAnsan_VR.git`
2. Unity 버전 `2022.3.23f1`에서 프로젝트 실행
3. 메타 퀘스트:  `File` - `Build Settings` - `Android` - `Build`   
   ![BuildHowTo](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/assets/113578212/8401674e-1f8d-4362-93b1-d6f8b7b3ae89 "HowToBuild")
5. 생성된 APK파일을 __메타 퀘스트 디벨로퍼 허브__ 를 통해 퀘스트 기기에 설치   
   `https://developer.oculus.com/meta-quest-developer-hub/`
6. 퀘스트 앱 목록 중 `알수 없는 출처`에서 탐색 후 실행
# 🧒사용자 매뉴얼
### ● 전체 사용자 기술문서
> 🔗[기술문서 (사용자)](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_기술문서_사용자.md)
### ● 타이틀 씬
 게임 실행 시 타이틀 씬에서 시작하게 됩니다.
```
1. [게임 시작] 버튼을 눌러 도시 디자인 씬으로 이동합니다.
```
### ● 도시 디자인 씬
 도시 디자인 씬에서 플레이어는 나만의 안산을 설계할 수 있습니다. 총 3가지 체험을 각각 마칠때 마다 도시 디자인 씬으로 돌아와 원하는 건물을 짓습니다.

 * 도시 건설
```
 1. 각 체험을 마칠 때마다 도시에 건물을 1개 지을 수 있습니다.
 2. 주변에 있는 건물을 하나 골라 **그랩 버튼**으로 들어서 파란색 구역에 놓으면 건물이 건설됩니다.
 3. 총 3번의 체험을 모두 마치고 도시 건설을 완료하면 게임이 종료됩니다.
```
### ● 자동차 산업 체험
자동차 산업 체험에서는 자동차를 조립하는 체험이 진행됩니다. 4방향에서 오는 자동차 부품을 들고 있는 렌치로 맞추면 자동차를 조립할 수 있습니다.

 * 플레이 방법
```
 1. 게임 시작 후 4방향에서 다가오는 부품을 렌치로 hitbox 안에서 맞출 시 점수를 획득합니다.
 2. 총 20점을 획득하거나, 제한 시간 내에 10점 이상 획득하면 게임이 클리어되며, [클리어] 버튼을 눌러 도시 디자인 씬으로 돌아갑니다.
 3. 10점 미만을 획득하거나 다시 플레이하고 싶은 경우, 메뉴의 [체험 재시작] 버튼을 눌러 체험을 다시 시작할 수 있습니다.
```
### ● 전기전자 산업 체험
전기전자 산업 체험에서는 고장 난 PCB보드판을 수리하는 체험이 진행됩니다. 9개의 홈 중 무작위로 고장 난 부분이 나타나며, 들고 있는 인두기를 1초 이상 갖다 대면 수리할 수 있습니다.

 * 플레이 방법
```
 1. 게임 시작 후 PCB보드판에 무작위로 점등되는 부분에 인두기를 1초 이상 갖다 대 수리합니다.
 2. 제한 시간 내에 10점 이상 획득하면 게임이 클리어되며, [클리어] 버튼을 눌러 도시 디자인 씬으로 돌아갑니다.
 3. 10점 미만을 획득하거나 다시 플레이하고 싶은 경우, 메뉴의 [체험 재시작] 버튼을 눌러 체험을 다시 시작할 수 있습니다.
```
### ● 섬유 산업 체험
섬유 산업 체험에서는 옷을 직접 색칠하여 완성하는 체험이 진행됩니다. 거대한 옷의 앞면에 페인트총을 쏘면 옷을 색칠할 수 있습니다.

 * 플레이 방법
```
 1. 게임 시작 후 오른손에 들고 있는 페인트 총을 조준하여 트리거 버튼을 누르면 페인트를 발사하며, 옷에 페인트가 닿으면 색칠됩니다.
 2. 주변에 있는 페인트통에 총을 담글 경우 해당 색으로 페인트 색이 변경됩니다.
 3. 제한 시간이 모두 지나면 자동으로 게임이 클리어되며, [클리어] 버튼을 눌러 도시 디자인 씬으로 돌아갑니다.
 4. 게임을 다시 플레이하고 싶은 경우, 메뉴의 [체험 재시작] 버튼을 눌러 체험을 다시 시작할 수 있습니다.
```
# 📄문서
> 🔗[동작시나리오](https://drive.google.com/file/d/1-t_zbweEWNYh9zIoSPi7PHAw1hgHfJox/view?usp=sharing)
> 
> 🔗[개발 일정](https://github.com/orgs/DevNeo-org/projects/4)
> 
> 🔗[전체 플로우차트](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_%ED%94%8C%EB%A1%9C%EC%9A%B0%EC%B0%A8%ED%8A%B8.png)
> 
> 🔗[개별 플로우차트](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/%EC%B2%B4%ED%97%98%EB%B3%84_%EC%83%81%EC%84%B8_%ED%94%8C%EB%A1%9C%EC%9A%B0%EC%B0%A8%ED%8A%B8.png)
> 
> 🔗[클래스 다이어그램](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_UML.png)
> 
> 🔗[기술문서 (개발자)](https://github.com/DevNeo-org/2024_MyLittleAnsan_VR/blob/main/Documents/MyLittleAnsan_기술문서_개발자.md)
>

# 📥외부 에셋
<details>
<summary>외부 에셋 목록</summary>
<div markdown="1">


- 타이틀 로고   
Assets\Image   
https://assetstore.unity.com/packages/2d/gui/icons/20-logo-templates-with-customizable-psd-vector-sources-174999

- 다이얼로그 및 메뉴 UI   
Assets\License Free Assets\Extra Clean UI   
https://assetstore.unity.com/packages/2d/gui/extra-clean-ui-138812

- 효과음 (건설 및 UI, 총 발사)   
Assets\License Free Assets\CasualGameSounds   
https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116

- 도시디자인씬 환경   
Assets\License Free Assets\SimpleLowPolyNature   
https://assetstore.unity.com/packages/3d/environments/landscapes/simple-low-poly-nature-pack-157552

- 메인 건물   
Assets\License Free Assets\SimplePoly City - Low Poly Assets   
https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899

- 건설 및 클리어 효과 파티클   
Assets\License Free Assets\Lana Studio   
https://assetstore.unity.com/packages/vfx/particles/hyper-casual-fx-200333

- 각종 효과음(축포, Hit, 박수 등)   
Assets\Audio   
https://soundeffect-lab.info/

- 메인 자동차   
Assets\License Free Assets\Stylized Vehicles Pack Free   
https://assetstore.unity.com/packages/3d/vehicles/land/stylized-vehicles-pack-free-150318

- 렌치   
Assets\License Free Assets\Monqo Pipe Wrench   
https://assetstore.unity.com/packages/3d/props/tools/free-pipe-wrench-187070

- 환경 배치용 자동차   
Assets\License Free Assets\Azerilo   
https://assetstore.unity.com/packages/3d/vehicles/land/hd-low-poly-racing-car-no-1201-118603

- 자동차 부품   
Assets\License Free Assets\Junk Car Parts   
https://assetstore.unity.com/packages/3d/props/junkyard-car-parts-119065

- 효과음 (색 변경)   
Assets\License Free Assets\Rocklynn Productions - Water Splash Pack   
https://assetstore.unity.com/packages/audio/sound-fx/foley/water-splash-pack-14039

- 페인트 총 모델   
Assets\License Free Assets\Sci Fi Gun Heavy   
https://assetstore.unity.com/packages/3d/props/guns/sci-fi-gun-heavy-87878

- 페인트통 나무 양동이   
Assets\License Free Assets\StylizedWoodenBucket   
https://assetstore.unity.com/packages/3d/props/tools/stylized-western-wooden-bucket-191513

- 옷 텍스쳐   
Assets\License Free Assets\Yughues Fabric Materials   
https://assetstore.unity.com/packages/2d/textures-materials/fabric/yughues-free-fabric-materials-13002

- 스카이박스   
Assets\Skybox   
https://assetstore.unity.com/packages/2d/textures-materials/sky/free-hdr-sky-61217

- PCB 기판 Image   
Assets\License Free Assets\FLATICON   
https://www.flaticon.com/kr/free-icon/pcb-board_2779266?term=pcb&page=1&position=1&origin=search&related_id=2779266   

- 배경  
Assets\License Free Assets\Sci-Fi Styled Modular Pack   
https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913   

- 배경(로봇 팔)   
Assets\License Free Assets\UnityFactorySceneHDRP   
https://assetstore.unity.com/packages/3d/environments/industrial/unity-factory-276400    

- 원형파티클   
Assets\License Free Assets\Hovl Studio   
https://assetstore.unity.com/packages/vfx/particles/spells/magic-effects-free-247933   

- 불꽃파티클   
Assets\License Free Assets\UnityTechnologies   
https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325   

- 효과음
Assets\Audio   
✔ SFX provided by 셀바이뮤직
https://sellbuymusic.com/md/srjcfcb-jchtkhh

- 전자 산업 BGM   
Assets\Audio   
https://gongu.copyright.or.kr/gongu/wrt/wrt/view.do?wrtSn=13366540&menuNo=200020   

- 섬유 산업 BGM   
Assets\License Free Assets\Sci-FiGameBGM#6   
https://assetstore.unity.com/packages/audio/music/sci-fi-bgm-6-245973

- 자동차 산업 BGM   
Assets\Audio   
`"Too Cool" Kevin MacLeod (incompetech.com)`   
`Licensed under Creative Commons: By Attribution 4.0 License`   
`http://creativecommons.org/licenses/by/4.0/`

- 타이틀, 도시 디자인 BGM
Assets\Audio  
`"Happy Bee" Kevin MacLeod (incompetech.com)`   
`Licensed under Creative Commons: By Attribution 4.0 License`   
`http://creativecommons.org/licenses/by/4.0/`

- 폰트
Assets\TextMesh Pro\Fonts  
https://maplestory.nexon.com/Media/Font

</div>
</details>





# 📕기타
- **한양대학교 ERICA 캠퍼스 컴퓨터학부 가상및증강현실프로그래밍 수업의 IC-PBL 팀 프로젝트입니다.*
- 연락처   
  [김나형] knh000125@hanyang.ac.kr   
  [박준선] my9544@naver.com   
  [김종하] heemang918@hanyang.ac.kr   
  [전우진] jokey1344@gmail.com
