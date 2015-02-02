using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCtr : MonoBehaviour {

	public GameObject ragdoll;
	public GameObject animated;

	public float Spring = 10f;
	public float Damper = 0.2f;
	public float MaxDistance = 0.5f;
	string OldValues = "";

	public float walkSpeed = 0.25f;
	public float moveSpeed = 0.5f;
	public float turnSpeed = 25f;

	public bool showAnimation = false;
	bool animationVisible = true;

	[HideInInspector]
	public List<Rigidbody> feet = new List<Rigidbody> ();
	[HideInInspector]
	public List<bool> limbOnGround = new List<bool> ();

	class Limb {
		public Rigidbody ragdoll;
		public Transform animated;

		public Transform ragdollRoot;
		public Transform animatedRoot;

		public Vector3 LocalPositionDelta { 
			get {
				var ragLimbPos = ragdoll.position - ragdollRoot.transform.position;
				var aniLimbPos = animated.position - animatedRoot.transform.position;
				return aniLimbPos - ragLimbPos;
			}
		}

		public Vector3 WorldPositionDelta {
			get {
				return animated.position - ragdoll.position;
			}
		}

		public Vector3 WorldRotationDelta {
			get {
				return Vector3.Cross(ragdoll.transform.forward, animated.forward);
			}
		}

		public Vector3 LocalRotationDelta { 
			get {
				var parentLimb = ragdoll.GetComponent<CharacterJoint>().connectedBody;
				var ragLimbRot = Vector3.Cross(parentLimb.transform.forward, ragdoll.transform.forward);
				var aniParent = Utils.FindChild(parentLimb.name, animatedRoot.transform);
				var aniLimbRot = Vector3.Cross (aniParent.forward, animated.forward);
				return aniLimbRot - ragLimbRot;
			}
		}
	}

	Limb leftLeg, rightLeg, leftFoot, rightFoot;
	Limb leftArm, rightArm, leftHand, rightHand;
	Limb spine, head, hips;

	List<Limb> allLimbs;



	void InitLimbs()
	{
		leftLeg = FindLimb("LeftUpLeg");
		rightLeg = FindLimb("RightUpLeg");
		leftFoot = FindLimb("LeftLeg");
		rightFoot = FindLimb("RightLeg");
		leftArm = FindLimb("LeftArm");
		leftHand = FindLimb("LeftForeArm");
		rightArm = FindLimb("RightArm");
		rightHand = FindLimb("RightForeArm");
		spine = FindLimb("Spine1");
		head = FindLimb("Head");
		hips = FindLimb("Hips");

		allLimbs = new List<Limb> { leftLeg, leftFoot, rightLeg, rightFoot, leftArm, leftHand, rightArm, rightHand, spine, head };

		feet = new List<Rigidbody> { leftFoot.ragdoll, rightFoot.ragdoll };
		limbOnGround = new List<bool> { false, false };
	}

	Limb FindLimb(string name)
	{
		Debug.Log ("Finding Limb " + name);
		var limb = new Limb();
		limb.ragdoll = Utils.FindChild(name, ragdoll.transform).rigidbody;
		limb.animated = Utils.FindChild(name, animated.transform);
		limb.ragdollRoot = ragdoll.transform;
		limb.animatedRoot = animated.transform;
		return limb;
	}

	void AdjustSprings()
	{
		AdjustSpring(hips.ragdoll);
		foreach (var limb in allLimbs) {
			AdjustSpring(limb.ragdoll);
		}
	}

	void AdjustSpring(Rigidbody obj) 
	{
		foreach (var joint in obj.GetComponents(typeof(SpringJoint))) {
			var spring = (SpringJoint)joint;
			spring.spring = Spring;
			spring.damper = Damper;
			spring.maxDistance = MaxDistance;
		}
	}

    void ReadInputs ()
    {
		bool isMoving = false;

		if (Input.GetKey (KeyCode.UpArrow)) {
			animated.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
			animated.GetComponent<Animator>().speed = walkSpeed;
			isMoving = true;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			animated.transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime, Space.Self);
			animated.GetComponent<Animator>().speed = -walkSpeed;
			isMoving = true;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			animated.transform.Rotate(Vector3.up, Time.deltaTime * -turnSpeed);
			isMoving = true;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			animated.transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed);
			isMoving = true;
		}

		if (!isMoving) {
			animated.GetComponent<Animator>().speed = 0;
		}
    }

	void Awake()
	{
		InitLimbs();
	}

    void Start ()
	{
		animated.GetComponent<Animator>().speed = 0f;
		animated.GetComponent<Animator>().Play("Run");
	}

	void Update()
	{
		if (animationVisible != showAnimation) {
			ToggleRenderers(animated, showAnimation);
			animationVisible = showAnimation;
		}

		var valuesHash = "" + Spring + "" + Damper + "" + MaxDistance;
		if (valuesHash != OldValues) {
			AdjustSprings();
			OldValues = valuesHash;
		}

		ReadInputs ();
	}

	void FixedUpdate ()
    {
		RaycastHit raycast;
		int layerMask = ~LayerMask.GetMask("Player");
		if (!Physics.Raycast(hips.ragdoll.position, Vector3.down, out raycast, 100, layerMask)) {
			Debug.Log ("Oh no! Where is the ground?");
			return;
		}

		var pos = animated.transform.position;
		pos.y = raycast.point.y;
		animated.transform.position = pos;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		if (hips != null) {
			//Gizmos.DrawLine(hips.ragdoll.position, hips.ragdoll.position + hips.WorldRotationDelta);

			RaycastHit raycast;
			int layerMask = ~hips.ragdoll.gameObject.layer;
			if (!Physics.Raycast(hips.ragdoll.position, Vector3.down, out raycast, float.PositiveInfinity, layerMask)) {
				Gizmos.DrawLine(hips.ragdoll.position, raycast.point);
			}
		}
	}

	static void ToggleRenderers(GameObject obj, bool enabled)
	{
		var renderer = obj.GetComponent<Renderer>();
		if (renderer != null) {
			renderer.enabled = enabled;
		}
		for (int i=0; i < obj.transform.childCount; i++) {
			ToggleRenderers(obj.transform.GetChild(i).gameObject, enabled);
		}
	}
}
