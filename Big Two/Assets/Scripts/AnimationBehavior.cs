using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehavior : MonoBehaviour
{
    new Animation animation;

    float WaitTimeIddle;
    public MonsterType monsterType;
    public MonsterState monsterState;
    public string MonsterName;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        monsterState = MonsterState.iddle;
        WaitTimeIddle = 0;
        switch (monsterType)
        {
            case MonsterType.orc:
                MonsterName = "Orc";
                break;
            case MonsterType.dragon:
                MonsterName = "Dragon";                
                break;
            case MonsterType.ghost:
                MonsterName = "Ghost";
                break;
            case MonsterType.mummy:
                MonsterName = "Mummy";
                break;
        }

        AppManager.gameState = GameState.SelectingChar;
        StartCoroutine(Playing());
    }

    IEnumerator Playing()
    {
        while (true && AppManager.gameState!=GameState.Pause)
        {
            switch (monsterState)
            {
                case MonsterState.iddle:
                    switch (monsterType)
                    {
                        case MonsterType.orc:
                            WaitTimeIddle -= Time.deltaTime;                            
                            if (WaitTimeIddle <= 0)
                            {
                                int MyRandom = Random.Range(0, 2);
                                switch (MyRandom) {
                                    case 0: animation.Play("orc_idle"); break;
                                    case 1: animation.Play("orc_talk"); break;                                    
                                }
                                
                                WaitTimeIddle = Random.Range(2, 5);
                            }
                            break;
                        case MonsterType.dragon:
                            WaitTimeIddle -= Time.deltaTime;
                            if (WaitTimeIddle <= 0)
                            {
                                int MyRandom = Random.Range(0, 2);
                                switch (MyRandom)
                                {
                                    case 0: animation.Play("dragon_idle"); break;
                                    case 1: animation.Play("dragon_talk"); break;                                    
                                }

                                WaitTimeIddle = Random.Range(2, 5);
                            }                            
                            break;
                        case MonsterType.ghost:
                            WaitTimeIddle -= Time.deltaTime;
                            if (WaitTimeIddle <= 0)
                            {
                                int MyRandom = Random.Range(0, 3);
                                switch (MyRandom)
                                {
                                    case 0: animation.Play("ghost_idle_hover"); break;
                                    case 1: animation.Play("ghost_idle_back_and_forth"); break;
                                    case 2: animation.Play("ghost_talk"); break;
                                }

                                WaitTimeIddle = Random.Range(2, 5);
                            }
                            break;
                        case MonsterType.mummy:
                            WaitTimeIddle -= Time.deltaTime;
                            if (WaitTimeIddle <= 0)
                            {
                                int MyRandom = Random.Range(0, 3);
                                switch (MyRandom)
                                {
                                    case 0: animation.Play("mummy_idle1"); break;
                                    case 1: animation.Play("mummy_idle2"); break;
                                    case 2: animation.Play("mummy_talk"); break;
                                }

                                WaitTimeIddle = Random.Range(2, 5);
                            }
                            break;
                    }
                    break;
                case MonsterState.win:
                    switch (monsterType)
                    {
                        case MonsterType.orc:
                            if(!animation.IsPlaying("orc_laugh"))
                                animation.Play("orc_laugh"); 
                            break;
                        case MonsterType.dragon:
                            if (!animation.IsPlaying("dragon_laugh"))
                                animation.Play("dragon_laugh"); 
                            break;
                        case MonsterType.ghost:
                            if (!animation.IsPlaying("ghost_laugh"))
                                animation.Play("ghost_laugh");
                            break;
                        case MonsterType.mummy:
                            if (!animation.IsPlaying("mummy_laugh"))
                                animation.Play("mummy_laugh");
                            break;
                    }
                    break;

                case MonsterState.loose:
                    switch (monsterType)
                    {
                        case MonsterType.orc:
                            if (!animation.IsPlaying("orc_panic"))
                                animation.Play("orc_panic");
                            break;
                        case MonsterType.dragon:
                            if (!animation.IsPlaying("dragon_panic"))
                                animation.Play("dragon_panic");
                            break;
                        case MonsterType.ghost:
                            if (!animation.IsPlaying("ghost_panic"))
                                animation.Play("ghost_panic");
                            break;
                        case MonsterType.mummy:
                            if (!animation.IsPlaying("mummy_panic"))
                                animation.Play("mummy_panic");
                            break;
                    }
                    break;
            }
            yield return null;
        }
    }

    public void SetAnimation(MonsterState _monsterState) {
        monsterState = _monsterState;
        if (monsterState == MonsterState.iddle) {
            WaitTimeIddle = 0;//force to start iddle
        }
    }
    
}
