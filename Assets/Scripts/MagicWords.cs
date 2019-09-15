using UnityEngine;
using Microsoft.CognitiveServices.Speech;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MagicWords : MonoBehaviour
{
    private object threadLocker = new object();
    private bool waitingForReco;
    private string message = "";
    private bool micPermissionGranted = false;

    public GameObject laserPrefab;
    public Transform wand;
    public float speed;

#if PLATFORM_ANDROID
    // Required to manifest microphone permission, cf.
    // https://docs.unity3d.com/Manual/android-manifest.html
    private Microphone mic;
#endif

    public async void ButtonClick()
    {
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("761590147de94dc0a89ecbe05a47c17c", "westus");
        config.SetProperty("SpeechServiceConnection_EndSilenceTimeoutMs", "10"); // setting default timeout to 10 miliseconds

        using (var recognizer = new SpeechRecognizer(config))
        {
            lock (threadLocker)
            {
                waitingForReco = true;
            }

            // Starts speech recognition, and returns after a single utterance is recognized. The end of a
            // single utterance is determined by listening for silence at the end or until a maximum of 15
            // seconds of audio is processed.  The task returns the recognition text as result.
            // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
            // shot recognition like command or query.
            // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
            var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

            // Checks result.
            string newMessage = string.Empty;
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                newMessage = result.Text;
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                newMessage = "NOMATCH: Speech could not be recognized.";
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
            }

            lock (threadLocker)
            {
                message = newMessage;
                waitingForReco = false;
            }
        }
    }

    public void Shoot()
    {
        GameObject laser = (GameObject)Instantiate(laserPrefab, wand.position, wand.rotation);
        laser.transform.Rotate(new Vector3(0, 0, 90));
        laser.GetComponent<Rigidbody>().velocity = wand.right * speed;
    }

    void Start()
    {
#if PLATFORM_ANDROID
            // Request to use the microphone, cf.
            // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
#else
        micPermissionGranted = true;
#endif
    }

    void Update()
    {
#if PLATFORM_ANDROID
        if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            micPermissionGranted = true;
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            ButtonClick();
        }

        //reducto
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            //GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            message = "";
            Shoot();
        }

        //lumos
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            //GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            message = "";
            Shoot();
        }
        
        if (message != "")
        {
            message = message.ToLower();
            if (message.Contains("stu"))
            {
                Debug.Log(message);
                //GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                Shoot();
            }
            else if (message.Contains("lum"))
            {
                Debug.Log(message);
                //GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                Shoot();
            }
            else if (message.Contains("reduc"))
            {
                Debug.Log(message);
                //GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                Shoot();
            }
            else if (message.Contains("expel"))
            {
                Debug.Log(message);
                //GetComponent<Renderer>().material.color = new Color(0, 0, 255);
                Shoot();
            }
            else if (message.Contains("avada") || message.Contains("bad"))
            {
                Debug.Log(message);
                //GetComponent<Renderer>().material.color = new Color(0, 255, 0);
                Shoot();
            }
            message = "";
        }
       
#endif

        lock (threadLocker)
        {
        }
    }
}