using System;

namespace Scientist.Exceptions
{
    public class BadBehaviourException : Exception
    {
        public BadBehaviourException(BaseExperiment ExperimentIn, string NameIn, string Message) 
            : base(Message)
        {
            Experiment = ExperimentIn;
            Name = NameIn;
        }

        public BaseExperiment Experiment { get; private set; }
        public string Name { get; private set; }
    }
}
