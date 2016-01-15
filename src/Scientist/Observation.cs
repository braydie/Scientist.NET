using System;

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

        public bool IsEquivalentTo(Observation Other)
        {
            var BothRaisedExceptions = (Other.Exception != null && Exception != null);
            var NeitherRaisedExceptions = (Other.Exception == null && Exception == null);

            var ValuesAreEqual = Other.Result == Result;

            var ExceptionsAreEquivalent = (BothRaisedExceptions &&
                                           Other.Exception.GetType() == Exception.GetType() &&
                                           Other.Exception.Message == Exception.Message);

            return (NeitherRaisedExceptions && ValuesAreEqual) || (BothRaisedExceptions && ExceptionsAreEquivalent);
        }
    }
}
