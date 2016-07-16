using System;

namespace MegProject.Business.Core.Cache
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MegCache : Attribute
    {
       
        private readonly int _duration;
        private readonly string _key;

        // This is a positional argument
        public MegCache(string key,int duration)
        {
            this._duration = duration;
            this._key = key;
        }

        public int Duration
        {
            get { return this._duration; }             
        }

        public string Key
        {
            get { return _key; }
        }



        
    }
  
}