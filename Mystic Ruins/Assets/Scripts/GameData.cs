using System;

[Serializable]
public class GameData
{
    public float x = 0, y = 0, z = 0;

    public int[] items = new int[4];

    public int currentMapValue;

    public int[] mapProgress = new int[9];
    //인덱스 - 맵이름 : (값)상태
    //0 - 튜토리얼 : (0)분기 없음
    //1 - 회전발판 : (0)분기 없음
    //2 - 광산 : (0)카트 조립 전 / (1)카트 조립 후
    //3 - 화로 : (0)불 원소 흡수 전 / (1)불 원소 흡수 후
    //4 - 수조룸 : (0)퍼즐 해결 전 / (1)퍼즐 해결 후
    //5 - 파이프룸 : (0)퍼즐 해결 전 / (1)퍼즐 해결 후
    //6 - 도르래룸 : (0)도르래 해결 전 / (1)하나의 도르래 해결 후
    //7 - 엔진룸 : (0)석탄 추가 전 / (1)석탄 추가 후 / (2)엔진 가열 후
    //8 - 보스룸 : (0)보스 활성화 전 / (1)보스 활성화 후 / (2)보스 처치 후
}
