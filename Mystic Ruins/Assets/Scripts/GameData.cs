using System;

[Serializable]
public class GameData
{
    public int bossSceneLoaded;

    public float x = -1.3f, y = 0, z = -40f;

    public int[] cameraTrans = new int[6] { 40, 0, 0, 0, 15, -13 };
    public float yAngle;

    public int[] items = new int[4];

    public int[] skillStates = new int[3];

    public int coalNum;
    public int wheelNum;

    public float localWaveY;

    public float[] cartPos = new float[3];

    public int[] pulleyState = new int[4];
    //(0)������ �ϰ� �� / (1)������ �ϰ� �� / (2)������ ��� ��

    public int currentMapValue;

    public int[] mapProgress = new int[9];
    //�ε��� - ���̸� : (��)����
    //0 - Ʃ�丮�� : (0)Ʃ�丮�� �Ϸ� �� / (1)Ʃ�丮�� �Ϸ� ��
    //1 - ȸ������ : (0)�б� ����
    //2 - ���� : (0)īƮ ���� �� / (1)īƮ ���� ��
    //3 - ȭ�� : (0)�� ���� ��� �� / (1)�� ���� ��� ��
    //4 - ������ : (0)��ٸ� ��ġ �� / (1)��ٸ� ��ġ �� / (2)���� ���� �� / (3)���� �ذ� ��
    //5 - �������� : (0)���� �ذ� �� / (1)���� �ذ� ��
    //6 - �������� : (0)������ �ذ� �� / (1)�ϳ��� ������ �ذ� ��
    //7 - ������ : (0)��ź �߰� �� / (1)��ź �߰� �� / (2)���� ���� ��
    //8 - ������ : (0)���� Ȱ��ȭ �� / (1)���� Ȱ��ȭ �� / (2)���� óġ ��
}
