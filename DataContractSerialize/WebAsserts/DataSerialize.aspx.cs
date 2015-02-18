using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataContractSerialize
{

    [System.Runtime.Serialization.DataContract] //DataContract attr for WCF service. not serializeable attr.
    public class PersonData
    {
        [System.Runtime.Serialization.DataMember] //DataMember attr required to serialize. Each individual member need to have this attr to expose for serialize
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDirty { get; set; }
    }

    public partial class DataSerialize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}