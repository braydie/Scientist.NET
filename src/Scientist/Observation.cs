using System;
using Scientist.Exceptions;

namespace Scientist
{
    public class Observation
    {
        public Observation(BaseExperiment ExperimentIn, string BehaviourName, dynamic Block)
        {
            Experiment = ExperimentIn;
            Name = BehaviourName;
            var Now = DateTime.Now;
            try
            {
                Result = Block();                
            }
            catch (Exception Ex)
            {
                Exception = Ex;
            }
            Duration = (DateTime.Now - Now).TotalSeconds;            
        }

        public BaseExperiment Experiment { get; private set; }
        public string Name { get; private set; }
        public double Duration { get; private set; }
        public Exception Exception { get; private set; }
        public dynamic Result { get; private set; }
    }
}
