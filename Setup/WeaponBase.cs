using UnityEngine;
using GbHapticsIntegration.Setup.Effects;
using GbHapticsIntegration.Managers;
using MelonLoader;

namespace GbHapticsIntegration.Setup
{
    internal class I_WeaponBase
    {
        internal WeaponType weaponType = WeaponType.None;
        internal CaestusType caestusType = CaestusType.None;
        internal E_Wobble wobble;
        internal E_Blunt blunt;
        internal E_Blunt2 blunt2;
        internal E_Blunt3 blunt3;
        internal E_Stab stab;
        internal E_Cut cut;
        internal E_DrawString drawString;
        internal E_Shoot shoot;
        internal E_ShootString shootString;

        internal I_WeaponBase() : this(WeaponType.None, CaestusType.None) { }
        internal I_WeaponBase(WeaponType type) : this(type, CaestusType.None) { }
        internal I_WeaponBase(CaestusType type) : this(WeaponType.None, type) { }
        internal I_WeaponBase(WeaponType type, CaestusType ctype)
        {
            weaponType = type;
            if (type != WeaponType.None)
                M_Tact.WeaponsByType[type] = this;

            caestusType = ctype;
            if (ctype != CaestusType.None)
                M_Tact.WeaponsByCaestusType[ctype] = this;
        }

        internal void Setup(string baseFolder,
            bool use_blunt = false,
            bool use_blunt2 = false,
            bool use_blunt3 = false,
            bool use_cut = false,
            bool use_drawString = false,
            bool use_shoot = false,
            bool use_shootString = false,
            bool use_stab = false,
            bool use_wobble = false)
        {
            if (use_blunt)
                blunt = new E_Blunt(this, baseFolder);
            if (use_blunt2)
                blunt2 = new E_Blunt2(this, baseFolder);
            if (use_blunt3)
                blunt3 = new E_Blunt3(this, baseFolder);
            if (use_cut)
                cut = new E_Cut(this, baseFolder);
            if (use_drawString)
                drawString = new E_DrawString(this, baseFolder);
            if (use_shoot)
                shoot = new E_Shoot(this, baseFolder);
            if (use_shootString)
                shootString = new E_ShootString(this, baseFolder);
            if (use_stab)
                stab = new E_Stab(this, baseFolder);
            if (use_wobble)
                wobble = new E_Wobble(this, baseFolder);
        }

        internal bool IsPlaying_Blunt(bHaptics.PositionType positionType)
            => (blunt != null) && blunt.IsPlaying(positionType);
        internal bool IsPlaying_Blunt2(bHaptics.PositionType positionType)
            => (blunt2 != null) && blunt2.IsPlaying(positionType);
        internal bool IsPlaying_Blunt3(bHaptics.PositionType positionType)
            => (blunt3 != null) && blunt3.IsPlaying(positionType);
        internal bool IsPlaying_Cut(bHaptics.PositionType positionType)
            => (cut != null) && cut.IsPlaying(positionType);
        internal bool IsPlaying_DrawString(bHaptics.PositionType positionType)
            => (drawString != null) && drawString.IsPlaying(positionType);
        internal bool IsPlaying_Shoot(bHaptics.PositionType positionType)
            => (shoot != null) && shoot.IsPlaying(positionType);
        internal bool IsPlaying_ShootString(bHaptics.PositionType positionType)
            => (shootString != null) && shootString.IsPlaying(positionType);
        internal bool IsPlaying_Stab(bHaptics.PositionType positionType)
            => (stab != null) && stab.IsPlaying(positionType);
        internal bool IsPlaying_Wobble(bHaptics.PositionType positionType)
            => (wobble != null) && wobble.IsPlaying(positionType);

        internal void OnBlunt(Vector3 velocity, bool is_left)
        {
            blunt?.Play(velocity, is_left);
            blunt2?.Play(velocity, is_left);
            blunt3?.Play(velocity, is_left);
        }
        internal void OnCut(Vector3 velocity, bool is_left)
            => cut?.Play(velocity, is_left);
        internal void OnDrawString(float magnitude, bool is_left)
            => drawString?.Play(magnitude, is_left);
        internal void OnShoot(bool is_left)
            => shoot?.Play(is_left);
         internal void OnShootString(bool is_left)
            => shootString?.Play(is_left);
        internal void OnStab(Vector3 velocity, bool is_left)
            => stab?.Play(velocity, is_left);
        internal void OnWobble(Vector3 velocity, bool is_left)
            => wobble?.Play(velocity, is_left);

        internal virtual Fist Parse2HandedDamageFist(WeaponBase weaponBase, Collision collision)
        {
            if (weaponBase is Spear)
            {
                Spear spearWeaponBase = (Spear)weaponBase;
                return Parse2HandedDamageFist(collision,
                    spearWeaponBase.frontGrip.grabbedBy,
                    spearWeaponBase.grabbable.grabbedBy,
                    spearWeaponBase.frontGrip.gameObject.GetComponent<Collider>(),
                    spearWeaponBase.headCollider);
            }
            else if (weaponBase is WarhammerWeaponBase)
            {
                WarhammerWeaponBase warhammerWeaponBase = (WarhammerWeaponBase)weaponBase;
                return Parse2HandedDamageFist(collision,
                    warhammerWeaponBase.frontGrip.grabbedBy,
                    warhammerWeaponBase.grabbable.grabbedBy,
                    warhammerWeaponBase.frontGrip.gameObject.GetComponent<Collider>(),
                    warhammerWeaponBase.headCollider);
            }
            else if (weaponBase is TwoHandedWeaponBase)
            {
                TwoHandedWeaponBase twoHandedWeaponBase = (TwoHandedWeaponBase)weaponBase;
                return Parse2HandedDamageFist(collision,
                    twoHandedWeaponBase.frontGrip.grabbedBy,
                    twoHandedWeaponBase.grabbable.grabbedBy,
                    twoHandedWeaponBase.frontGrip.gameObject.GetComponent<Collider>());
            }
            return null;
        }

        internal Fist Parse2HandedDamageFist(Collision collision, GrabHand front_GrabHand, GrabHand back_GrabHand, params Collider[] weapon_front_colliders)
        {
            bool collider_check = false;
            for (int i = 0; i < collision.contactCount; i++)
            {
                ContactPoint contactPoint = collision.contacts[i];
                for (int z = 0; z < weapon_front_colliders.Length; z++)
                {
                    Collider collider = weapon_front_colliders[i];
                    if ((collider == contactPoint.thisCollider)
                        || (collider == contactPoint.otherCollider))
                    {
                        collider_check = true;
                        break;
                    }
                }
                if (collider_check)
                    break;
            }

            if (collider_check)
            {
                // Front
                if (front_GrabHand != null)
                    return back_GrabHand.ownerFist;

                // Back
                if (back_GrabHand != null)
                    return back_GrabHand.ownerFist;
            }
            else
            {
                // Back
                if (back_GrabHand != null)
                    return back_GrabHand.ownerFist;

                // Front
                if (front_GrabHand != null)
                    return front_GrabHand.ownerFist;
            }

            return null;
        }
    }
}
