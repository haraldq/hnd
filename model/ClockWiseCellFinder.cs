//namespace model
//{
//    public class ClockWiseCellFinder : ICellFinder
//    {
//        public Cell Pick(Cell[,] field, int row, int column, out int newRow, out int newColumn)
//        {
//            var rowLength = field.GetLength(0);
//            var columnLength = field.GetLength(1);

//            // up
//            if (row - 1 >= 0)
//            {
//                newRow = row - 1;
//                newColumn = column;

//                return field[newRow, newColumn];
//            }

//            // right
//            if (column + 1 < columnLength)
//            {
//                newRow = row;
//                newColumn = column + 1;

//                return field[newRow, newColumn];
//            }

//            //down
//            if (row + 1 < rowLength)
//            {
//                newRow = row + 1;
//                newColumn = column;

//                return field[newRow, newColumn];
//            }

//            //left
//            if (column - 1 >= 0)
//            {
//                newRow = row;
//                newColumn = column - 1;

//                return field[newRow, newColumn];
//            }

//            newRow = -1;
//            newColumn = -1;

//            return null;
//        }

//        public void PickWithoutAgent(Cell[,] field, int row, int column, out int newRow, out int newColumn)
//        {
//            var rowLength = field.GetLength(0);
//            var columnLength = field.GetLength(1);

//            // up
//            if (row - 1 >= 0 && field[row - 1, column].Agent == null)
//            {
//                newRow = row - 1;
//                newColumn = column;
//            }

//            // up right
//            else if (row - 1 >= 0 && column + 1 < columnLength && field[row - 1, column + 1].Agent == null)
//            {
//                newRow = row - 1;
//                newColumn = column + 1;
//            }

//            // right
//            else if (column + 1 < columnLength && field[row, column + 1].Agent == null)
//            {
//                newRow = row;
//                newColumn = column + 1;
//            }
//            // down right
//            else if (column + 1 < columnLength && row + 1 < rowLength && field[row + 1, column + 1].Agent == null)
//            {
//                newRow = row + 1;
//                newColumn = column + 1;
//            }
//            //down
//            else if (row + 1 < rowLength && field[row + 1, column].Agent == null)
//            {
//                newRow = row + 1;
//                newColumn = column;
//            }
//            // down left
//            else if (row + 1 < rowLength && column - 1 >= 0 && field[row + 1, column - 1].Agent == null)
//            {
//                newRow = row + 1;
//                newColumn = column - 1;
//            }
//            //left
//            else if (column - 1 >= 0 && field[row, column - 1].Agent == null)
//            {
//                newRow = row;
//                newColumn = column - 1;
//            }
//            // left up
//            else if (column - 1 >= 0 && row - 1 >= 0 && field[row - 1, column - 1].Agent == null)
//            {
//                newRow = row - 1;
//                newColumn = column - 1;
//            }
//            // none available
//            else
//            {
//                newRow = -1;
//                newColumn = -1;
//            }
//        }
//    }
//}