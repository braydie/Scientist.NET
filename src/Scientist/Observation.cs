using System;

namespace Scientist
{
    public class Observation
    {
        public Observation(string NameIn, dynamic Block)
        {
            Name = NameIn;
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

        public string Name { get; private set; }
        public double Duration { get; private set; }
        public Exception Exception { get; private set; }
        public dynamic Result { get; private set; }
    }
}
