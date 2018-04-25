using DS.SimpleServiceBus.Events.Enums;

namespace DS.SimpleServiceBus.Events
{
    public class ModelPropertyChange
    {
        public string PropertyName { get; set; }
        public object ValueBefore { get; set; }
        public object ValueAfter { get; set; }
        public ModelPropertyChangeTypeEnum Type { get; set; }
    }
}
