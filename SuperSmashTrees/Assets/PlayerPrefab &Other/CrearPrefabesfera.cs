using UnityEngine;

public class SpawnSphereOnKey : MonoBehaviour
{
    // Puedes usar esto para ajustar el color y transparencia
    public Color sphereColor = new Color(0f, 0f, 1f, 0.5f); // Azul semitransparente

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SpawnSphere();
        }
    }

    void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(2f, 2f, 2f);

        // Añadir un material transparente
        Renderer rend = sphere.GetComponent<Renderer>();
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = sphereColor;
        mat.SetFloat("_Mode", 3); // Transparent mode
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
        rend.material = mat;
    }
}
