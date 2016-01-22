using System;
using Scientist.Exceptions;

namespace Scientist
{
    public class Experiment<T> : BaseExperiment
    {                     
        public Experiment() 
            : this("Experiment")
        {            
        }

        public Experiment(string ExperimentName) 
            : base(ExperimentName)
        {                        
        }

        public void Use(Func<T> ControlIn)
        {
            if(Control != null)
            {
                throw new BehaviourNotUniqueException(this, "Control");
            }
            Control = ControlIn;
        }
        
        public void Try(Func<T> CandidateIn)
        {            
            Candidate = CandidateIn;
        }
        
        public new T Run()
        {
            return (T)base.Run();
        }
    }    
}
