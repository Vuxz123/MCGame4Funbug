namespace com.ethnicthv.Script.EventImpl
{
    public class ObjectCollisionEvent : Event
    {
        public readonly ICollider BaseCollider;
        public readonly ICollider TargetCollider;
        
        public ObjectCollisionEvent(ICollider baseCollider, ICollider targetCollider) : base(0)
        {
            BaseCollider = baseCollider;
            TargetCollider = targetCollider;
        }
    }
}