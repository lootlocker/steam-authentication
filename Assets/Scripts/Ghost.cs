using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public Transform parentTransform;
    public RectTransform parentRect;

    private Image parentImage;

    public int numberOfGhosts;

    private List<RectTransform> ghosts = new List<RectTransform>();

    public List<Vector2> sizes = new List<Vector2>();

    public List<Vector3> positions = new List<Vector3>();

    public List<Quaternion> rotations = new List<Quaternion>();

    public List<Color> colors = new List<Color>();

    public List<Image> images = new List<Image>();

    public List<RectTransform> rects = new List<RectTransform>();

    public GameObject ghostPrefab;

    private Vector4 colorVector = new Vector4(1f, 1f, 1f, 1f);

    public float ghostIncrementalAlpha = 0.7f;

    public bool off;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 600;
        parentImage = parentRect.GetComponent<Image>();
        CreateGhosts();
    }

    void CreateGhosts()
    {
        for (int i = 0; i < numberOfGhosts; i++)
        {
            GameObject newGhost = Instantiate(ghostPrefab, parentTransform);
            newGhost.transform.position = parentRect.position;
            newGhost.transform.SetAsFirstSibling();
            ghosts.Add(newGhost.GetComponent<RectTransform>());
            sizes.Add(Vector2.zero);
            rotations.Add(Quaternion.identity);
            positions.Add(Vector3.zero);
            colors.Add(Color.black);
            images.Add(newGhost.GetComponent<Image>());
            rects.Add(newGhost.GetComponent<RectTransform>());
            images[i].raycastTarget = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(off)
        {
            for (int i = 0; i < numberOfGhosts; i++)
            {
                images[i].color = Vector4.zero;
            }
            return;
        }
        for (int i = 0; i < numberOfGhosts; i++)
        {
            if(i == 0)
            {
                ghosts[i].sizeDelta = sizes[i];
                ghosts[i].rotation = rotations[i];
                rects[i].position = positions[i];

                colorVector = parentImage.color;
                colorVector.w = ghostIncrementalAlpha;
                images[i].color = colorVector;

                sizes[i] = parentRect.sizeDelta;
                rotations[i] = parentRect.rotation;
                positions[i] = parentRect.position;
            }
            else
            {
                ghosts[i].sizeDelta = sizes[i];
                ghosts[i].rotation = rotations[i];
                rects[i].position = positions[i];
                colorVector = images[i - 1].color;
                colorVector.w *= ghostIncrementalAlpha;
                images[i].color = colorVector;

                sizes[i] = ghosts[i-1].sizeDelta;
                rotations[i] = ghosts[i - 1].rotation;
                positions[i] = rects[i - 1].position;
            }
        }
    }
}
