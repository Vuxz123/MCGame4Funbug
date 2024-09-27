namespace com.ethnicthv.Script.EventImpl
{
    public class ObjectCollisionEnterEvent : Event
    {
        public readonly ICollider BaseCollider;
        public readonly ICollider TargetCollider;
        
        public ObjectCollisionEnterEvent(ICollider baseCollider, ICollider targetCollider) : base(1)
        {
            BaseCollider = baseCollider;
            TargetCollider = targetCollider;
        }
    }
}