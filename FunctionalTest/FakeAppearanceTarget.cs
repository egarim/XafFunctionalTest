using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionalTest
{
    public class FakeAppearanceTarget : IAppearanceEnabled
    {
        #region IAppearanceEnabled Members
        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        public void ResetEnabled()
        {
            Enabled = true;
        }
        #endregion
    }
}
