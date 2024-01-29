using Newtonsoft.Json;
using System.Collections;

namespace BloomFilterLibrary
{
    public class BloomFilter<T>
    {
        private BitArray _bitArray;
        private Func<T, int, int> _hashFunc1;
        private Func<T, int, int> _hashFunc2;

        public BloomFilter(int capacity, Func<T, int, int> hashFunc1, Func<T, int, int> hashFunc2)
        {
            _bitArray = new BitArray(capacity);
            _hashFunc1 = hashFunc1;
            _hashFunc2 = hashFunc2;
        }

        public void Add(T item)
        {
            int hash1 = Math.Abs(_hashFunc1(item, _bitArray.Length) % _bitArray.Length);
            int hash2 = Math.Abs(_hashFunc2(item, _bitArray.Length) % _bitArray.Length);

            _bitArray[hash1] = true;
            _bitArray[hash2] = true;
        }

        /// <summary>
        /// The hashcode value being negative is expected behavior in C#, as the GetHashCode() method returns an integer, which can be either positive or negative. This is because integers in C# are signed types, meaning they can hold both positive and negative values.

        //However, in the context of a bloom filter, we usually only care about the magnitude of the hash value, not whether it's positive or negative. So, to ensure that the hash value is always positive, you can take the absolute value of the hashcode.

        //Here's how you can modify the Add and Contains methods in the BloomFilter class to apply this restriction:
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public bool Contains(T item)
        {
            int hash1 = Math.Abs(_hashFunc1(item, _bitArray.Length) % _bitArray.Length);
            int hash2 = Math.Abs(_hashFunc2(item, _bitArray.Length) % _bitArray.Length);

            return _bitArray[hash1] && _bitArray[hash2];
        }

        public void Save(string filePath)
        {
            byte[] bytes = new byte[_bitArray.Count / 8 + (_bitArray.Count % 8 == 0 ? 0 : 1)];
            _bitArray.CopyTo(bytes, 0);
            string base64String = Convert.ToBase64String(bytes);

            var jsonObject = new { Data = base64String };
            string jsonString = JsonConvert.SerializeObject(jsonObject);

            File.WriteAllText(filePath, jsonString);
        }

        public static BloomFilter<T> Load(string filePath, int capacity, Func<T, int, int> hashFunc1, Func<T, int, int> hashFunc2)
        {
            string jsonString = File.ReadAllText(filePath);
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

            byte[] bytes = Convert.FromBase64String((string)jsonObject.Data);
            BitArray bitArray = new BitArray(bytes);

            return new BloomFilter<T>(capacity, hashFunc1, hashFunc2)
            {
                _bitArray = bitArray
            };
        }


    }

}
