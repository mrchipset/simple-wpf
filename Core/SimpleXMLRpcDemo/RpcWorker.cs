using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleXMLRpcDemo
{
    internal class Structure
    {
        [XmlRpcMember("name")]
        public string Name { get; set; }

        [XmlRpcMember("age")]
        public int Age { get; set; }

        [XmlRpcMember("gender")]
        public string Gender { get; set; }
    }

    [XmlRpcUrl("http://127.0.0.1:8000/rpc")]
    internal class RpcWorker : XmlRpcClientProtocol
    {
        [XmlRpcMethod]
        public string Foo()
        {
            return (string)Invoke("Foo", new object[] { });
        }

        [XmlRpcMethod]
        public Structure BarWithStructureInvoke()
        {
            return (Structure)Invoke("BarWithStructureInvoke", new object[] { });
        }

        [XmlRpcMethod]
        public double SumArray(double[] array)
        {
            return (double)Invoke("SumArray", new object[] { array });
        }
    }
}
