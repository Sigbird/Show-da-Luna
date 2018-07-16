using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioRotation : MonoBehaviour {     
    public enum RotationTypes {RotateToGravity, RotateToInputAcceleration, NoRotation}      
    public RotationTypes RotationType = RotationTypes.RotateToGravity;
    public float RotationSpeed = 1.5f;
    
	void FixedUpdate () {       
        if (RotationType == RotationTypes.RotateToGravity) {
            //Rotação de "para baixo" em direção à gravidade
            Quaternion q = Quaternion.FromToRotation(Vector2.down, Physics2D.gravity);                
            //rotaciona na direção da nova rotação
            q = Quaternion.Lerp(transform.rotation, q, Time.fixedDeltaTime * RotationSpeed);
            transform.rotation = q;
        }                                                
    }

    void Update() {
        if (RotationType == RotationTypes.RotateToInputAcceleration) {
            var q = Quaternion.FromToRotation(Vector2.down, Input.acceleration);
            q = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * RotationSpeed);        
            transform.rotation = Quaternion.Euler(0,0, q.eulerAngles.z);
        }                        
    }
}
