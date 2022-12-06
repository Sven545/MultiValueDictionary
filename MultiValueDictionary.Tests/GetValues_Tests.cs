using MultiValDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MultiValueDictionary.Tests
{
    public class GetValues_Tests
    {
        [Fact]
        public void GetValues_ValuesExists_ReturnValuesCollection()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 1);
            mvDictionary.Add("hello", 2);
            mvDictionary.Add("hello", 3);


            var values = mvDictionary.GetValues("hello");
            Assert.Contains(1, values);
            Assert.Contains(2, values);
            Assert.Contains(3, values);
            Assert.Equal(3, values.Count());

        }

        [Fact]
        public void GetValues_ValuesNotExists_ReturnEmptyCollection()
        {
            var mvDictionary = new MultiValueDictionary<string, int>(10);

            mvDictionary.Add("hello", 1);
            mvDictionary.Add("hello", 2);
            mvDictionary.Add("hello", 3);


            var values = mvDictionary.GetValues("other");
            Assert.Empty( values);
            

        }
    }
}
