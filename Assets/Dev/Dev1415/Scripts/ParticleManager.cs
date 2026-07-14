using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance = null; // 메모리 할당 변수

    [Header("VFX Prefabs")]
    public GameObject buffUpVFX;      // HCFX_Buff_Up - 카드 효과 발동 성공
    public GameObject buffDownVFX;    // HCFX_Buff_Down - 디버프 / 효과 실패
    public GameObject energy05VFX;    // HCFX_Energy_05 - 카드 효과 발동
    public GameObject energy08VFX;    // HCFX_Energy_08 - 회복 효과
    public GameObject hit03VFX;       // HCFX_Hit_03 - 캐릭터 타격
    public GameObject hit08VFX;       // HCFX_Hit_08 - 카드 교전
    public GameObject stunVFX;        // HCFX_Stun 캐릭 피격 시 스턴 표시



    //공격 트레일
    [SerializeField] private GameObject attackTrailPrefab;
    public void PlayAttackTrail(Vector2 startPos, Vector2 endPos)
    {
        PlayTrail(attackTrailPrefab,hit08VFX,
            startPos,
            endPos,
            0.5f);

    }
    private void Awake()  // singleton pattern
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }

    public void PlayVFX(GameObject vfxPrefab, Vector2 position) // play VFX
    {
        if (vfxPrefab == null)
        {
            Debug.Log("vfx Prefab is Empty!");
            return;
        }

        Vector3 spawnPos = new Vector3(position.x, position.y, 0f);
        Instantiate(vfxPrefab, spawnPos, Quaternion.identity);
    }




    public void PlayTrail(GameObject trailPrefab, GameObject endVFXPrefab, Vector2 startPos, Vector2 endPos, float duration)
    {// 트레일, 끝 VFX , 시작 위치 , 끝 위치 , 지속시간
        Vector3 spawnPos = new Vector3(startPos.x, startPos.y, 0f); //시작점의 x,시작점 y Vector3 형태로 바꿈

        GameObject trailObject = Instantiate(trailPrefab, spawnPos, Quaternion.identity);
        trailObject.transform.DOMove(endPos, duration).OnComplete(() => //트레일 무브 후 VFX 발동
        { PlayVFX(endVFXPrefab, endPos); }); //끝 VFX , // VFX 발동 위치

    }


    public void PlayBuffUpVFX(Vector2 position)
    {
        PlayVFX(buffUpVFX, position);
    }
    public void PlayBuffDownVFX(Vector2 position)
    {
        PlayVFX(buffDownVFX, position);
    }
    public void PlayEnergy05VFX(Vector2 position)
    {
        PlayVFX(energy05VFX, position);
    }
    public void PlayEnergy08VFX(Vector2 position)
    {
        PlayVFX(energy08VFX, position);
    }
    public void PlayHit03VFX(Vector2 position)
    {
        PlayVFX(hit03VFX, position);
    }
    public void PlayHit08VFX(Vector2 position)
    {
        PlayVFX(hit08VFX, position);
    }
    public void PlayStunVFX(Vector2 position)
    {
        PlayVFX(stunVFX, position);
    }


    public void PlayHitCombo(Vector2 position) // 카드 격돌 VFX 난투
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            PlayHit08VFX(position); // VFX 호출
        });

        seq.AppendInterval(0.1f); // VFX 대기

        seq.AppendCallback(() =>
        {
            PlayHit08VFX(position);
        });

        seq.AppendInterval(0.1f);

        seq.AppendCallback(() =>
        {
            PlayHit08VFX(position);
        });

    }




}