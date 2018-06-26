﻿using Newtonsoft.Json.Linq;
using Archmage.Models.Modifiers;
namespace Archmage.Services
{
  public static class AbilityScoreService
  {
    private const string Modifiers = "Modifiers";

    public static bool TryGetDexterityModifiers(int abilityScore,
      out int reactionAdjustment, out int missileAttackAdjustment, out int defensiveAdjustment)
    {
      reactionAdjustment = int.MinValue;
      missileAttackAdjustment = int.MinValue;
      defensiveAdjustment = int.MinValue;

      if (!IsValidAbilityScore(abilityScore))
      {
        return false;
      }

      var dexterity = JObject.Parse(Properties.Resources.Dexterity);

      var modifiers = dexterity.GetValue(Modifiers)[abilityScore - 1];

      reactionAdjustment = modifiers.Value<int>(Dexterity.ReactionAdjustment.ToString());
      missileAttackAdjustment = modifiers.Value<int>(Dexterity.MissileAttackAdjustment.ToString());
      defensiveAdjustment = modifiers.Value<int>(Dexterity.DefensiveAdjustment.ToString());
      
      return true;
    }

    public static bool TryGetStrengthModifiers(int abilityScore, int? percentile,
      out int toHitAdjustment, out int damageAdjustment, out int weightAllowance,
      out int maxPress, out float openDoors, out float openLockedBarredMagicDoors, 
      out float bendBarsLiftGates)
    {
      toHitAdjustment = int.MinValue;
      damageAdjustment = int.MinValue;
      weightAllowance = int.MinValue;
      maxPress = int.MinValue;
      openDoors = float.MinValue;
      openLockedBarredMagicDoors = float.MinValue;
      bendBarsLiftGates = float.MinValue;

      if (!IsValidAbilityScore(abilityScore, percentile))
      {
        return false;
      }

      toHitAdjustment = 3;
      damageAdjustment = 6;
      weightAllowance = 335;
      maxPress = 480;
      openDoors = .8f;
      openLockedBarredMagicDoors = .3f;
      bendBarsLiftGates = .4f;

      return true;
    }

    /// <summary>
    /// Checks validity of ability scores
    /// </summary>
    /// <param name="abilityScore"></param>
    /// <param name="percentile"> Used only for Strength attribute</param>
    /// <returns></returns>
    private static bool IsValidAbilityScore(int abilityScore, int? percentile = null)
    {
      if (percentile.HasValue)
      {
        if (!IsValidPercentile(percentile.Value))
        {
          return false;
        }
      }
      return 0 < abilityScore && abilityScore < 26;
    }

    private static bool IsValidPercentile(int percentile)
    {
      return 0 < percentile && percentile < 101;
    }
  }
}