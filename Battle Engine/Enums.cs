using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public enum AnimationKey
    {
        Explosion,
        RedCat,
        FarmerWalkRight,
        Asteroid,
        Light,
        MeteorIdle,
        ExplosionMeteor,
        Vacina,
        Mandioca,
        Scracth,
        Slam,
        Poop,
        Scratch,

        GeorgeCimaAnim,
        GeorgeBaixoAnim,
        GeorgeDireitaAnim,
        GeorgeEsquerdaAnim,
        GeorgeParadoCimaAnim,
        GeorgeParadoBaixoAnim,
        GeorgeParadoDireitaAnim,
        GeorgeParadoEsquerdaAnim

    }

    public enum Mode
    {
        player,
        ship
    }

    public enum ModuleKey
    {
        ShowChoiceMenu,
        InitialText,
        PlayerAnimation,
        MonsterBlink,
        MonsterLifeBar,
        PlayerAction,
        CheckMonsterHealth,
        MonsterAnimation,
        PlayerBlink,
        PlayerLifeBar,
        MonsterAttack,
        CheckPlayerHealth,
        GameOverModule,

        ChooseActionModule
    }

    public enum ScreenKey
    {
        Screen1
    }
}
