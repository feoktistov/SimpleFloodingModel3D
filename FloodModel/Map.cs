using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace FloodModel
{
    public class Map
    {
        Cell[] cells;
        Cell[,] cellsMapped;
        List<Cell> cellsWithWaterSource;
        List<int> indices;
        List<int> indicesMesh;

        Vector3[] terrain;

        public void Init()
        {
            CalculateVertices(20, 20, 1f);

            CellMath.AddOffest(new Vector2(-10, -10), cells);
            CellMath.AddOffest(new Vector2(-10, -10), terrain);

            CellMath.MakeRelief(cells);
            CellMath.ClearWaterLevel(cells);

            cellsMapped = CellMath.To2DMap(cells);

            cellsWithWaterSource = new List<Cell>();
            cellsMapped[10, 10].waterSource = 0.08f;
            cellsWithWaterSource.Add(cellsMapped[10, 10]);

        }

        public void Randomize(float amplitude)
        {
            CellMath.RandomHeight(amplitude, cells);
            CellMath.CheckWaterLevel(cells);
        }

        public static float DeltaHeight = 0.01f;

        public void Step()
        {
            int width = cellsMapped.GetLength(0);
            int height = cellsMapped.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    List<KeyValuePair<float, Cell>> cellList = new List<KeyValuePair<float, Cell>>();
                    float sumHeight = 0;
                    Cell activeCell = cellsMapped[i,j];
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            if (i + k < 0 || i + k >= width)
                                continue;
                            if (j + l < 0 || j + l >= width)
                                continue;
                            Cell curCell = cellsMapped[i + k, j + l];
                            float dHeight = activeCell.waterHeight - curCell.waterHeight;
                            if (dHeight > 0)
                            {
                                cellList.Add(new KeyValuePair<float, Cell>(dHeight, curCell));
                                sumHeight += dHeight;
                            }
                        }
                        float dH = Math.Abs(activeCell.AddWater(-DeltaHeight));
                        foreach (KeyValuePair<float, Cell> heightAndCell in cellList)
                        {
                            heightAndCell.Value.AddWater(dH * heightAndCell.Key / sumHeight);
                        }
                    }
                }
            }
            foreach (Cell cell in cellsWithWaterSource)
            {
                cell.ApplyWaterSource();
            }
            foreach (Cell cell in cells)
            {
                cell.Apply();
            }
        }

        #region Cells init methods

        void CalculateVertices(float width, float height, float size)
        {

            int widthInPoint = (int)(width / size);
            int heightInPoint = (int)(height / size);
            if (widthInPoint == Math.Truncate(width / size))
            {
                widthInPoint += 1;
            }
            if (heightInPoint == Math.Truncate(height / size))
            {
                heightInPoint += 1;
            }
            cells = new Cell[widthInPoint * heightInPoint];
            int i = 0;

            for (int row = 0; row < heightInPoint; row++)
            {
                for (int col = 0; col < widthInPoint; col++)
                {
                    cells[i] = new Cell();
                    cells[i].Position.X = col * size;
                    cells[i].Position.Y = row * size;
                    i++;
                }
            }


            indices = new List<int>();

            // Set up indices
            for (int r = 0; r < heightInPoint - 1; ++r)
            {
                if (r != 0)
                {
                    indices.Add(r * widthInPoint);
                }
                for (int c = 0; c < widthInPoint; ++c)
                {
                    indices.Add(r * widthInPoint + c);
                    indices.Add((r + 1) * widthInPoint + c);
                }
                if (r != heightInPoint - 2)
                {
                    indices.Add((r + 1) * widthInPoint + (widthInPoint - 1));
                }
            }

            indicesMesh = new List<int>();
            for (int r = 0; r < heightInPoint; ++r)
            {
                for (int c = widthInPoint - 1; c >= 0; --c)
                {
                    indicesMesh.Add(r * widthInPoint + c);
                }
                if (r != heightInPoint - 1)
                {
                    for (int c = 0; c < widthInPoint - 1; ++c)
                    {
                        indicesMesh.Add((r + 1) * widthInPoint + c);
                        indicesMesh.Add(r * widthInPoint + c + 1);
                    }
                }
            }

            terrain = new Vector3[16];
            float tWidth = width * 100;
            float tHeight = height * 100;
            int index = 0;

            terrain[index] = new Vector3(-tWidth, -tHeight, 0);
            index++;
            terrain[index] = new Vector3(0, -tHeight, 0);
            index++;
            terrain[index] = new Vector3(0, tHeight, 0);
            index++;
            terrain[index] = new Vector3(-tWidth, tHeight, 0);
            index++;

            terrain[index] = new Vector3(width + tWidth, -tHeight, 0);
            index++;
            terrain[index] = new Vector3(width, -tHeight, 0);
            index++;
            terrain[index] = new Vector3(width, tHeight, 0);
            index++;
            terrain[index] = new Vector3(width + tWidth, tHeight, 0);
            index++;

            terrain[index] = new Vector3(0, -tHeight, 0);
            index++;
            terrain[index] = new Vector3(width, -tHeight, 0);
            index++;
            terrain[index] = new Vector3(width, 0, 0);
            index++;
            terrain[index] = new Vector3(0, 0, 0);
            index++;

            terrain[index] = new Vector3(0, height, 0);
            index++;
            terrain[index] = new Vector3(0, height + tHeight, 0);
            index++;
            terrain[index] = new Vector3(width, height + tHeight, 0);
            index++;
            terrain[index] = new Vector3(width, height, 0);
            index++;
        }

        #endregion

        #region  DrawMap

        public void DrawMap()
        {
            DrawSurface();
            DrawWater();
        }

        private void DrawWater()
        {

            GL.Begin(BeginMode.LineStrip);
            GL.Color3(Color.Blue);
            foreach (var element in indicesMesh)
            {
                var vertex = cells[element];
                GL.Vertex3(new Vector3(vertex.Position.X, vertex.Position.Y, vertex.waterHeight));

            }
            GL.End();

            GL.Begin(BeginMode.TriangleStrip);
            GL.Color3(Color.DarkBlue);
            foreach (var element in indices)
            {
                var vertex = cells[element];
                GL.Vertex3(new Vector3(vertex.Position.X, vertex.Position.Y, vertex.waterHeight));

            }
            GL.End();

        }

        private void DrawSurface()
        {

            GL.Begin(BeginMode.Quads);
            GL.Color3(Color.Green);
            foreach (Vector3 vect in terrain)
            {
                GL.Vertex3(vect);
            }

            GL.End();

            GL.Begin(BeginMode.LineStrip);
            GL.Color3(Color.GreenYellow);
            foreach (var element in indicesMesh)
            {
                var vertex = cells[element];
                GL.Vertex3(new Vector3(vertex.Position.X, vertex.Position.Y, vertex.surfaceHeight));

            }
            GL.End();

            GL.Begin(BeginMode.TriangleStrip);
            GL.Color3(Color.Green);
            foreach (var element in indices)
            {
                var vertex = cells[element];
                GL.Vertex3(new Vector3(vertex.Position.X, vertex.Position.Y, vertex.surfaceHeight));

            }
            GL.End();

        }

        #endregion
    }
}
