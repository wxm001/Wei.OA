namespace Wei.OA.Workflow
{
    using System;

    public class BaseResumeBookMarkValue
    {
        public object Value { get; set; }

        public string BookMarkName { get; set; }

        public Guid InstanceId { get; set; }
    }
}