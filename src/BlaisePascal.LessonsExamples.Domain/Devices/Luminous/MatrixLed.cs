//using System;

//namespace BlaisePascal.LessonsExamples.Domain.Devices.Luminous
//{
//    public class MatrixLed
//    {
//        private readonly AbstractLamp[,] matrix;

//        public int Rows { get; }
//        public int Cols { get; }

//        public MatrixLed(int rows, int cols, AbstractLamp prototype)
//        {
//            if (rows <= 0 || cols <= 0)
//                throw new ArgumentException("Invalid Matrix dimensions.");

//            if (prototype == null)
//                throw new ArgumentNullException(nameof(prototype));

//            Rows = rows;
//            Cols = cols;

//            matrix = new AbstractLamp[rows, cols];

//            for (int r = 0; r < rows; r++)
//                for (int c = 0; c < cols; c++)
//                    matrix[r, c] = CloneLamp(prototype);
//        }

//        public AbstractLamp this[int r, int c] => matrix[r, c];

//        private AbstractLamp CloneLamp(AbstractLamp lamp)
//        {
//            if (lamp is Lamp lamp1)
//                return new Lamp(lamp1.Name);

//            if (lamp is EcoLamp lamp2)
//                return new EcoLamp(lamp2.Name);

//            throw new InvalidOperationException("Lamp Type not supported.");
//        }

//        // ======================================================
//        // OPERAZIONI DI BASE
//        // ======================================================

//        public void SwitchOnAll()
//        {
//            for (int r = 0; r < Rows; r++)
//                for (int c = 0; c < Cols; c++)
//                    matrix[r, c].SwitchOn();
//        }

//        public void SwitchOffAll()
//        {
//            for (int r = 0; r < Rows; r++)
//                for (int c = 0; c < Cols; c++)
//                    matrix[r, c].SwitchOff();
//        }

//        public void SetIntensityAll(int intensity)
//        {
//            for (int r = 0; r < Rows; r++)
//                for (int c = 0; c < Cols; c++)
//                    matrix[r, c].SetIntensity(intensity);
//        }

//        // ======================================================
//        // PATTERN 1 – CHECKERBOARD
//        // ======================================================
//        public void PatternCheckerboard(int intensityA, int intensityB)
//        {
//            for (int r = 0; r < Rows; r++)
//            {
//                for (int c = 0; c < Cols; c++)
//                {
//                    int intensity = (r + c) % 2 == 0 ? intensityA : intensityB;
//                    matrix[r, c].SwitchOn();
//                    matrix[r, c].SetIntensity(intensity);
//                }
//            }
//        }

//        // ======================================================
//        // PATTERN 2 – WAVE PATTERN
//        // ======================================================

//        /// <summary>Onda orizzontale: ogni riga cresce o decresce.</summary>
//        public void PatternWaveHorizontal(int baseIntensity, int step)
//        {
//            for (int r = 0; r < Rows; r++)
//            {
//                for (int c = 0; c < Cols; c++)
//                {
//                    int intensity = baseIntensity + c * step;
//                    matrix[r, c].SwitchOn();
//                    matrix[r, c].SetIntensity(intensity);
//                }
//            }
//        }

//        /// <summary>Onda verticale: ogni colonna cresce o decresce.</summary>
//        public void PatternWaveVertical(int baseIntensity, int step)
//        {
//            for (int c = 0; c < Cols; c++)
//            {
//                for (int r = 0; r < Rows; r++)
//                {
//                    int intensity = baseIntensity + r * step;
//                    matrix[r, c].SwitchOn();
//                    matrix[r, c].SetIntensity(intensity);
//                }
//            }
//        }

//        // ======================================================
//        // PATTERN 3 – SPIRALE (clockwise)
//        // Tipico LeetCode: "Spiral Matrix"
//        // ======================================================
//        public void PatternSpiral(int startIntensity, int step)
//        {
//            int top = 0;
//            int bottom = Rows - 1;
//            int left = 0;
//            int right = Cols - 1;

//            int current = startIntensity;

//            while (top <= bottom && left <= right)
//            {
//                // riga superiore →
//                for (int c = left; c <= right; c++)
//                {
//                    ApplyIntensity(top, c, ref current, step);
//                }
//                top++;

//                // colonna destra ↓
//                for (int r = top; r <= bottom; r++)
//                {
//                    ApplyIntensity(r, right, ref current, step);
//                }
//                right--;

//                if (top <= bottom)
//                {
//                    // riga inferiore ←
//                    for (int c = right; c >= left; c--)
//                    {
//                        ApplyIntensity(bottom, c, ref current, step);
//                    }
//                    bottom--;
//                }

//                if (left <= right)
//                {
//                    // colonna sinistra ↑
//                    for (int r = bottom; r >= top; r--)
//                    {
//                        ApplyIntensity(r, left, ref current, step);
//                    }
//                    left++;
//                }
//            }
//        }

//        private void ApplyIntensity(int r, int c, ref int intensity, int step)
//        {
//            matrix[r, c].SwitchOn();
//            matrix[r, c].SetIntensity(intensity);
//            intensity += step;
//        }

//        // ======================================================
//        //  PATTERN 4 – SNAKE FILL (zig-zag)
//        //  Tipico LeetCode: "Zigzag matrix / Snake matrix"
//        // ======================================================
//        public void PatternSnakeFill(int startIntensity, int step)
//        {
//            int current = startIntensity;

//            for (int r = 0; r < Rows; r++)
//            {
//                if (r % 2 == 0)
//                {
//                    // → da sinistra a destra
//                    for (int c = 0; c < Cols; c++)
//                    {
//                        ApplyIntensity(r, c, ref current, step);
//                    }
//                }
//                else
//                {
//                    // ← da destra a sinistra
//                    for (int c = Cols - 1; c >= 0; c--)
//                    {
//                        ApplyIntensity(r, c, ref current, step);
//                    }
//                }
//            }
//        }

//        // ======================================================
//        // Utility
//        // ======================================================
//        public void ReverseRows()
//        {
//            for (int r = 0; r < Rows; r++)
//            {
//                int left = 0;
//                int right = Cols - 1;

//                while (left < right)
//                {
//                    var tmp = matrix[r, left];
//                    matrix[r, left] = matrix[r, right];
//                    matrix[r, right] = tmp;

//                    left++;
//                    right--;
//                }
//            }
//        }

//        public void ReverseColumns()
//        {
//            for (int c = 0; c < Cols; c++)
//            {
//                int top = 0;
//                int bottom = Rows - 1;

//                while (top < bottom)
//                {
//                    var tmp = matrix[top, c];
//                    matrix[top, c] = matrix[bottom, c];
//                    matrix[bottom, c] = tmp;

//                    top++;
//                    bottom--;
//                }
//            }
//        }

//        public void Transpose()
//        {
//            if (Rows != Cols)
//                throw new InvalidOperationException("It needs NxN matrix");

//            for (int r = 0; r < Rows; r++)
//                for (int c = r + 1; c < Cols; c++)
//                {
//                    var tmp = matrix[r, c];
//                    matrix[r, c] = matrix[c, r];
//                    matrix[c, r] = tmp;
//                }
//        }
//    }
//}
