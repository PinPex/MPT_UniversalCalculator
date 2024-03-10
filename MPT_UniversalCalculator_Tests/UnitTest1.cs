using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MPT_UniversalCalculator;

namespace MPT_UniversalCalculator_Tests
{
    [TestClass]
    public class MathOperationTestClass
    {
        [TestMethod]
        public void PlusTestMethod()
        {
            MathOperation<Fract> operation = new MathOperation<Fract>();
            operation.Operand1 = new Fract("1/2");
            operation.Operand2 = new Fract("3/2");
            operation.Operation = "+";
            operation.MakeOperation();
            Assert.AreEqual("2", operation.Result.ToString());
        }
    }

    [TestClass]
    public class ComplexTestClass
    {
        [TestMethod]
        public void ConstructTestMethod()
        {
            Complex complex = new Complex("31232131.023123131 +    i *    131234124214214.412412412412");
            Assert.AreEqual("31232131.023123131 + i * 131234124214214.412412412412", complex.ToString());
        }
        [TestMethod]
        public void PlusTestMethod()
        {
            
        }
    }

    [TestClass]
    public class BigNumberTestClass
    {
        [TestMethod]
        public void ConstructTestMethod()
        {
            
        }
        [TestMethod]
        public void PlusTestMethod()
        {
            BigNumber bigNum1 = BigNumber.Parse("232.322");
            BigNumber bigNum2 = BigNumber.Parse("232.722");
            Assert.AreEqual("465.044", (bigNum1 + bigNum2).ToString());
        }
        [TestMethod]
        public void MinusTestMethod()
        {
            BigNumber bigNum1 = BigNumber.Parse("464.644");
            BigNumber bigNum2 = BigNumber.Parse("232.922");
            Assert.AreEqual("231.722", (bigNum1 - bigNum2).ToString());
        }
    }
}
