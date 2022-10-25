using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    // Public variables
    public string[] urls;
    public GameObject player;
    public GameObject canvas;
    public GameObject spawnPoint;
    public Button videoButton;
    public YoutubePlayer ytPlayer;
    public WinCondition winCondition;
    public GameObject videoCanvas;
    public DailyReward dailyReward;

    // Private variables
    private VideoPlayer videoPlayer;
    public static int i = 0;
    public static int videoCount;

    void Start()
    {
        // Hide video canvas
        videoCanvas.SetActive(false);
        ytPlayer.enabled = false;

        // Hide double button if player can't watch videos
        if (videoCount > 9)
        {
            videoButton.gameObject.SetActive(false);
        }
    }

    // Play next video
    public void PlayVideo()
    {
        // Play only if player has watched under 10 videos
        if (videoCount < 9)
        {
            // Stop play time
            winCondition.countdownTime = 0;
            Destroy(spawnPoint);
            Destroy(player);
            Time.timeScale = 1;

            // Show video canvas
            canvas.SetActive(false);
            videoCanvas.SetActive(true);
            ytPlayer.enabled = true;

            // Play next video from list
            if (i < urls.Length)
            {
                ytPlayer.Play(urls[i]);
                i++;
                videoCount++;
            }
            // Reset video list
            else
            {
                i = 0;
            }
        }
    }

    // When video has stopped playing
    public void VideoEnd()
    {
        videoCanvas.SetActive(false);
        canvas.SetActive(true);
        ytPlayer.enabled = false;

        winCondition.DoubleMoneyVideo();
        videoButton.gameObject.SetActive(false);
    }
}