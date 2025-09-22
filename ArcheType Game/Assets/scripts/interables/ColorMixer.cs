using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorMixer : MonoBehaviour
{
    public int chandColorId = 0;
    public Image slot1;
    public Image slot2;
    public Image color1;
    public Image color2;
    public Image result;

    public bool ObjCompleto = false;

    public string colorselect1, colorselect2;

    public GameObject mycolorpalette;
    public GameObject prefabmycolorslot;


    private Dictionary<string, Color> colorVisuals = new Dictionary<string, Color>()
{
    // Bases
    {"Vermelho", new Color(1f, 0f, 0f)},
    {"Amarelo", new Color(1f, 1f, 0f)},
    {"Branco", Color.white},
    {"Preto", Color.black},

    // Laranja
    {"Laranja", new Color(1f, 0.5f, 0f)},
    {"Laranja Claro 1", new Color(1f, 0.65f, 0.2f)},
    {"Laranja Claro 2", new Color(1f, 0.75f, 0.4f)},
    {"Laranja Claro 3", new Color(1f, 0.85f, 0.6f)},
    {"Laranja Escuro 1", new Color(0.8f, 0.4f, 0f)},
    {"Laranja Escuro 2", new Color(0.6f, 0.3f, 0f)},
    {"Laranja Escuro 3", new Color(0.4f, 0.2f, 0f)},

    // Vermelhos
    {"Vermelho Claro 1", new Color(1f, 0.4f, 0.4f)},
    {"Vermelho Claro 2", new Color(1f, 0.6f, 0.6f)},
    {"Vermelho Claro 3", new Color(1f, 0.8f, 0.8f)},
    {"Vermelho Escuro 1", new Color(0.7f, 0f, 0f)},
    {"Vermelho Escuro 2", new Color(0.5f, 0f, 0f)},
    {"Vermelho Escuro 3", new Color(0.3f, 0f, 0f)},

    // Amarelos
    {"Amarelo Claro 1", new Color(1f, 1f, 0.6f)},
    {"Amarelo Claro 2", new Color(1f, 1f, 0.75f)},
    {"Amarelo Claro 3", new Color(1f, 1f, 0.9f)},
    {"Amarelo Escuro 1", new Color(0.8f, 0.8f, 0f)},
    {"Amarelo Escuro 2", new Color(0.6f, 0.6f, 0f)},
    {"Amarelo Escuro 3", new Color(0.4f, 0.4f, 0f)},
};

    [System.Serializable]
    public class ColorData
    {
        public string name; // Ex: "Vermelho", "Amarelo", "Laranja Escuro 2"
        public List<string> components; // Ex: ["Vermelho", "Preto"]

        public ColorData(string name, params string[] components)
        {
            this.name = name;
            this.components = new List<string>(components);
        }
    }   
    [Header("concluir ocjetivo")]
        public List<string> coresParaConclui = new List<string>();
    public UnityEvent ObjetivoConcluiod;

    [Header("Paleta do Jogador")]
    public List<ColorData> myColorPalette = new List<ColorData>();

    [Header("Evento chamado quando uma nova cor é adicionada")]
    public UnityEvent<string> onNewColorAdded;

    public void Start()
    {
        // AddBaseColor("Vermelho");
        // AddBaseColor("Preto");
        // AddBaseColor("Branco");
        // AddBaseColor("Amarelo");
        SelectSlotColor(0);
    }

    void Update(){
        int num =0;
        foreach(var corNecessaria in coresParaConclui)
        {
            foreach (var mycolor in myColorPalette)
            {
                if(corNecessaria == mycolor.name){
                    num++;
                }
            }
        }
        if(num == coresParaConclui.Count && !ObjCompleto){
            ObjetivoConcluiod.Invoke();
            ObjCompleto = true;
        }
    }

    // -------------------- ADICIONAR COR INICIAL --------------------
    public void AddBaseColor(string colorName)
    {
        if (!HasColor(colorName))
        {
            myColorPalette.Add(new ColorData(colorName, colorName));
            // onNewColorAdded?.Invoke(colorName);
            GameObject coisa = Instantiate(prefabmycolorslot, new Vector3(0 - 100f * myColorPalette.Count, 0, 0), Quaternion.identity, mycolorpalette.transform);
            coisa.GetComponent<Image>().color = GetColorVisual(colorName);
            coisa.GetComponent<RectTransform>().anchoredPosition = new Vector2(-140f * myColorPalette.Count, 0);
            coisa.GetComponent<Button>().onClick.AddListener(() => SelectColor(colorName));
            coisa.GetComponent<ColorHover>().SetName(colorName);
        }
        else
        {
            Debug.Log("Você já possui essa cor.");
        }
    }

    // -------------------- MISTURAR CORES --------------------
    public void MixColors()
    {
        if (!HasColor(colorselect1) || !HasColor(colorselect2))
        {
            Debug.LogWarning("Você não possui essas cores para misturar!");
            return;
        }

        string result = GetMixResult(colorselect1, colorselect2);

        if (!string.IsNullOrEmpty(result) && !HasColor(result))
        {
            AddBaseColor(result); // Adiciona a nova cor à paleta
            // myColorPalette.Add(new ColorData(result, colorselect1, colorselect2));
            onNewColorAdded?.Invoke(result);
            Debug.Log("Nova cor adicionada: " + result);
        }
        else
        {
            Debug.Log("Essa mistura não gera nada novo ou já foi descoberta.");
        }
    }

    // -------------------- REGRAS DE MISTURA --------------------
    private string GetMixResult(string a, string b)
    {
        // Normaliza ordem (Vermelho + Amarelo == Amarelo + Vermelho)
        List<string> mix = new List<string>() { a, b };
        mix.Sort();

        
        string combo = string.Join("+", mix);

        // Regras principais
        switch (combo)
{
    case "Amarelo+Branco": return "Amarelo Claro 1";
    case "Amarelo Claro 1+Branco": return "Amarelo Claro 2";
    case "Amarelo Claro 2+Branco": return "Amarelo Claro 3";
    
    case "Amarelo+Preto": return "Amarelo Escuro 1";
    case "Amarelo Escuro 1+Preto": return "Amarelo Escuro 2";
    case "Amarelo Escuro 2+Preto": return "Amarelo Escuro 3";
    
    case "Amarelo+Vermelho": return "Laranja";
    
    case "Branco+Vermelho": return "Vermelho Claro 1";
    case "Branco+Vermelho Claro 1": return "Vermelho Claro 2";
    case "Branco+Vermelho Claro 2": return "Vermelho Claro 3";
    
    case "Branco+Laranja": return "Laranja Claro 1";
    case "Branco+Laranja Claro 1": return "Laranja Claro 2";
    case "Branco+Laranja Claro 2": return "Laranja Claro 3";

    case "Laranja+Preto": return "Laranja Escuro 1";
    case "Laranja Escuro 1+Preto": return "Laranja Escuro 2";
    case "Laranja Escuro 2+Preto": return "Laranja Escuro 3";
    
    case "Preto+Vermelho": return "Vermelho Escuro 1";
    case "Preto+Vermelho Escuro 1": return "Vermelho Escuro 2";
    case "Preto+Vermelho Escuro 2": return "Vermelho Escuro 3";
}

     
       

        return null; // mistura não conhecida
    }

    // -------------------- CHECAR SE JÁ TEM A COR --------------------
    public bool HasColor(string colorName)
    {
        return myColorPalette.Exists(c => c.name == colorName);
    }
    public Color GetColorVisual(string name)
    {
        Debug.Log("GetColorVisual: " + name);
        if (colorVisuals.ContainsKey(name))
            return colorVisuals[name];
        return Color.magenta; // fallback (rosa choque) caso não ache
    }

    public void SelectColor(string color)
    {
        if (chandColorId == 0)
        {
            // slot1.color = Color.yellow;
            // slot1.color = Color.yellow;
            color1.GetComponentInChildren<Image>().color = GetColorVisual(color);
            colorselect1 = color;

        }
        else
        {
            // slot3.GetComponentInChildren<Image>().color = GetColorVisual(color);
            color2.GetComponentInChildren<Image>().color = GetColorVisual(color);
            colorselect2 = color;

        }
        if (colorselect1 != "" && colorselect2 != "")
        {
            string nameColorResult = GetMixResult(colorselect1,colorselect2);
            Debug.Log("ColorResult "+nameColorResult);
            if(nameColorResult != null)
            result.color = GetColorVisual(nameColorResult);
        }
    }
    public void SelectSlotColor(int id)
    {
        chandColorId = id;

        if (chandColorId == 0)
        {
            slot1.color = Color.white;
            slot2.color = new Color(0, 0, 0, 0);
        }
        else
        {
            slot1.color = new Color(0, 0, 0, 0);
            slot2.color = Color.white;
        }
    }
}
