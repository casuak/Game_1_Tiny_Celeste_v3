using TinyCeleste._01_Framework;
using UnityEngine;

namespace TinyCeleste._02_Modules._02_Camera
{
    public class C_CameraRippleEffect : EntityComponent
    {
        public AnimationCurve waveform = new AnimationCurve(
            new Keyframe(0.00f, 0.50f, 0, 0),
            new Keyframe(0.05f, 1.00f, 0, 0),
            new Keyframe(0.15f, 0.10f, 0, 0),
            new Keyframe(0.25f, 0.80f, 0, 0),
            new Keyframe(0.35f, 0.30f, 0, 0),
            new Keyframe(0.45f, 0.60f, 0, 0),
            new Keyframe(0.55f, 0.40f, 0, 0),
            new Keyframe(0.65f, 0.55f, 0, 0),
            new Keyframe(0.75f, 0.46f, 0, 0),
            new Keyframe(0.85f, 0.52f, 0, 0),
            new Keyframe(0.99f, 0.50f, 0, 0)
        );

        // 波纹的跟踪对象
        public Transform followTarget;
        
        [Range(0.01f, 1.0f)]
        public float refractionStrength = 0.5f;

        public Color reflectionColor = Color.gray;

        [Range(0.01f, 1.0f)]
        public float reflectionStrength = 0.7f;

        [Range(1.0f, 3.0f)]
        public float waveSpeed = 1.25f;
        
        [SerializeField, HideInInspector]
        private Shader shader = null;

        private class Droplet
        {
            private Vector2 position;
            private float time;

            public Droplet()
            {
                time = 1000;
            }

            public void Reset()
            {
                time = 0;
            }

            public void Stop()
            {
                time = 1000f;
            }

            public void Update(Vector2 screenPos)
            {
                time += Time.deltaTime;
                position = screenPos;
            }

            public Vector4 MakeShaderParameter(float aspect)
            {
                return new Vector4(position.x * aspect, position.y, time, 0);
            }
        }

        private Droplet droplet;
        private Texture2D gradTexture;
        private Material material;
        private float timer;
        private int dropCount;
        private Camera m_Camera;

        private void Awake()
        {
            droplet = new Droplet();

            gradTexture = new Texture2D(2048, 1, TextureFormat.Alpha8, false);
            gradTexture.wrapMode = TextureWrapMode.Clamp;
            gradTexture.filterMode = FilterMode.Bilinear;
            for (var i = 0; i < gradTexture.width; i++)
            {
                var x = 1.0f / gradTexture.width * i;
                var a = waveform.Evaluate(x);
                gradTexture.SetPixel(i, 0, new Color(a, a, a, a));
            }

            gradTexture.Apply();

            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            material.SetTexture("_GradTex", gradTexture);

            UpdateShaderParameters();
        }
        
        private void UpdateShaderParameters()
        {
            m_Camera = GetComponent<Camera>();

            material.SetVector("_Drop1", droplet.MakeShaderParameter(m_Camera.aspect));

            material.SetColor("_Reflection", reflectionColor);
            material.SetVector("_Params1", new Vector4(m_Camera.aspect, 1, 1 / waveSpeed, 0));
            material.SetVector("_Params2", new Vector4(1, 1 / m_Camera.aspect, refractionStrength, reflectionStrength));
        }

        private void Update()
        {
            var screenPos = m_Camera.WorldToScreenPoint(followTarget.transform.position);
            screenPos.x /= m_Camera.pixelWidth;
            screenPos.y /= m_Camera.pixelHeight;
            droplet.Update(screenPos);
            UpdateShaderParameters();
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, material);
        }

        public void Emit()
        {
            droplet.Reset();
        }

        public void Stop()
        {
            droplet.Stop();
        }
    }
}