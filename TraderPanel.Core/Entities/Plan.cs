using System;
using TraderPanel.Core.Entities;

namespace TraderPanel.Core.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public decimal TraderRate { get; set; }
        public decimal PanelRate { get; set; }
        public decimal CustomerRate { get; set; }
    }
}
