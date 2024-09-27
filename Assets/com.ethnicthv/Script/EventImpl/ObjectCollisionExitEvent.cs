namespace com.ethnicthv.Script.EventImpl
{
    public class ObjectCollisionExitEvent : Event
    {
        public readonly ICollider BaseCollider;
        public readonly ICollider TargetCollider;
        
        public ObjectCollisionExitEvent(ICollider baseCollider, ICollider targetCollider) : base(2)
        {
            BaseCollider = baseCollider;
            TargetCollider = targetCollider;
        }
    }
}