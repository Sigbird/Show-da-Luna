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
		private bool babyMode = false;
		public string[] localFiles;
		public int index;


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
			if (localFiles != null) {
				if (index > localFiles.Length) {
					index = 0;
				}
			}
		}

		public void Play() {
			showControls = true;
			receivedInput = false;

			Player.Prepare();

			OnPlayEvent.Invoke();

			delayedCoroutine = StartCoroutine(PlayDelayedTimer());
		}

		public void Play(string videoUrl, string[] localVideosUrl) {
			Player.source = VideoSource.Url;
			Player.url = videoUrl;

			localFiles = localVideosUrl;

			Play();
		}        

		public void Pause() {
			OnPauseEvent.Invoke();

			if (delayedCoroutine != null) {
				StopCoroutine(delayedCoroutine);
			}            
		}

		public void OnTouchScreen() {           
			if (Player.isPlaying && !babyMode) {
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

		public void BabyMode() {
			if (!babyMode) { //BLOQUEIA CONTROLES
				babyMode = true;
				if (showControls) {
					OnPlayDelayedEvent.Invoke();
					showControls = false;
				}
			} else { //LIBERA CONTROLES
				babyMode = false;
				if (!showControls) {
					OnPlayDelayedEvent.Invoke();
					showControls = true;
				}
			}
		}

		public void Next(){
			index++;
			if (localFiles != null) {
				if (index > localFiles.Length) {
					index = 0;
				}
			}
			Player.source = VideoSource.Url;//PULA PARA PRÓXIMO VIDEO
			Player.url = localFiles[index];
			//Play();
			showControls = true;
			receivedInput = false;

			Player.Prepare();

			OnPlayEvent.Invoke();
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
			//Close();

			Next ();
		}



		private IEnumerator InputTimeout() {
			yield return new WaitForSecondsRealtime(DelayedOnPlaySeconds);

			receivedInput = false;
		}
	}
}
