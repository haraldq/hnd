using model;
using Xunit;

namespace hnd.tests
{
    public class EssTests
    {
        private const int Hawk = 0;
        private const int Dove = 1;

        private static readonly int[,] PayoffMatrix = { { -25, 50 }, { 0, 15 } };

        [Theory,
         InlineData(-25, 50, 0, 15, false, Hawk),
         InlineData(-25, 50, 0, 15, false, Dove),
         InlineData(15, 50, 0, 15, true, Hawk),
         InlineData(15, 10, 15, 5, true, Hawk)]
        public void Strategy_ess(int hh, int hd, int dh, int dd, bool isEss, int hawkOrDove)
        {
            PayoffMatrix[Hawk, Hawk] = hh;
            PayoffMatrix[Hawk, Dove] = hd;
            PayoffMatrix[Dove, Hawk] = dh;
            PayoffMatrix[Dove, Dove] = dd;

            var model = new Ess();

            Assert.True(model.IsEss(PayoffMatrix, hawkOrDove) == isEss);
        }
    }
}