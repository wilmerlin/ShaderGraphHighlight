using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Highlighter : MonoBehaviour
{
    // reference to this MeshRenderer component
    private MeshRenderer meshRenderer;
    [SerializeField]

    // non-highlighted material
    private Material originalMaterial;

    // glowing material
    [SerializeField]
    private Material highlightedMaterial;

    void Start()
    {
        // cache a reference to the MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();

        // use non-highlighted material by default
        EnableHighlight(false);
    }

    // toggle betweeen the original and highlighted materials
    public void EnableHighlight(bool onOff)
    {
        if (meshRenderer != null && originalMaterial != null && highlightedMaterial != null)
        {
            meshRenderer.material = onOff ? highlightedMaterial : originalMaterial;
        }
    }

    private void OnMouseOver()
    {
        EnableHighlight(true);
    }

    private void OnMouseExit()
    {
        EnableHighlight(false);
    }

}
