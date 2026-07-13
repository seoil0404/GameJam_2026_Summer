using System;
using UnityEngine;

public static class EnemyStateBridge
{
    public static event Action OnAllocateComplete;

    public static void StartAllocate()
    {
        
    }

    private static void Example()
    {
        // 카드 배치가 끝났을때는 아래처럼
        OnAllocateComplete.Invoke();

        // StartAllocate가 호출되면 아래 객체들을 참고해서 카드 배치하기

        // *손패 접근 (List<CardData>로 반환)
        var hands = HandManager.Instance.EnemyHands;

        // *필드 접근
        var field = FieldManager.Instance.EnemyField;

        /*
        손패에서 카드를 꺼내서 필드에 카드를 넣는 로직을 구현해야하는데, 
        처음 라운드에는 EnemyField에서 필드가 다 null이니까 그걸 감지해서
        처음 라운드일때는 손패에서 4개 꺼내서 배치하면 되고
        처음 라운드가 아닐 때에는 손패에서 새로운걸 꺼내서 필드에 넣을지,
        아니면 필드 카드끼리 교체할지를 너의 AI가 선택해야 함
        손패나 필드에서 따로 교환 로직은 없고 너가 바꾸는대로 다 바뀌는거라 너가 그런 로직까지 만들면 될듯
        */
    }
}
