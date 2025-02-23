// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("FN+i/WLCibLqCs3VxZ0wo/awItDgzWAewhpvy6G7aq1QalOqILPjR66PIkD41T8CMsnm2w+ZpT0GdQ+cOYA+XkcOlRX9375nrvEGfME65p5ZhPjpUjKnNWBF7i4PsbdtdBhIeziKCSo4BQ4BIo5Ajv8FCQkJDQgL7aiQXlNDewRv9Mw8h/FFLovuButPPg55cMeImUY2os/ZXjwak+84Ai1AzTYLMnRHAAr2ssNAXWvE6enA2JlOJXfSvh3wckTQ1NLmHi5+DXiKCQcIOIoJAgqKCQkInHsCBK8wwNMoinqiFsBYyK1XT/E6Ngc0AlEQvgryLvSbOjf6vQVXo9wRG4xfU0VryrcoKG6FKqB8ubm9BBJz9UPrR4UNK+8eyfDYvwoLCQgJ");
        private static int[] order = new int[] { 6,3,2,9,13,6,6,13,8,13,13,11,12,13,14 };
        private static int key = 8;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
