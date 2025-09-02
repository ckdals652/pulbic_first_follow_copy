using System.Collections;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Video;

public class VideoRemoteController : MonoBehaviour
{
    public Grab leftGrab, rightGrab;
    public VideoPlayer tutorialVideoPlayer;

    public float videoScaleSpeed = 10f;

    private Vector3 startLeftDoorPosition;
    private Vector3 endLeftDoorPosition;

    private Vector3 startRightDoorPosition;
    private Vector3 endRightDoorPosition;

    private Vector3 targetScale;

    bool isPlaying = false;

    private void Start()
    {
        Vector3 videoScale = tutorialVideoPlayer.transform.localScale;
        videoScale.x = 0f;
        tutorialVideoPlayer.transform.localScale = videoScale;

        targetScale = new Vector3(16f, 9f, 1f);

        tutorialVideoPlayer.Stop();
    }

    private void Update()
    {
        bool isLeftGrabbed = leftGrab.isGrabbed && leftGrab.grabedObject == this.gameObject;
        bool isRightGrabbed = rightGrab.isGrabbed && rightGrab.grabedObject == this.gameObject;
        Vector3 curScale = tutorialVideoPlayer.transform.localScale;

        if ((isLeftGrabbed || isRightGrabbed) && !isPlaying)
        {
            curScale.x = Mathf.Lerp(curScale.x, targetScale.x, Time.deltaTime * videoScaleSpeed);
            tutorialVideoPlayer.transform.localScale = new Vector3(curScale.x, curScale.y, curScale.z);

            tutorialVideoPlayer.Play();
        }

        else if (!leftGrab.isGrabbed && !rightGrab.isGrabbed)
        {
            curScale.x = Mathf.Lerp(curScale.x, 0f, Time.deltaTime * videoScaleSpeed);
            tutorialVideoPlayer.transform.localScale = new Vector3(curScale.x, curScale.y, curScale.z);

            StartCoroutine(StopVideo());
        }
    }

    IEnumerator StopVideo()
    {
        isPlaying = true;
        yield return new WaitForSeconds(1f);
        tutorialVideoPlayer.frame = 0;
        isPlaying = false;
    }
}
