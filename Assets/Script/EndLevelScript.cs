using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public string next_scene_name;
    public Material mesh_material;
    public Light point_light;
    public Color start_color = Color.red;
    public Color finish_color = Color.white;
    public float color_change_speed = 1f;

    private float t = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh_material.color = start_color;
    }

    // Update is called once per frame
    void Update()
    {

        if (GlobalStats.Instance.collected_items == GlobalStats.Instance.max_items){

            t += Time.deltaTime * color_change_speed;
            t = Mathf.Clamp01(t); // optional: clamp between 0 and 1

            //mesh_material.SetColor("Color", Color.Lerp(mesh_material.GetColor("Color"), finish_color, t));
            mesh_material.color = Color.Lerp(mesh_material.color, finish_color, t);
            point_light.GetComponent<Light>().color = Color.Lerp(point_light.GetComponent<Light>().color, finish_color, t);

        }

    }

    public void ChangeScene(){
        DontDestroyOnLoad(GlobalStats.Instance.musicPlayer);
        SceneManager.LoadScene(next_scene_name);
    }
}
