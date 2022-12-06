using MultiValDictionary;
using Xunit;

namespace MultiValueDictionary.Tests
{
    public class Add_Tests
    {

        [Fact]
        public void Add_RepeatKeyAdding_ReturnTrue()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 1);
            bool result = mvDictionary.Add("hello", 5);

            Assert.True(result);

            result = mvDictionary.Add("hello", 1);
            Assert.False(result);

            result = mvDictionary.Add("hello", 2);
            Assert.True(result);

            result = mvDictionary.Add("hello", 2);
            Assert.False(result);

        }

       
        [Fact]
        public void Add_MultiplyKeys_TestResize()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            for (int i = 0; i < 100; i++)
            {
               var res= mvDictionary.Add(i.ToString(), i);

                Assert.True(res);

            }

          
        }
    }
}