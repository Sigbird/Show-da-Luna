using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

namespace YupiPlay.Luna.LunaPlayer 
{
    public class VideoPlayerController : MonoBehaviour {
        public VideoPlayer Player;

        public float DelayedOnPlaySeconds = 4f;

        public UnityEvent OnPlayEvent;
        public UnityEvent OnPlayDelayedEvent;
        public UnityEvent OnPauseEvent;
        public UnityEvent OnTouchScreenEvent;
        public UnityEvent OnClose;        

        private Coroutine delayedCoroutine;
        private Coroutine timeoutCoroutine;
        private bool showControls = false;
        private bool receivedInput = false;

        public static VideoPlayerController Instance {
            get {
                return instance;
            }
            private set {}
        }

        private static VideoPlayerController instance;

        void Awake() {
            if (instance == null) {
                instance = this;
            }            
        }

        void Start() {            
            
        }

        void OnEnable() {
            Player.prepareCompleted += OnPlayerPrepared;
            Player.loopPointReached += OnLoopPointReached;
        }

        private void OnDisable() {
            Player.prepareCompleted -= OnPlayerPrepared;
            Player.loopPointReached -= OnLoopPointReached;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Close();
            }
        }

        public void Play() {
            showControls = true;
            receivedInput = false;

            Player.Prepare();

            OnPlayEvent.Invoke();

            delayedCoroutine = StartCoroutine(PlayDelayedTimer());
        }

        public void Play(string videoUrl) {
            Player.source = VideoSource.Url;
            Player.url = videoUrl;

            Play();
        }        

        public void Pause() {
            OnPauseEvent.Invoke();

            if (delayedCoroutine != null) {
                StopCoroutine(delayedCoroutine);
            }            
        }

        public void OnTouchScreen() {           
            if (Player.isPlaying) {
                receivedInput = true;
                showControls = !showControls;

                if (timeoutCoroutine != null) {
                    StopCoroutine(timeoutCoroutine);
                }
                timeoutCoroutine = StartCoroutine(InputTimeout());

                OnTouchScreenEvent.Invoke();

                StopCoroutine(delayedCoroutine);
                delayedCoroutine = StartCoroutine(PlayDelayedTimer());
            }
        }

        public void Close() {
            StopAllCoroutines();
            OnClose.Invoke();
        }

        private IEnumerator PlayDelayedTimer() {
            yield return new WaitForSecondsRealtime(DelayedOnPlaySeconds);           
               
            if (!receivedInput && showControls) {
                OnPlayDelayedEvent.Invoke();
                showControls = false;
            }            
        }

        private void OnPlayerPrepared(VideoPlayer source) {
            Debug.Log("on player prepared called");            
        }

        private void OnLoopPointReached(VideoPlayer source) {
            Debug.Log("OnLoopPointReached called");
            Close();
        }

        private IEnumerator InputTimeout() {
            yield return new WaitForSecondsRealtime(DelayedOnPlaySeconds);

            receivedInput = false;
        }
    }
}
