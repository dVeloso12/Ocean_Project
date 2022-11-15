using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveEffect : MonoBehaviour
{

    public Material waveMaterial;
    public Texture2D waveTexture;
    public bool reflectiveBoundary;
    public float offset = 0.5f;
    float[][] waveN, waveNm1, waveNp1; // state info

    float Lx = 10; //width
    float Ly = 10; //height
    [SerializeField] float dx = 0.1f; //resolution
    float dy { get => dx;} //y-axis density
    int nx, ny; // resolution

    public float CFL = 0.5f;
    public float c = 1;
    float dt; // time step
    float t;

    [SerializeField] float floatToColorMultiplier = 2f;
    [SerializeField] float pulseFrequency = 1f;
    [SerializeField] float pulseMagnitude = 1f;
    [SerializeField] Vector2Int pulsePosition = new Vector2Int(50,50); 

    private void Start()
    {
        nx = Mathf.FloorToInt(Lx / dx);
        ny = Mathf.FloorToInt(Ly / dy);
        waveTexture = new Texture2D(nx, ny, TextureFormat.RGBA32, false);
        waveN = new float[nx][];
        waveNm1 = new float[nx][];
        waveNp1 = new float[nx][];

        for(int i = 0; i<nx;i++)
        {
            waveN [i]= new float[ny];
            waveNm1[i] = new float[ny];
            waveNp1[i] = new float[ny];
        }

        waveMaterial.SetTexture("_MainTex", waveTexture); //coloring Texture
        waveMaterial.SetTexture("_Displacement", waveTexture); //displacement texture

    }
    void WaveStep()
    {
        dt = CFL * dx / c; //recalculate dt
        t += dx; //increment time

        if (reflectiveBoundary) ApplyReflectiveBoundary();
        else ApplyAbsorptiveBoundary();

        for(int i = 0;i <nx;i++)
        {
            for(int j= 0;j<ny;j++)
            {
                waveNm1[i][j] = waveN[i][j];
                waveN[i][j] = waveNp1[i][j];

            }
        }

        waveN[pulsePosition.x][pulsePosition.y] = dt * dt * offset * pulseMagnitude *Mathf.Sin(t * Mathf.Rad2Deg * pulseFrequency);

        for (int i = 1; i < nx-1; i++)
        {
            for (int j = 1; j < ny-1; j++)
            {

                float n_ij = waveN[i][j];
                float n_ip1j = waveN[i + 1][j];
                float n_im1j = waveN[i -1][j];
                float n_ijp1 = waveN[i][j+1];
                float n_ijm1 = waveN[i][j-1];
                float nm1_ij = waveN[i][j];
                waveNp1[i][j] = 2f * n_ij - nm1_ij + CFL * CFL * (n_ijm1 + n_ijp1 + n_im1j + n_ip1j - 4f * n_ij);
            }
        }
    }
    void ApplyMatrixToTexture(float[][] state, ref Texture2D tex,float floatToColorMultiplier)
    {
        for (int i = 0; i < nx; i++)
        {
            for (int j = 0; j < ny; j++)
            {
                float val = state[i][j] * floatToColorMultiplier;
                tex.SetPixel(i, j, new Color(val+ 0.5f, val + 0.5f, val + 0.5f, 1f));
            }
        }
        tex.Apply();
    }

    void ApplyReflectiveBoundary()
    {
        for (int i = 0; i < nx; i++)
        {
            waveN[i][0] = 0f;
            waveN[i][ny - 1] = 0f;
        }
        for (int j = 0; j < ny; j++)
        {
            waveN[0][j] = 0f;
            waveN[nx - 1][j] = 0f;
        }
    }

    void ApplyAbsorptiveBoundary()
    {
        float v = (CFL - 1f) / (CFL + 1f);
        for (int i = 0; i < nx; i++)
        {
            waveN[i][0] = waveN[i][1] + v * (waveNp1[i][1] - waveN[i][0]) ;
            waveN[i][ny - 1] = waveN[i][ny-2] + v * (waveNp1[i][ny-2] - waveN[i][ny-1]);
        }
        for (int j = 0; j < ny; j++)
        {
            waveNp1[0][j] = waveN[1][j] + v * (waveNp1[1][j] - waveN[0][j]);
            waveNp1[nx - 1][j] = waveN[nx - 2][j] + v * (waveNp1[nx - 2][j] - waveN[nx - 1][j]);
        }
    }
    private Vector2Int MousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return new Vector2Int((int)(hit.textureCoord.x * nx), (int)(hit.textureCoord.y * ny));
        }
        else return new Vector2Int(0, 0);
    }
    private void Update()
    {
        pulsePosition = MousePosition();
        WaveStep();
        ApplyMatrixToTexture(waveN, ref waveTexture, floatToColorMultiplier);
    }
}
