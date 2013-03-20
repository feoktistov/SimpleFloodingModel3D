using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Diagnostics;

namespace FloodModel
{
    public class Cell
    {
        public Vector2 Position;

        public float waterHeight;
        public float surfaceHeight;
        public float waterSource;

        private float dHeightAdd;
        private float dHeightSpend;

        public void ApplyWaterSource()
        {
            AddWater(waterSource);
        }

        public float AddWater(float dHeight)
        {
            if (dHeight == 0)
                return 0;

            if (dHeight > 0)
            {
                this.dHeightAdd += dHeight;
                return dHeight;
            }
            else
            {
                if (waterHeight + dHeight > surfaceHeight)
                {
                    this.dHeightSpend += dHeight;
                }
                else
                {
                    this.dHeightSpend = waterHeight - surfaceHeight;
                }
                return this.dHeightSpend;
            }
            
        }

        public void Apply()
        {
            waterHeight += dHeightAdd + dHeightSpend;
            dHeightAdd = 0;
            dHeightSpend = 0;
            if (waterHeight < surfaceHeight)
            {
                Debug.Print("Error waterHeight < surfaceHeight");
            }
        }
    }
}
