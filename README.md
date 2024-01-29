# BloomFilter- DotNET
This library is a bloom filter implementation - which will help the user to determine the unique entries.4

# What is Bloom Filter
A Bloom filter is a data structure designed to tell you, rapidly and memory-efficiently, whether an element is present in a set. The price paid for this efficiency is that a Bloom filter is a probabilistic data structure: it tells us that the element either definitely is not in the set or may be in the set.

Let’s consider an example. Suppose we have a Bloom filter for checking whether a number is in a set, and we’ve added the numbers 1, 2, and 3 to it. If we check the number 4, the Bloom filter will definitively tell us that 4 is not in the set. But if we check for the number 2, the Bloom filter will tell us that 2 might be in the set.
