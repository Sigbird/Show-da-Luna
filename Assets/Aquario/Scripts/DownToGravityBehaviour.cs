using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YupiPlay.Luna.Aquario
{
    public class DownToGravityBehaviour : MonoBehaviour
    {
        private Rigidbody2D rb;
        private BubbleTorqueReaction torqueReaction;

        public Vector2 ToGroundBelow {
			get {
				return _toGroundBelow;
			}
		}        

		private Vector2 _toGroundBelow;

        // Use this for initialization
        void Start()
        {
            torqueReaction = GetComponent<BubbleTorqueReaction>();
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            //Direção para o vetor da gravidade, paralello
            _toGroundBelow = VectorUtil.ParalellToGravity(transform.position);            
            //Rotação de "para baixo" em direção ao plano
            Quaternion q = Quaternion.FromToRotation((Vector3)Vector2.down, (Vector3) ToGroundBelow);
            //rotaciona na direção da nova rotação
            q = Quaternion.Lerp(transform.rotation, q, Time.fixedDeltaTime);                        

            //aplica a rotação se não estiver recebendo torque ou torque estiver desligado
            if (torqueReaction != null)
            {
                if (!torqueReaction.IsReceivingTorque)
                {
                    rb.MoveRotation(q.eulerAngles.z);
                    return;
                }
            } else
            {
                rb.MoveRotation(q.eulerAngles.z);
            }           
        }
    }
}

