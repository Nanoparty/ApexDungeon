using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Skill;

[CreateAssetMenu(fileName = "SkillGenerator", menuName = "ScriptableObjects/Skill Generator")]
public class SkillGenerator : ScriptableObject
{
    // Icons
    public Sprite Fireball_icon;
    public Sprite IceSpike_icon;
    public Sprite LightningBolt_icon;
    public Sprite PoisonJet_icon;
    public Sprite Restore_icon;
    public Sprite Cleanse_icon;
    public Sprite Plague_icon;
    public Sprite Lacerate_icon;
    public Sprite Bless_icon;
    public Sprite Teleport_icon;
    public Sprite ArmorPolish_icon;
    public Sprite Berserk_icon;
    public Sprite FieldDress_icon;
    public Sprite LifeDrain_icon;
    public Sprite ManaDrain_icon;
    public Sprite Hypnosis_icon;
    public Sprite Silence_icon;
    public Sprite Stun_icon;
    public Sprite WhirlwindStrike_icon;
    public Sprite Thrust_icon;
    public Sprite Bash_icon;
    public Sprite Stomp_icon;
    public Sprite Bite_icon;
    public Sprite Slash_icon;
    public Sprite Scratch_icon;
    public Sprite Pound_icon;
    public Sprite Headbutt_icon;
    public Sprite Trap_icon;
    public Sprite MagicMissile_icon;
    public Sprite BloodCurse_icon;
    public Sprite FlamePalm_icon;
    public Sprite IcePalm_icon;
    public Sprite PoisonPalm_icon;
    public Sprite StaticPalm_icon;
    public Sprite Bind_icon;
    public Sprite Stealth_icon;
    public Sprite Taunt_icon;
    public Sprite Invisibility_icon;

    public Skill GetSkill(SkillType st)
    {
       
        var skills = new Dictionary<SkillType, Skill>();
        skills[SkillType.Fireball] = Fireball;
        skills[SkillType.IceShard] = IceShard;
        skills[SkillType.LightningBolt] = LightningBolt;
        skills[SkillType.PoisonSpike] = PoisonSpike;
        skills[SkillType.WhirlwindStrike] = WhirlwindStrike;
        skills[SkillType.Thrust] = Thrust;
        skills[SkillType.Bash] = Bash;
        skills[SkillType.Stomp] = Stomp;
        skills[SkillType.Bite] = Bite;
        skills[SkillType.Slash] = Slash;
        skills[SkillType.Scratch] = Scratch;
        skills[SkillType.Pound] = Pound;
        skills[SkillType.Headbutt] = Headbutt;
        skills[SkillType.Trap] = Trap;
        skills[SkillType.MagicMissile] = MagicMissile;
        skills[SkillType.LifeDrain] = LifeDrain;
        skills[SkillType.ManaDrain] = ManaDrain;
        skills[SkillType.BloodCurse] = BloodCurse;
        skills[SkillType.FlamePalm] = FlamePalm;
        skills[SkillType.IcePalm] = IcePalm;
        skills[SkillType.StaticPalm] = StaticPalm;
        skills[SkillType.PoisonPalm] = PoisonPalm;
        skills[SkillType.Hypnosis] = Hypnosis;
        skills[SkillType.Lacerate] = Lacerate;
        skills[SkillType.Stun] = Stun;
        skills[SkillType.ArmorPolish] = ArmorPolish;
        skills[SkillType.FieldDress] = FieldDress;
        skills[SkillType.Berserk] = Berserk;
        skills[SkillType.Bind] = Bind;
        skills[SkillType.Silence] = Silence;
        skills[SkillType.Bless] = Bless;
        skills[SkillType.Plague] = Plague;
        skills[SkillType.Stealth] = Stealth;
        skills[SkillType.Taunt] = Taunt;
        //skills[SkillType.Invisibility] = Invisibility;
        skills[SkillType.Teleport] = Teleport;
        skills[SkillType.Restore] = Restore;
        skills[SkillType.Cleanse] = Cleanse;

        return skills[st];
    }

    public Skill Fireball
    {
        get { return new Skill(Skill.SkillType.Fireball, Fireball_icon); }
    }
    public Skill IceShard
    {
        get { return new Skill(Skill.SkillType.IceShard, IceSpike_icon); }
    }
    public Skill LightningBolt
    {
        get { return new Skill(Skill.SkillType.LightningBolt, LightningBolt_icon); }
    }
    public Skill PoisonSpike
    {
        get { return new Skill(Skill.SkillType.PoisonSpike, PoisonJet_icon); }
    }
    public Skill FlamePalm
    {
        get { return new Skill(Skill.SkillType.FlamePalm, FlamePalm_icon); }
    }
    public Skill IcePalm
    {
        get { return new Skill(Skill.SkillType.IcePalm, IcePalm_icon); }
    }
    public Skill StaticPalm
    {
        get { return new Skill(Skill.SkillType.StaticPalm, StaticPalm_icon); }
    }
    public Skill PoisonPalm
    {
        get { return new Skill(Skill.SkillType.PoisonPalm, PoisonPalm_icon); }
    }
    public Skill Restore
    {
        get { return new Skill(Skill.SkillType.Restore, Restore_icon); }
    }
    public Skill Cleanse
    {
        get { return new Skill(Skill.SkillType.Cleanse, Cleanse_icon); }
    }
    public Skill Pound
    {
        get { return new Skill(Skill.SkillType.Pound, Pound_icon); }
    }
    public Skill Bite
    {
        get { return new Skill(Skill.SkillType.Bite, Bite_icon); }
    }
    public Skill Scratch
    {
        get { return new Skill(Skill.SkillType.Scratch, Scratch_icon); }
    }
    public Skill Stomp
    {
        get { return new Skill(Skill.SkillType.Stomp, Stomp_icon); }
    }
    public Skill Headbutt
    {
        get { return new Skill(Skill.SkillType.Headbutt, Headbutt_icon); }
    }
    public Skill Thrust
    {
        get { return new Skill(Skill.SkillType.Thrust, Thrust_icon); }
    }
    public Skill Slash
    {
        get { return new Skill(Skill.SkillType.Slash, Slash_icon); }
    }
    public Skill Plague
    {
        get { return new Skill(Skill.SkillType.Plague, Plague_icon); }
    }
    public Skill Lacerate
    {
        get { return new Skill(Skill.SkillType.Lacerate, Lacerate_icon); }
    }
    public Skill Bless
    {
        get { return new Skill(Skill.SkillType.Bless, Bless_icon); }
    }
    public Skill Teleport
    {
        get { return new Skill(Skill.SkillType.Teleport, Teleport_icon); }
    }
    public Skill ArmorPolish
    {
        get { return new Skill(Skill.SkillType.ArmorPolish, ArmorPolish_icon); }
    }
    public Skill Berserk
    {
        get { return new Skill(Skill.SkillType.Berserk, Berserk_icon); }
    }
    public Skill FieldDress
    {
        get { return new Skill(Skill.SkillType.FieldDress, FieldDress_icon); }
    }
    public Skill LifeDrain
    {
        get { return new Skill(Skill.SkillType.LifeDrain, LifeDrain_icon); }
    }
    public Skill ManaDrain
    {
        get { return new Skill(Skill.SkillType.ManaDrain, ManaDrain_icon); }
    }
    public Skill Hypnosis
    {
        get { return new Skill(Skill.SkillType.Hypnosis, Hypnosis_icon); }
    }
    public Skill Silence
    {
        get { return new Skill(Skill.SkillType.Silence, Silence_icon); }
    }
    public Skill Stun
    {
        get { return new Skill(Skill.SkillType.Stun, Stun_icon); }
    }
    public Skill WhirlwindStrike
    {
        get { return new Skill(Skill.SkillType.WhirlwindStrike, WhirlwindStrike_icon); }
    }
    public Skill Bash
    {
        get { return new Skill(Skill.SkillType.Bash, Bash_icon); }
    }
    public Skill Trap
    {
        get { return new Skill(Skill.SkillType.Trap, Trap_icon); }
    }
    public Skill MagicMissile
    {
        get { return new Skill(Skill.SkillType.MagicMissile, MagicMissile_icon); }
    }
    public Skill BloodCurse
    {
        get { return new Skill(Skill.SkillType.BloodCurse, BloodCurse_icon); }
    }
    public Skill Bind
    {
        get { return new Skill(Skill.SkillType.Bind, Bind_icon); }
    }
    public Skill Stealth
    {
        get { return new Skill(Skill.SkillType.Stealth, Stealth_icon); }
    }
    public Skill Taunt
    {
        get { return new Skill(Skill.SkillType.Taunt, Taunt_icon); }
    }
    //public Skill Invisibility
    //{
    //    get { return new Skill(Skill.SkillType.Invisibility, Invisibility_icon); }
    //}


}

