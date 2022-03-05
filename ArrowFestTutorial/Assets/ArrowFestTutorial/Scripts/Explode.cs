using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://github.com/thomasfriday/exploding-cube/blob/master/Assets/Scripts/Explode.cs
public class Explode : MonoBehaviour
{
    [SerializeField] private int cubesPerAxis = 8;
    [SerializeField] private float force = 3000f;
    [SerializeField] private float radius = 2f;
    [SerializeField] private GameController gameController;

    void Main() {
        for (int x = 0; x < cubesPerAxis; x++) {
            for (int y = 0; y < cubesPerAxis; y++) {
                for (int z = 0; z < cubesPerAxis; z++) {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }

        Destroy(gameObject);
    }

    void CreateCube(Vector3 coordinates) {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubesPerAxis;

        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, radius);
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.tag == "Arrow")
        {
            obj.transform.parent.gameObject.SetActive(false);
            Main();
            gameController.IsLevelCompleted = true;
        }
    }
}
