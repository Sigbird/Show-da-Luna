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
            Player.prepareCompleted += OnPlayerPrepared;
            Player.loopPointReached += OnLoopPointReached;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Close();
            }
        }

        public void Play() {
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
                OnTouchScreenEvent.Invoke();
                delayedCoroutine = StartCoroutine(PlayDelayedTimer());
            }
        }

        public void Close() {
            OnClose.Invoke();
        }

        private IEnumerator PlayDelayedTimer() {
            yield return new WaitForSecondsRealtime(DelayedOnPlaySeconds);

            OnPlayDelayedEvent.Invoke();
        }

        private void OnPlayerPrepared(VideoPlayer source) {
            Debug.Log("on player prepared called");
            
        }

        private void OnLoopPointReached(VideoPlayer source) {
            Debug.Log("OnLoopPointReached called");
            Close();
        }
    }
}
