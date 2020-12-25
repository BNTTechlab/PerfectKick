using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

    public GoalieAI goalieAI;
	Rigidbody ball;
	Transform target;

    public float h;//= 25;
	public float gravity = -22;

	public bool debugPath;

	void Start() {
		//ball.useGravity = false;
        
	}

	void Update() {

    }


	public void Launch(Rigidbody ba, Transform ta) {
		//Debug.Log ("bat dau launch");
		ball = ba;
		target = ta;
		Physics.gravity = Vector3.up * gravity;
		ball.useGravity = true;
		Vector3 vt = CalculateLaunchData ().initialVelocity;
		//Debug.Log ("tinh toan: " + vt);

		ball.velocity = vt;
        goalieAI.GetComponent<GoalieAI>().Jump();

    }

	 LaunchData CalculateLaunchData() {
		//Debug.Log ("calculate launch");
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h )/gravity);
      //  Debug.Log("displacementY" + displacementY + "\t h= " +h); 

        Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

      //  Debug.Log("velocityXY" + velocityXZ); 
		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	public void DrawPath(Rigidbody ba, Transform ta) {
        ball = ba;
        target = ta;

        LaunchData launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++) {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ball.position + displacement;
			Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
}
	