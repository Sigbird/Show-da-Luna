using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Utils {




	[System.Serializable]
	public class ActionSequenceElement
	{
		public ActionObject activatorTimerObject;

		// Tempo de espera para ativar/desativar proximo elemento
		public float waitSecsToNext = 2.0f;
	}


	/**
	 * Quando ativado, inicialmente, todos os elementos (activatorTimerElements) sao desativados/ativados 
	 * a depender da sua respectiva ActivatorTimerAction para serem ativados/desativados
	 * sequencialmente durante a execuçao do timer.
	 */
	public class ActionSequence : MonoBehaviour 
	{
		public ActionSequenceElement[] activatorTimerElements;		
		public ActionObject[] activateOnFinish;

		private int currentElementIndex;
		private ActionSequenceElement currentElement;

		private float startTime;
		private bool firstElement;


		public void Reset()
		{
			firstElement = true;
			currentElement = null;
			currentElementIndex = 0;
		}

		private void LoadLevelAction(string levelName) 
		{
			Application.LoadLevel (levelName);
		}

		/**
		 * Executa acao do ActivatorTimerObject
		 * 
		 * Ativa/Desativa Game Object OU
		 * Pula para scena
		 */
		private void DoAction(ActionObject timerObject) 
		{

			timerObject.DoAction();

		}

		private void ChangeToElement(int elementIndex)
		{
			
			// procura por elemento na posiçao elementIndex
			if (activatorTimerElements.Length > elementIndex && activatorTimerElements[elementIndex] != null)
			{
				currentElementIndex = elementIndex;
				currentElement = activatorTimerElements[elementIndex];

				//Atualiza timer para proximo elemento
				startTime = Time.realtimeSinceStartup;
			}
			else 
			{
				OnFinishTimer();				
				return;
			}
			
			// executa elemento
			if (currentElement != null)
			{
				if (currentElement.activatorTimerObject != null)
					DoAction(currentElement.activatorTimerObject);
			}
			
			
		}

		private void OnFinishTimer()
		{	
			currentElement = null;
			currentElementIndex = 0;
			
			// executa ultimas açoes do timer
			
			for (int i = 0; i < activateOnFinish.Length; ++i)
			{
				if (activateOnFinish[i] != null)
				{
					DoAction(activateOnFinish[i]);
				}

			}

			gameObject.SetActive(false);
		}

		// Toda vez que for ativado, 
		// Sequencia de timer sera reiniciada
		void OnEnable()
		{
			Reset ();
		}

		void Update()
		{
			if (firstElement)
			{
				firstElement = false;
				ChangeToElement(0);
			} else 
			{
				if (currentElement != null)
				{
					if ((Time.realtimeSinceStartup - startTime) > currentElement.waitSecsToNext)
					{
						ChangeToElement(currentElementIndex+1);
					}
				}
			}
		}
	}


}
