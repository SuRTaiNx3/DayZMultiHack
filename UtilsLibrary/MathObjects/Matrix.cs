using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLibrary.MathObjects
{
    public class Matrix
    {
        #region Globals

        private float[] data;
        private int rows, columns;
        
        #endregion

        #region Properties

        public float this[int i]
        {
            get { return data[i]; }
            set { data[i] = value; }
        }
        public float this[int row, int column]
        {
            get { return data[row * columns + column]; }
            set { data[row * columns + column] = value; }
        }
        #endregion

        #region Constructor

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.data = new float[rows * columns];
        }

        #endregion

        #region Methods

        public void Read(byte[] data)
        {
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < columns; x++)
                    this[y, x] = BitConverter.ToSingle(data, sizeof(float) * ((y * columns) + x));
        }

        public byte[] ToByteArray()
        {
            int sof = sizeof(float);
            byte[] data = new byte[this.data.Length * sof];
            for (int i = 0; i < this.data.Length; i++)
                Array.Copy(BitConverter.GetBytes(this.data[i]), 0, data, i * sof, sof);
            return data;
        }
        #endregion

        
    }
}
