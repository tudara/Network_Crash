using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource jumpSound1;
    public AudioSource jumpSound2;
    public AudioSource deathSound;
    public AudioSource deathDoorSound;
    public AudioSource runSound;

    private bool isRunning = false;

    // Call this for jumping sounds
    public void PlayJumpSound()
    {
        // Randomly choose between the two jump sounds
        if (Random.Range(0, 2) == 0)
            jumpSound1.Play();
        else
            jumpSound2.Play();
    }

    // Call this for the death door sound
    public void PlayDeathDoorSound()
    {
        deathDoorSound.Play();
    }

    // Call this for dying sound
    public void PlayDyingSound()
    {
        deathSound.Play();
    }

    // Call this for running sound
    public void PlayRunningSound()
    {
        if (!isRunning) // Ensure the sound doesn't overlap
        {
            runSound.Play();
            isRunning = true;
        }
    }

    public void StopRunningSound()
    {
        runSound.Stop();
        isRunning = false;
    }
}

