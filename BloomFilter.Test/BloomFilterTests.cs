using BloomFilterLibrary;

namespace BloomFilter.Test
{
    [TestClass]
    public class BloomFilterTests
    {
        private BloomFilter<string> _bloomFilter;

        [TestInitialize]
        public void Initialize()
        {
            _bloomFilter = new BloomFilter<string>(1000, (item, length) => item.GetHashCode(), (item, length) => item.GetHashCode());
        }

        [TestMethod]
        public void TestPositiveCase()
        {
            _bloomFilter.Add("test");
            Assert.IsTrue(_bloomFilter.Contains("test"));
        }

        [TestMethod]
        public void TestFalsePositiveCase()
        {
            Assert.IsFalse(_bloomFilter.Contains("test"));
        }

        [TestMethod]
        public void TestNegativeCase()
        {
            _bloomFilter.Add("test");
            Assert.IsFalse(_bloomFilter.Contains("notInFilter"));
        }
    }
}