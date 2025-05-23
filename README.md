플레이어:
  1) Move Speed: 플레이어 이동속도
  2) Jump Power: 플레이어 점프력
  3) Fall Speed: 낙하한 시간에 비례하는 낙하 속도 기본 값
  4) Charge Cost: 충전 스테미나 소모량
  5) Charging Speed: 점프 충전 속도
  6) Max Charge: 최대 충전량
     
아이템:
  1) 청사과: 플레이어의 체력을 회복시킵니다.
  2) 바나나: 잠시 플레이어의 점프 충전 속도를 상승시킵니다.
  3) 컵케잌: 위치에 관계없이 사용 시점으로부터 높게 날아오릅니다.

발판(Platform):
  1) is Clear: 발판을 밟는다면 게임이 종료되며 Rerty버튼을 활성화합니다.
  2) is Rotate: 발판이 Y축을 기준으로 지정한 속도로 회전합니다. 
  3) is Bradkable: 발판을 밟으면 지정한 시간동안 발판이 흔들리고 시간이 다 되면 발판이 제거됩니다.
  4) is Hot: 발판 위 플레이어의 체력을 지정한 값만큼 지속적으로 감소시킵니다.



핵심 기능

점프 충전
  *위치: Player 폴더 > PlayerController.cs > ChargigJump(),JumpCancle()
  
3인칭 카메라
  *위치: Camera 폴더 > PlayerCamera.cs
