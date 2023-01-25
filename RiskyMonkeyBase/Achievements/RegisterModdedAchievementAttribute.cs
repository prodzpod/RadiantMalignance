using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Bindings;

namespace RiskyMonkeyBase.Achievements
{
    public class RegisterModdedAchievementAttribute : Attribute
    {
        public readonly string identifier;
        public readonly string unlockableRewardIdentifier;
        public readonly string prerequisiteAchievementIdentifier;
        public readonly Type serverTrackerType;
        public readonly string[] mods;

        public RegisterModdedAchievementAttribute(
          [NotNull] string identifier,
          string unlockableRewardIdentifier,
          string prerequisiteAchievementIdentifier,
          Type serverTrackerType = null,
          params string[] mods)
        {
            this.identifier = identifier;
            this.unlockableRewardIdentifier = unlockableRewardIdentifier;
            this.prerequisiteAchievementIdentifier = prerequisiteAchievementIdentifier;
            this.serverTrackerType = serverTrackerType;
            this.mods = mods;
        }
    }
}
