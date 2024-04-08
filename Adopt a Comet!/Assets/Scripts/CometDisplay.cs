using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace CometDisplay.Utilities
{

    public class CometDisplay : MonoBehaviour
    {
        public static CometDisplay instance;

        private void Awake()
        {
            //First time?
            if (instance == null)
            {
                //Assign instance
                instance = this;
                //Don't destroy this gameObject through different scene
                DontDestroyOnLoad(this);

            }
            else
            {
                //Destroy self if there is duplicate
                Destroy(gameObject);
            }

        }

        [SerializeField]
        public RectTransform statsFrame;


        [SerializeField]
        public Image img;

        [SerializeField]
        public TextMeshProUGUI tX_name;

        [SerializeField]
        public TextMeshProUGUI tX_orbit_class;

        

        public void DisplayCometStats(Asteroid cometToDisplay)
        {
            tX_name.text = "Hello! I'm... \n" + cometToDisplay.asteroidName;

            StartCoroutine(DownloadImage(cometToDisplay.moodEmojiURl));

            tX_orbit_class.text = "Orbit class:\n" + cometToDisplay.orbit_class;

        }


        IEnumerator DownloadImage(string imageURL)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log(request.error);
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            }
        }

    }


}
