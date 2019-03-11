using System;

namespace MoqNetCoreGenerator.CLI
{
    public class SyntaxProcessor
    {
        private readonly string _className;
        private readonly string _methodName;
        private readonly string _outputFilePath; 
        
        public SyntaxProcessor(string className, string methodName, string outputFilePath)
        {
            _className = className;
            _methodName = methodName;
            _outputFilePath = outputFilePath;
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}