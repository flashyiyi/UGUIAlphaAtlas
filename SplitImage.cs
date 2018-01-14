using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public class SplitImage : Image
    {
#if UNITY_IOS
        public override Material material
        {
            get
            {
                if (m_Material != null)
                    return m_Material;

                if (overrideSprite && (overrideSprite.associatedAlphaSplitTexture != null || AlphaAtlasManager.GetInstance().GetAlphaTexture(overrideSprite) != null))
                    return defaultETC1GraphicMaterial;

                return defaultMaterial;
            }

            set
            {
                base.material = value;
            }
        }

        protected override void UpdateMaterial()
        {
            base.UpdateMaterial();

            Texture2D alphaTex = AlphaAtlasManager.GetInstance().GetAlphaTexture(overrideSprite);

            if (alphaTex != null)
            {
                canvasRenderer.SetAlphaTexture(alphaTex);
            }
        }
#endif
    }
}
