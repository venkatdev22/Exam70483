using System;
using System.Collections.Generic;
using System.Linq;

namespace Hashing
{
    /// <summary>
    /// LISTING 3-21  A naïve set implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public class Set<T>
    //{
    //    private List<T> list = new List<T>();

    //    public void Insert(T item)
    //    {
    //        if (!Contains(item))
    //        {
    //            list.Add(item);
    //        }
    //    }

    //    public bool Contains(T item)
    //    {
    //        foreach (T member in list)
    //        {
    //            if (member.Equals(item))
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}

    /// <summary>
    /// LISTING 3-22  A set implementation that uses hashing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Set<T>
    {
        private List<T>[] buckets = new List<T>[100];

        public void Insert(T Item)
        {
            int bucket = GetBucket(Item.GetHashCode());
            if (Contains(Item, bucket))
            {
                return;
            }
            if (buckets[bucket] == null)
            {
                buckets[bucket] = new List<T>();
            }
            buckets[bucket].Add(Item);
        }

        public bool Contains(T Item)
        {
            return Contains(Item, GetBucket(Item.GetHashCode()));
        }

        private int GetBucket(int hashcode)
        {
            /* A Hash code can be negative. To make sure that you end up with a positive   
               value, cast the value to an unsigned int. 
               The unchecked block makes sure that you can cast a value larger then int to an int safely.
             */
            unchecked
            {
                return (int)((uint)hashcode % (uint)buckets.Length);
            }
        }

        private bool Contains(T Item, int bucket)
        {
            if (buckets[bucket] != null)
            {
                foreach (T member in buckets[bucket])
                {
                    if (member.Equals(Item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Text.UnicodeEncoding byteConverter = new System.Text.UnicodeEncoding();
                byte[] message = byteConverter.GetBytes("Hash message to compute");

                System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create();

                byte[] Hash_message = sha256.ComputeHash(message);

                byte[] unChangedMessage = byteConverter.GetBytes("Hash message to compute");
                byte[] Hash_unChangedMessage = sha256.ComputeHash(unChangedMessage);

                byte[] changedMessage = byteConverter.GetBytes("Fake message to compute");
                byte[] Hash_changedMessage = sha256.ComputeHash(changedMessage);

                Console.WriteLine("Hash_message equal Hash_unChangedMessage:{0}", Hash_message.SequenceEqual(Hash_unChangedMessage));
                Console.WriteLine("Hash_message equal Hash_changedMessage :{0}", Hash_message.SequenceEqual(Hash_changedMessage));
            }
            catch(Exception Ex)
            { Console.WriteLine(Ex); }

            Console.ReadKey();
        }
    }
}
