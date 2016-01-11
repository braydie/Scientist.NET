using System;

namespace Scientist.Exceptions
{
    public class BehaviourMissingException : Exception
    {
        public BehaviourMissingException(BaseExperiment Experiment, string Name)
            : base(string.Format("{0} missing {1} behaviour", Experiment.Name, Name))
        {            
        }
    }
}
