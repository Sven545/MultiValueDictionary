using MultiValDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MultiValueDictionary.Tests
{
    public class RemoveKey_Tests
    {
        [Fact]
        public void RemoveKey_KeyNotExist_ReturnFalse()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 1);
            mvDictionary.Add("hello", 2);
            mvDictionary.Add("hello", 3);

            bool result = mvDictionary.RemoveKey("otherKey");
            Assert.False(result);



        }
        [Fact]
        public void RemoveKey_KeyExist_ReturnTrue()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 1);
            mvDictionary.Add("hello", 2);
            mvDictionary.Add("hello", 3);
            mvDictionary.Add("new", 1);


            var result = mvDictionary.RemoveKey("hello");
            Assert.True(result);
           



        }
    }
}
