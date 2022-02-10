using GbHapticsIntegration.Setup;
using UnityEngine;

namespace GbHapticsIntegration.Weapons
{
    internal class W_GiantFlail : I_WeaponBase
    {
        internal W_GiantFlail() : base(WeaponType.GiantFlail)
            => Setup("Weapons\\GiantFlail",
                use_blunt2: true);
    }
}