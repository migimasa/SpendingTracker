using System.Collections.Generic;

namespace TrackerService.Base
{
    public class ServiceResult
    {
        public bool IsSuccess
        {
            get { return Errors.Count == 0; }
        }

        public List<string> Errors { get; set; } = new List<string>();
        
        public object Data { get; set; }

        public void AddErrorMessage(string error)
        {
            Errors.Add(error);
        }
    }
}