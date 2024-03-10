using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT_UniversalCalculator
{
    public class MathOperation<T> where T : IComparable, IConvertible
    {
        private T operand1;
        private T operand2;
        private T result;
        private String operation = "";
        Dictionary<string, Func<T, T, T>> operations;

        public MathOperation()
        {
            operations = new Dictionary<string, Func<T, T, T>>();
            operations.Add("+", (a, b) => (dynamic)a + (dynamic)b);
            operations.Add("-", (a, b) => (dynamic)a - (dynamic)b);
            operations.Add("*", (a, b) => (dynamic)a * (dynamic)b);
            operations.Add("/", (a, b) => (dynamic)a / (dynamic)b);
        }
        public T Operand1
        {
            get { return operand1; }
            set { operand1 = value; }
        }
        public T Operand2
        {
            get { return operand2; }
            set { operand2 = value; }
        }
        public T Result
        {
            get { return result; }
            set { result = value; }
        }

        public String Operation
        {
            get
            {
                return operation;
            }
            set
            {
                operation = value;
            }
        }
        public void MakeOperation(String operation_)
        {
            operation = operation_;
            result = operations[operation](operand1, operand2);
        }
        public void MakeOperation()
        {
            result = operations[operation](operand1, operand2);

        }

    }
}
