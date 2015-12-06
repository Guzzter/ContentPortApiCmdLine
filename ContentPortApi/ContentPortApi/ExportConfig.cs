using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPortApi
{


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ExportConfig
    {

        private ExportConfigGeneral generalField;

        private ExportConfigPackage[] packageField;

        /// <remarks/>
        public ExportConfigGeneral General
        {
            get
            {
                return this.generalField;
            }
            set
            {
                this.generalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Package")]
        public ExportConfigPackage[] Package
        {
            get
            {
                return this.packageField;
            }
            set
            {
                this.packageField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ExportConfigGeneral
    {

        private string exportDirectoryField;

        private string logLevelField;

        private string bluePrintModeField;

        /// <remarks/>
        public string ExportDirectory
        {
            get
            {
                return this.exportDirectoryField;
            }
            set
            {
                this.exportDirectoryField = value;
            }
        }

        /// <remarks/>
        public string LogLevel
        {
            get
            {
                return this.logLevelField;
            }
            set
            {
                this.logLevelField = value;
            }
        }

        /// <remarks/>
        public string BluePrintMode
        {
            get
            {
                return this.bluePrintModeField;
            }
            set
            {
                this.bluePrintModeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ExportConfigPackage
    {

        private string zipFileField;

        private string[] itemsSelectionField;

        private string[] subtreeSelectionField;

        private string[] processDefinitionsSelectionField;

        private string[] taxonomiesSelectionField;

        private string[] approvalStatusesSelectionField;

        private string[] multimediaTypesSelectionField;

        private string[] groupsSelectionField;

        /// <remarks/>
        public string ZipFile
        {
            get
            {
                return this.zipFileField;
            }
            set
            {
                this.zipFileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] ItemsSelection
        {
            get
            {
                return this.itemsSelectionField;
            }
            set
            {
                this.itemsSelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] SubtreeSelection
        {
            get
            {
                return this.subtreeSelectionField;
            }
            set
            {
                this.subtreeSelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] ProcessDefinitionsSelection
        {
            get
            {
                return this.processDefinitionsSelectionField;
            }
            set
            {
                this.processDefinitionsSelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] TaxonomiesSelection
        {
            get
            {
                return this.taxonomiesSelectionField;
            }
            set
            {
                this.taxonomiesSelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] ApprovalStatusesSelection
        {
            get
            {
                return this.approvalStatusesSelectionField;
            }
            set
            {
                this.approvalStatusesSelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] MultimediaTypesSelection
        {
            get
            {
                return this.multimediaTypesSelectionField;
            }
            set
            {
                this.multimediaTypesSelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public string[] GroupsSelection
        {
            get
            {
                return this.groupsSelectionField;
            }
            set
            {
                this.groupsSelectionField = value;
            }
        }
    }




}
