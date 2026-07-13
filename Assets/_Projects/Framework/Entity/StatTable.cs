using System;

public static class StatTable
{
    // 효과 구현하는 사람들은 이 곱한 값을 효과에 적용하고
    // 캐릭터 구현하는 사람들은 이 값을 조정하고
    // 그런데 static class는 게임 꺼질때까지 값 초기화 안되니까 알아서 해줘야 함

    public static float LoseEffectMultiplier { get; set; } = 1.0f;
    public static float WinEffectMultiplier { get; set; } = 1.0f;
    public static float DrawEffectMultiplier { get; set; } = 1.0f;

    // . . .
}