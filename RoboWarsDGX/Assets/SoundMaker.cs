using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    [Header ("Weapon shot sounds")]
    public Sound shotgunShotSound;
    public Sound sniperShotSound;
    public Sound smgShotSound;

    private AudioSource shotSource;

    public void SetShotSound(WeaponType type)
    {
        shotSource = gameObject.AddComponent<AudioSource>();

        Sound shotSound;
        switch (type)
        {
            case WeaponType.Shotgun:
                shotSound = shotgunShotSound;
                break;
            case WeaponType.Sniper:
                shotSound = sniperShotSound;
                break;
            case WeaponType.SMG:
                shotSound = smgShotSound;
                break;
            default:
                shotSound = sniperShotSound;
                break;
        }

        shotSource.clip = shotSound.clip;
        shotSource.volume = shotSound.volume;
        shotSource.pitch = shotSound.pitch;
        shotSource.spatialBlend = 1f;
    }


    public void ShotSound()
    {
        shotSource.Play();
    }
}
