
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   public class LayerViewModel
    {
        public int LayerId { get; set; }
        public string LayerName { get; set; }
    }
    
    public class ApprovalLayerViewModel
    {
        public int ApprovalId { get; set; }
        public bool Layer1 { get; set; }
        public string Layer1UserName { get; set; }
        public bool Layer2 { get; set; }
        public string Layer2UserName { get; set; }
        public bool Layer3 { get; set; }
        public string Layer3UserName { get; set; }
        public bool Layer4 { get; set; }
        public string Layer4UserName { get; set; }
        public bool Layer5 { get; set; }
        public string Layer5UserName { get; set; }
        public bool Layer6 { get; set; }
        public string Layer6UserName { get; set; }
        public bool Layer7 { get; set; }
        public string Layer7UserName { get; set; }
        public bool Layer8 { get; set; }
        public string Layer8UserName { get; set; }
        public bool Layer9 { get; set; }
        public string Layer9UserName { get; set; }
        public bool Layer10 { get; set; }
        public string Layer10UserName { get; set; }
        public string CurrentLevel { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime Layer1ApprovalDate { get; set; }
        public DateTime Layer2ApprovalDate { get; set; }
        public DateTime Layer3ApprovalDate { get; set; }
        public DateTime Layer4ApprovalDate { get; set; }
        public DateTime Layer5ApprovalDate { get; set; }
        public DateTime Layer6ApprovalDate { get; set; }
        public DateTime Layer7ApprovalDate { get; set; }
        public DateTime Layer8ApprovalDate { get; set; }
        public DateTime Layer9ApprovalDate { get; set; }
        public DateTime Layer10ApprovalDate { get; set; }
        public Nullable<int> TransferOrderRequisitionId { get; set; }
        public Nullable<int> TransferRequisitionId { get; set; }
        public Nullable<int> ProcurementRequisitionId { get; set; }
        public Nullable<int> ProductionRequisitionId { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class LayerAssignViewModel
    {
      
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int LayerId { get; set; }
        public string LayerName { get; set; }

    }
}
