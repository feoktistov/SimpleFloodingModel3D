using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace FloodModel
{
    public static class CellMath
    {
        public static void AddOffest(Vector2 offset, Cell[] cells)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Position += offset;
            }
        }

        public static void AddOffest(Vector2 offset, Vector3[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += new Vector3(offset);
            }
        }

        public static Cell[,] To2DMap(Cell[] cells)
        {
            float width = cells[cells.Length - 1].Position.X - cells[0].Position.X;
            float height = cells[cells.Length - 1].Position.Y - cells[0].Position.Y;
            float h = (float)Math.Sqrt(cells.Length * height / width);
            float w = h * width / height;
            Cell[,] map = new Cell[(int)w, (int)h];
            int index = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    map[i, j] = cells[index];
                    index++;
                }
            }
            return map;
        }

        public static void MakeRelief(Cell[] cells)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].Position.Y > -5 && cells[i].Position.Y < 5)
                {
                    cells[i].surfaceHeight += -(Math.Abs(cells[i].Position.Y) - 5) *
                        (Math.Abs(cells[i].Position.Y) - 5) / 30;
                }
            }
        }

        public static void RandomHeight(float amplitude, Cell[] vertices)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].waterHeight = (float)(amplitude * (rnd.NextDouble() - 0.5));
            }
        }

        public static void ClearWaterLevel(Cell[] cells)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].waterHeight = cells[i].surfaceHeight;
            }
        }

        public static void CheckWaterLevel(Cell[] cells)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].waterHeight < cells[i].surfaceHeight)
                {
                    cells[i].waterHeight = cells[i].surfaceHeight;
                }
            }
        }
    }
}
