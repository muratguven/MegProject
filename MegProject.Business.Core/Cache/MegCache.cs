using System;

namespace MegProject.Business.Core.Cache
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MegCache : Attribute
    {
       
        private readonly int _duration;

        // This is a positional argument
        public MegCache(int duration)
        {
            this._duration = duration;            
        }

        public int Duration
        {
            get { return this._duration; }             
        }

        
    }
  
}