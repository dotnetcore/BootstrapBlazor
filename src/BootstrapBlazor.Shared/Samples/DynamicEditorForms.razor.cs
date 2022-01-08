using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DynamicEditorForms
    {

        private object model = new F1();


        private IRenderFlag editor;

        private void Switch()
        {
            if (this.model.GetType() == typeof(F1))
                this.model = new F2();
            else
                this.model = new F1();

            this.editor.Reset();
            this.StateHasChanged();
        }


        public class F1
        {
            public string Name { get; set; }
        }

        public class F2
        {
            public int ID { get; set; }
        }
    }
}
