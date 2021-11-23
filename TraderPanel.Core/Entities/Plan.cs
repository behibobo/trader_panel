using System;
using TraderPanel.Core.Entities;

namespace TraderPanel.Core.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public float TraderRate { get; set; }
        public float PanelRate { get; set; }
        public float CustomerRate { get; set; }
    }
}
