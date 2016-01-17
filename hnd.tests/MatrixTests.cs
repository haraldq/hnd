using System;
using model;
using Xunit;

namespace hnd.tests
{
    public class MatrixTests
    {
        [Fact]
        public void IndexTests()
        {
            var field = new IndexCell[3, 3];

            int cnt = 0;
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    field[i, j] = new IndexCell {Index = cnt++};

        }

        private class IndexCell : Cell
        {
            public override string ToString()
            {
                return Index.ToString();
            }

            public int Index { get; set; }
        }
    }
}