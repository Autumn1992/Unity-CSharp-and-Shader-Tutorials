using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

	public Mesh[] meshs;

	//public Mesh mesh;
	public Material material;

	public int maxDepth;
	private int depth;

	public float childScale;

	public float spwanProbability;

	public float maxRotationSpeed;
	private float rotationSpeed;

	public float maxTwist = 20f;

	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back,
	};

	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 0f, -90f),
		Quaternion.Euler (0f, 0f, 90f),
		Quaternion.Euler (90f, 0f, 0f),
		Quaternion.Euler (-90f, 0f, -90f),
	};

	// this works well and material's shader will influnce batching
	//static Material sharedMaterial = null;

	private Material[,] materials;

	private void InitializeMaterials () {
		materials = new Material[maxDepth + 1, 2];
		for (int i = 0; i <= maxDepth; i++) {
			float t = i / (maxDepth - 1f);
			t *= t;
			materials[i, 0] = new Material(material);
			materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
			materials[i, 1] = new Material(material);
			materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
		}
		materials[maxDepth, 0].color = Color.magenta;
		materials[maxDepth, 1].color = Color.red;
	}

	private void Start () {

		rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);

		transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);

		if (materials == null) {
			InitializeMaterials();
		}
		gameObject.AddComponent<MeshFilter>().mesh = meshs[Random.Range(0, meshs.Length)];
		gameObject.AddComponent<MeshRenderer>().material =
			materials[depth, Random.Range(0, 2)];
		if (depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}

	IEnumerator CreateChildren(){
		for (int i = 0; i < childDirections.Length; i++) {

			if (Random.value < spwanProbability) {
				var duration = Random.Range (0.1f, 0.5f);
				yield return new WaitForSeconds (duration);
				new GameObject ("Fractal Child").AddComponent<Fractal> ().
				Initialize (this, i);
                
			}
		}
	}

	private void Initialize(Fractal parent, int index){

		meshs = parent.meshs;
		material = parent.material;
		materials = parent.materials;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;

		spwanProbability = parent.spwanProbability;
		maxRotationSpeed = parent.maxRotationSpeed;
		maxTwist = parent.maxTwist;

		childScale = parent.childScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;

		transform.localPosition =  childDirections[index] * (0.5f + 0.5f * childScale);

		transform.localRotation = childOrientations[index];
	}

	void Update(){

        transform.Rotate (0f, rotationSpeed * Time.deltaTime, 0f);

	}
}
