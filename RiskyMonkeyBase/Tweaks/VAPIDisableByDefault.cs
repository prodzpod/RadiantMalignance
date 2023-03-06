using RoR2;

namespace RiskyMonkeyBase.Tweaks
{
    public class VAPIDisableByDefault
    {
        public static void Patch()
        {
            On.RoR2.RuleDef.FromExpansion += (orig, self) =>
            {
                RuleDef ret = orig(self);
                if (self.nameToken == "VAPI_EXPANSION_NAME") ret.defaultChoiceIndex = 1; // off
                return ret;
            };
        }
    }
}
