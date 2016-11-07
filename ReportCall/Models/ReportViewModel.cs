using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace ReportCall.Models
{
    public class ReportViewModel
    {
        [DisplayName("AgentId")]
        public string AgentIdSubmit { get; set; }

        [DisplayName("SomeId")]
        public string SomeIdSubmit { get; set; }

        [DisplayName("CurrencyId")]
        public string CurrencySubmit { get; set; }

        [DisplayName("PropertyId")]
        public string PropertySubmit { get; set; }

        [DisplayName("AvailabilityId")]
        public string AvailabilityIdSubmit { get; set; }

        [DisplayName("Locale")]
        public string LocaleSubmit { get; set; }

        public IEnumerable<SelectListItem> AgentId { get; set; }

        public IEnumerable<SelectListItem> SomeId { get; set; }

        public IEnumerable<SelectListItem> CurrencyId { get; set; }

        public IEnumerable<SelectListItem> PropertyId { get; set; }

        public IEnumerable<SelectListItem> AvailabilityId { get; set; }

        public IEnumerable<SelectListItem> Locale { get; set; }
    }
}