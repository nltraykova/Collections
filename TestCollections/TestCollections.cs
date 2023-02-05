using Collections;

namespace TestCollections
{
    public class TestCollections
    {
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            Collection<int> collection = new Collection<int>();

            Assert.That(collection.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            Collection<int> collection = new Collection<int>(5);

            Assert.That(collection.ToString(), Is.EqualTo("[5]"));
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            Collection<int> collection = new Collection<int>(5, 16, 271);

            Assert.That(collection.ToString(), Is.EqualTo("[5, 16, 271]"));
        }

        //DDT tests
        [TestCase("", "[]")]
        [TestCase("5", "[5]")]
        [TestCase("5, 16, 271", "[5, 16, 271]")]

        public void Test_Collection_ConstructorDDT(string data, string expected)
        {
            Collection<int> collection = new Collection<int>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

            Assert.That(collection.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_Add()
        {
            Collection<int> collection = new Collection<int>(5, 16, 271);

            collection.Add(-7);

            Assert.That(collection.ToString(), Is.EqualTo("[5, 16, 271, -7]"));
        }

        //DDT tests
        [TestCase("", 88, "[88]")]
        [TestCase("5, 16, 271", -7, "[5, 16, 271, -7]")]
        [TestCase("5, 16, 271", 2, "[5, 16, 271, 2]")]

        public void Test_Collection_AddDDT(string data, int addData, string expected)
        {
            Collection<int> collection = new Collection<int>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

            collection.Add(addData);

            string actual = collection.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            Collection<int> collection = new Collection<int>(5, 16, 271);

            int oldCapacity = collection.Capacity;

            collection.Add(-7);

            int newCapacity = collection.Capacity;

            Assert.That(newCapacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(newCapacity, Is.GreaterThanOrEqualTo(collection.Count));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            Collection<int> collection = new Collection<int>(5, 16, 271);

            collection.AddRange(new int[] {37, -8, 55});

            Assert.That(collection.ToString(), Is.EqualTo("[5, 16, 271, 37, -8, 55]"));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            collection[1] = "Maria";

            Assert.That(collection[1], Is.EqualTo("Maria"));
            Assert.That(collection[0], Is.EqualTo("Ivan"));
        }

        //DDT tests
        [TestCase("Dimitar", 0, "Dimitar")]
        [TestCase("Ivan, Stephan, Dimitar", 0, "Ivan")]
        [TestCase("Ivan, Stephan, Dimitar", 1, "Stephan")]
        [TestCase("Ivan, Stephan, Dimitar", 2, "Dimitar")]

        public void Test_Collection_GetByIndexDDT(string data, int index, string expected)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            string actual = collection[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            Assert.That(() => collection[10], Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => collection[-1], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        //DDT tests
        [TestCase("", 0)]
        [TestCase("Ivan, Stephan, Dimitar", -1)]
        [TestCase("Ivan, Stephan, Dimitar", 3)]
        [TestCase("Ivan, Stephan, Dimitar", 150)]

        public void Test_Collection_GetByInvalidIndexDDT(string data, int index)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => collection[index], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            Collection<int> collection = new Collection<int>();

            int oldCapacity = collection.Capacity;

            int[] newRange = { 8, 12, 96, 898, 5897, 5, 3, 9, 25, 17, 78, 45, 99, -9, 77, 10, 88, 88, 99 };

            collection.AddRange(newRange);

            int newCapacity = collection.Capacity;

            string expectedCollection = $"[{string.Join(", ", newRange)}]";

            Assert.That(newCapacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(newCapacity, Is.GreaterThanOrEqualTo(collection.Count));
            Assert.That(collection.ToString(), Is.EqualTo(expectedCollection));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            collection.InsertAt(0, "Peter");

            Assert.That(collection.ToString(), Is.EqualTo("[Peter, Ivan, Stephan, Dimitar]"));
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int lastIndex = collection.Count; 

            collection.InsertAt(lastIndex, "Peter");

            Assert.That(collection[collection.Count-1], Is.EqualTo("Peter"));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int middleIndex = collection.Count / 2;
            
            collection.InsertAt(middleIndex, "Peter");

            Assert.That(collection[middleIndex], Is.EqualTo("Peter"));
        }

        //DDT tests
        [TestCase("", 0, "Petar", "[Petar]")]
        [TestCase("Petar", 0, "Ivan", "[Ivan, Petar]")]
        [TestCase("Ivan, Stephan, Dimitar", 0, "Petar", "[Petar, Ivan, Stephan, Dimitar]")]
        [TestCase("Ivan, Stephan, Dimitar", 3, "Petar", "[Ivan, Stephan, Dimitar, Petar]")]
        [TestCase("Ivan, Stephan, Dimitar", 1, "Petar", "[Ivan, Petar, Stephan, Dimitar]")]

        public void Test_Collection_InsertAtDDT(string data, int index, string insertData, string expected)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            collection.InsertAt(index, insertData);

            string actual = collection.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int oldCapacity = collection.Capacity;

            collection.InsertAt(1, "Peter");

            int newCapacity = collection.Capacity;

            Assert.That(newCapacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(newCapacity, Is.GreaterThanOrEqualTo(collection.Count));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            Assert.That(() => { collection.InsertAt(11, "Peter"); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        //DDT tests
        [TestCase("", 1, "Peter")]
        [TestCase("Ivan, Stephan, Dimitar", -1, "Peter")]
        [TestCase("Ivan, Stephan, Dimitar", 11, "Peter")]

        public void Test_Collection_InsertAtInvalidIndexDDT(string data, int index, string insertData)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => { collection.InsertAt(index, insertData); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int middleIndex = collection.Count / 2;

            collection.Exchange(middleIndex, 2);

            Assert.That(collection[middleIndex], Is.EqualTo("Dimitar"));
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int firstIndex = 0;
            int lastIndex = collection.Count - 1;

            collection.Exchange(firstIndex, lastIndex);

            Assert.That(collection[firstIndex], Is.EqualTo("Dimitar"));
            Assert.That(collection[lastIndex], Is.EqualTo("Ivan"));
        }

        //DDT tests
        [TestCase("Ivan, Stephan, Dimitar", 1, 2, "[Ivan, Dimitar, Stephan]")]
        [TestCase("Ivan, Stephan, Dimitar, Yordan", 0, 3, "[Yordan, Stephan, Dimitar, Ivan]")]

        public void Test_Collection_ExchangeDDT(string data, int index1, int index2, string expected)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            collection.Exchange(index1, index2);

            string actual = collection.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            Assert.That(() => { collection.Exchange(11, 0); }, Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { collection.Exchange(11, 25); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            
            Assert.That(() => { collection.Exchange(1, -12); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        //DDT tests
        [TestCase("", 11, 0)]
        [TestCase("Ivan, Stephan, Dimitar", 11, 0)]
        [TestCase("Ivan, Stephan, Dimitar", 11, 25)]
        [TestCase("Ivan, Stephan, Dimitar", 1, -12)]

        public void Test_Collection_ExchangeInvalidIndexesDDT(string data, int index1, int index2)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => { collection.Exchange(index1, index2); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            collection.RemoveAt(0);

            Assert.That(collection.ToString(), Is.EqualTo("[Stephan, Dimitar]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int oldLastIndex = collection.Count - 1;

            collection.RemoveAt(oldLastIndex);

            int newLastIndex = collection.Count - 1;

            Assert.That(collection[newLastIndex], Is.EqualTo("Stephan"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            int middleIndex = collection.Count / 2;

            collection.RemoveAt(middleIndex);

            Assert.That(collection[middleIndex], Is.EqualTo("Dimitar"));
        }

        //DDT tests
        [TestCase("Ivan", 0, "[]")]
        [TestCase("Ivan, Stephan, Dimitar", 0, "[Stephan, Dimitar]")]
        [TestCase("Ivan, Stephan, Dimitar", 2, "[Ivan, Stephan]")]
        [TestCase("Ivan, Stephan, Dimitar", 1, "[Ivan, Dimitar]")]

        public void Test_Collection_RemoveAtDDT(string data, int index, string expected)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            collection.RemoveAt(index);

            string actual = collection.ToString();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            Assert.That(() => { collection.RemoveAt(11); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        //DDT tests
        [TestCase("", 0)]
        [TestCase("Ivan, Stephan, Dimitar", -1)]
        [TestCase("Ivan, Stephan, Dimitar", 3)]
        [TestCase("Ivan, Stephan, Dimitar", 11)]

        public void Test_Collection_RemoveAtInvalidIndexDDT(string data, int index)
        {
            Collection<string> collection = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => { collection.RemoveAt(index); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }

            Assert.That(collection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            collection.Clear();

            Assert.That(collection.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            Collection<string> collection = new Collection<string>("Ivan", "Stephan", "Dimitar");

            Assert.That(collection.Count, Is.EqualTo(3));
            Assert.That(collection.Capacity, Is.EqualTo(16));
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            Collection<string> collection = new Collection<string>();

            Assert.That(collection.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringSingle()
        {
            Collection<string> collection = new Collection<string>("Peter");

            Assert.That(collection.ToString(), Is.EqualTo("[Peter]"));
        }

        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            Collection<string> collection = new Collection<string>("Peter", "Georgi", "Simeon", "Atanas");

            Assert.That(collection.ToString(), Is.EqualTo("[Peter, Georgi, Simeon, Atanas]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            Collection<string> collectionNames = new Collection<string>("Peter", "Georgi");
            Collection<int> collectionNums = new Collection<int>(26, 258, 79);
            Collection<DateTime> collectionDates = new Collection<DateTime>();
            
            Collection<object> collectionNested = new Collection<object>(collectionNames, collectionNums, collectionDates);


            Assert.That(collectionNested.ToString(), Is.EqualTo("[[Peter, Georgi], [26, 258, 79], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            Collection<int> collection = new Collection<int>();

            const int itemsCount = 1000000;

            collection.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            Assert.That(collection.Count == itemsCount);
            Assert.That(collection.Capacity >= itemsCount);

            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }

            Assert.That(collection.ToString() == "[]");
            Assert.That(collection.Capacity >= collection.Count);
        }
    }
}