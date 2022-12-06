using MultiValDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MultiValueDictionary.Tests
{
    public class RemoveValue_Tests
    {

        [Fact]
        public void RemoveKey_KeyExistValueNotExist_ReturnFalse()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 1);


            bool result = mvDictionary.RemoveValue("hello", 5);
            Assert.False(result);



        }
        [Fact]
        public void RemoveKey_KeyExistValueExist_ReturnTrue()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 5);


            bool result = mvDictionary.RemoveValue("hello", 5);
            Assert.True(result);


        }

        [Fact]
        public void RemoveKey_KeyNotExist_ReturnFalse()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 5);

            bool result = mvDictionary.RemoveValue("other", 5);
            Assert.False(result);


        }
    }
}
