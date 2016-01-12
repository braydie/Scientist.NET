namespace Scientist.Exceptions
{
    public class BehaviourNotUniqueException : BadBehaviourException
    {
        public BehaviourNotUniqueException(BaseExperiment Experiment, string Name) 
            : base(Experiment, Name, string.Format("{0} already has {1} behaviour", Experiment.Name, Name))
        {            
        }
    }
}
