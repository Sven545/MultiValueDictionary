using MultiValDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MultiValueDictionary.Tests
{
    public class TestCaseFromTask
    {
       

        [Fact]
        public void Add_RepeatKeyRepeatValueAdding_ReturnFalse()
        {
            var mvDictionary = new MultiValueDictionary<string, int>();

            mvDictionary.Add("hello", 1);
            mvDictionary.Add("hello", 13);
            mvDictionary.Add("goodbye", 8);
            bool result = mvDictionary.Add("hello", 5);
            Assert.True(result);

            result = mvDictionary.Add("hello", 1);
            Assert.False(result);
            result = mvDictionary.RemoveKey("aloha");
            Assert.False(result);
            result = mvDictionary.RemoveKey("goodbye");
            Assert.True(result);

            result = mvDictionary.RemoveValue("goodbye", 13);
            Assert.False(result);

            result = mvDictionary.RemoveValue("hello", 13);
            Assert.True(result);

            IEnumerable<int> list = mvDictionary.GetValues("aloha");
            Assert.Empty(list);

            list = mvDictionary.GetValues("hello");
            Assert.Contains(1, list);
            Assert.Contains(5, list);


        }
    }
}
