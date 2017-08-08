using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayVoiceMessage : MonoBehaviour {

    public static PlayVoiceMessage Instance { get; private set; }
        
    public GameObject photoCaptureManagerGmObj;
    
    void Awake()
    {
        Instance = this;
    }

    public void PlayTextToSpeechMessage(MCSFaceDto face)
    {
        string message = string.Empty;
        string emotionName = string.Empty;

        if (face.faces.Count > 0)
        {
            EmotionAttributes emotionAttributes = face.faces[0].emotionAttributes;

            Dictionary<string, float> emotions = new Dictionary<string, float>
            {
                { "anger", emotionAttributes.anger },
                { "contempt", emotionAttributes.contempt },
                { "disgust", emotionAttributes.disgust },
                { "fear", emotionAttributes.fear },
                {"happiness", emotionAttributes.happiness },
                {"sadness", emotionAttributes.sadness },
                {"suprise", emotionAttributes.surprise }
            };

            emotionName = emotions.Keys.Max();

            //Message 
            message = string.Format("{0} is pretty much {1} years old and looks {2}", face.faces[0].faceAttributes.gender == 0 ? "He" : "She", face.faces[0].faceAttributes.age, emotionName);         
        }
        else
            message = "I could't detect anyone.";

        // Try and get a TTS Manager
        TextToSpeechManager tts = null;

        if (photoCaptureManagerGmObj != null)
        {
            tts = photoCaptureManagerGmObj.GetComponent<TextToSpeechManager>();
        }

        if (tts != null)
        {
            //Play voice message
            tts.SpeakText(message);
        }
    }
}
