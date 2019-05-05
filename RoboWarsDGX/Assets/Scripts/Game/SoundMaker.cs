using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    [Header ("Weapon shot sounds")]
    public Sound shotgunShotSound;
    public Sound sniperShotSound;
    public Sound smgShotSound;

    /*[Header("Movement sounds")]
    public Sound footstep;*/

    private AudioSource shotSource;

    [Header("Movement sounds")]
    public AudioSource footstepSource;
    public AudioSource jumpSource;
    public AudioSource landSource;

    private bool soundOn = false;

    /*private void Start()
    {
        footstepSource = gameObject.AddComponent<AudioSource>();
        shotSource.clip = footstep.clip;
        shotSource.volume = footstep.volume;
        shotSource.pitch = footstep.pitch;
        shotSource.spatialBlend = 1f;
    }*/

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

    public void StartedWalking()
    {
        footstepSource.Play();
        soundOn = true;
    }

    public void Stopped()
    {
        footstepSource.Stop();
        soundOn = false;
    }

    public void Landed()
    {
        landSource.Play();
    }

    public void Jumped()
    {
        if (soundOn)
        {
            Stopped();
        }

        jumpSource.Play();
    }
}
