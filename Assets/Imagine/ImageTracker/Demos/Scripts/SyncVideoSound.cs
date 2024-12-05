using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
namespace Imagine.WebAR.Samples

{
    public class SyncVideoSound : MonoBehaviour
    {
        [SerializeField] VideoPlayer video;
        [SerializeField] AudioSource sound;
        public TextMeshProUGUI TxtTest;
        bool isReady;
        bool isPause;
        bool isSound;        


        void OnEnable(){
            StartCoroutine("SyncRoutine");
            isPause = false;
            sound.time = 0;
            video.time = 0;
            isReady = false;
            isSound = false;
            
        }

        void OnDisable(){
            StopCoroutine("SyncRoutine");
            Debug.Log("Disabled");
        }
        
        IEnumerator SyncRoutine()
        {
            while(!video.isPrepared){
                Debug.Log("Waiting video preparation");
                yield return null;
            }

            video.Play();
            //sound.Play();
            isReady = true;




            while (true)
            {



                //    if((Mathf.Abs(sound.time - (float)video.time) > 0.5)&&isPause==false)
                //    {

                //            sound.Pause();
                //            sound.time = (float)video.time;
                //            Debug.Log(sound.time + "=>" + video.time);
                //            isPause = true;


                //    }
                //    else if(isPause&&video.frame>10)
                //    {

                //        sound.UnPause();
                //        isPause = false;



                //    }
                //    Debug.Log("Frames: " + video.frame);
                //    yield return new WaitForSeconds(1);
                
                if (isReady)
                {


                    if ((sound.time - (float)video.time) > 1f|| (sound.time - (float)video.time) < -1f)
                    {
                        
                        sound.Pause();
                        Debug.Log("Status: Pause");
                        isPause = true;
                    }
                   
                    else
                    {
                        if (isPause)
                        {
                            
                            sound.UnPause();
                            sound.time = (float)video.time;
                            isPause = false;
                            
                            Debug.Log("Status: Resume");
                        }


                    }
                }
                yield return new WaitForSeconds(1f);
            }
        }


        private void Update()
        {
            Debug.Log("Sound Play: " + sound.isPlaying+"   "+"Sound Pause: "+isPause);
            TxtTest.text = "Sound: " + sound.time + "  Video: " + video.time + "\n" + "Sound Play: " + sound.isPlaying + "   " + "Sound Pause: " + isPause;
            if (video.frame>8f && !isSound&&isReady)
            {
                if(!sound.isPlaying)
                {
                    sound.Play();
                }
                
                sound.Pause();
                isPause = true;

                isSound = true;
            }
            

        }


    }
}
