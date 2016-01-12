namespace Scientist.Exceptions
{
    public class BehaviourMissingException : BadBehaviourException
    {
        public BehaviourMissingException(BaseExperiment Experiment, string Name)
            : base(Experiment, Name, string.Format("{0} missing {1} behaviour", Experiment.Name, Name))
        {            
        }
    }
}
