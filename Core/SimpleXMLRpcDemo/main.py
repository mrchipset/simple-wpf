from xmlrpc.server import SimpleXMLRPCServer, SimpleXMLRPCRequestHandler
import logging
import numpy as np

logging.basicConfig(level=logging.INFO)
class RequestHandler(SimpleXMLRPCRequestHandler):
    rpc_paths = ('/rpc',)

class Funcs:
    def __init__(self) -> None:
       # do nothing in this function
       pass
    
    def Foo(self):
        return 'bar'
    
    def BarWithStructureInvoke(self):
        return {'name': 'Bob', 'age': 10, 'gender': 'male'}
    
    def SumArray(self, a:[]):
        a = np.array(a)
        sum = np.sum(a)
        return float(sum)
    
   
    
server = SimpleXMLRPCServer(('127.0.0.1', 8000), logRequests=True, requestHandler=RequestHandler)

server.register_instance(Funcs())
server.serve_forever()